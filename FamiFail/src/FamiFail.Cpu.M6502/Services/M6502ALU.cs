using System;
using System.Collections.Generic;
using System.Linq;
using FamiFail.Common.DataContracts.Motherboard;
using FamiFail.Cpu.M6502.Interfaces;
using System.Threading.Tasks;
using FamiFail.Cpu.M6502.State;

namespace FamiFail.Cpu.M6502.Services
{
    public class M6502Alu : IM6502Alu
    {
        private readonly IBus _bus;
        private readonly IM6502State _state;

        public M6502Alu(IBus bus, IM6502State state)
        {
            _bus = bus;
            _state = state;
        }

        private int Scratch
        {
            get => _state.AluScratch.Value;
            set => _state.AluScratch.Value = value;
        }

        public async Task ProcessAsync(int instruction)
        {
            // var operand = 0;

            //ALU instructions operands are consistent
            switch (instruction & 0b_000_111_00)
            {
                case 0b_000_000_00: //(d,x) -- Indexed Indirect
                    _state.Add(() => { Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result; });
                    _state.Add(() => { Scratch = (Scratch + _state.XRegister.Value) & 0xFF; });
                    _state.Add(() =>
                    {
                        {
                            var temp = Scratch;
                            Scratch = _bus.ReadAsync(temp).Result;
                            //Scratch = Scratch << 8;
                            Scratch = Scratch | (_bus.ReadAsync(temp + 1).Result << 8);
                        }
                    });
                    _state.Add(() => { Scratch = _bus.ReadAsync(Scratch).Result; });
                    break;

                case 0b_000_001_00: //d -- Zero Page, load value of memory at pointer loaded from program counter
                    //operand = await _bus.ReadAsync(_state.ProgramCounter.Value++);
                    _state.Add(() => { Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result; });
                    _state.Add(() => { Scratch = _bus.ReadAsync(Scratch).Result; });
                    break;

                case 0b_000_010_00: //#i -- Immediate, load value of memory at Program Counter to operand
                    _state.Add(() => { Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result; });

                    break;

                case 0b_000_011_00: //a -- Absolute -- Load next two memory values into scratch
                    _state.Add(() => { Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result; });
                    _state.Add(() => { Scratch = Scratch; });
                    _state.Add(() =>
                    {
                        Scratch = Scratch | (_bus.ReadAsync(_state.ProgramCounter.Value++).Result << 8);
                    });
                    break;

                case 0b_000_100_00: //(d),y -- Indirect Indexed
                    _state.Add(() => { Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result; });
                    _state.Add(() =>
                    {
                        {
                            var temp = Scratch;
                            Scratch = _bus.ReadAsync(temp).Result;
                            //Scratch = Scratch << 8;
                            Scratch = Scratch | (_bus.ReadAsync(temp + 1).Result << 8);
                        }
                    });
                    _state.Add(() => { Scratch = _bus.ReadAsync(Scratch).Result; });
                    _state.Add(() => { Scratch = (Scratch + _state.YRegister.Value) & 0xFF; });

                    break;

                case 0b_000_101_00: //d,x -- Zero Page,X --
                    _state.Add(() =>
                    {
                        Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result;
                    });
                    _state.Add(() => { Scratch = _bus.ReadAsync(Scratch).Result; });
                    _state.Add(() => { Scratch = (Scratch + _state.XRegister.Value) & 0xFF; });
                    break;

                case 0b_000_110_00: //a,y
                    _state.Add(() => { Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result; });
                    _state.Add(() => { Scratch = Scratch; });
                    _state.Add(() =>
                    { //Todo some operations take an extra clock "if page crossed"... what does this mean exactly
                        Scratch = Scratch | (_bus.ReadAsync(_state.ProgramCounter.Value++).Result << 8);
                        Scratch += (Scratch + _state.YRegister.Value) & 0xFF;
                    });
                    break;

                case 0b_000_111_00: //a,x
                    _state.Add(() => { Scratch = _bus.ReadAsync(_state.ProgramCounter.Value++).Result; });
                    _state.Add(() => { Scratch = Scratch; });
                    _state.Add(() =>
                    { //Todo some operations take an extra clock "if page crossed"... what does this mean exactly
                        Scratch = Scratch | (_bus.ReadAsync(_state.ProgramCounter.Value++).Result << 8);
                        Scratch += (Scratch + _state.XRegister.Value) & 0xFF;
                    });
                    break;
            }

            switch (instruction & 0b_111_00000) // Get type of operation
            {
                case 0b_000_00000: //ORA
                    _state.Combine(() =>
                    {
                        _state.ARegister.Value |= Scratch;
                        _state.StatusRegister.Zero = (_state.ARegister.Value == 0);
                        _state.StatusRegister.Negative = ((_state.ARegister.Value & 0b_1_0000000) == 0b_1_0000000);
                    });
                    break;

                case 0b_001_00000: //AND
                    _state.Combine(() =>
                    {
                        _state.ARegister.Value &= Scratch;
                        _state.StatusRegister.Zero = (_state.ARegister.Value == 0);
                        _state.StatusRegister.Negative = ((_state.ARegister.Value & 0b_1_0000000) == 0b_1_0000000);
                    });
                    break;

                case 0b_010_00000: //EOR
                    _state.Combine(() =>
                    {
                        _state.ARegister.Value ^= Scratch;
                        _state.StatusRegister.Zero = (_state.ARegister.Value == 0);
                        _state.StatusRegister.Negative = ((_state.ARegister.Value & 0b_1_0000000) == 0b_1_0000000);
                    });
                    break;

                case 0b_011_00000: //ADC
                    _state.Combine(() =>
                    {
                        _state.ARegister.Value += Scratch;
                        _state.StatusRegister.Zero = (_state.ARegister.Value == 0);
                        _state.StatusRegister.Negative = ((_state.ARegister.Value & 0b_1_0000000) == 0b_1_0000000);
                        if (_state.ARegister.Value > 0xFF)
                        {
                            _state.ARegister.Value &= 0xFF;
                            _state.StatusRegister.Carry = true;
                            _state.StatusRegister.Overflow = _state.StatusRegister.Negative;
                        }
                    });
                    break;

                case 0b_100_00000: //STA
                    _state.Combine(() => { _bus.WriteAsync(Scratch, _state.ARegister.Value); });
                    break;

                case 0b_101_00000: //LDA
                    _state.Combine(() =>
                    {
                        _state.ARegister.Value = Scratch;
                        _state.StatusRegister.Zero = (_state.ARegister.Value == 0);
                        _state.StatusRegister.Negative = ((_state.ARegister.Value & 0b_1_0000000) == 0b_1_0000000);
                    });
                    break;

                case 0b_110_00000: //CMP
                    _state.Combine(() =>
                    {
                        _state.StatusRegister.Carry = _state.ARegister.Value >= Scratch;
                        _state.StatusRegister.Zero = _state.ARegister.Value == Scratch;
                        _state.StatusRegister.Negative = ((_state.ARegister.Value & 0b_1_0000000) == 0b_1_0000000);
                    });
                    break;

                case 0b_111_00000: //SBC
                    _state.Combine(() =>
                    {
                        _state.ARegister.Value -= Scratch - (1 - (_state.StatusRegister.Carry ? 1 : 0));
                        _state.StatusRegister.Zero = (_state.ARegister.Value == 0);
                        _state.StatusRegister.Negative = ((_state.ARegister.Value & 0b_1_0000000) == 0b_1_0000000);
                        if (_state.ARegister.Value < -0xFF)
                        {
                            _state.ARegister.Value &= 0xFF;
                            _state.StatusRegister.Carry = true;
                            _state.StatusRegister.Overflow = _state.StatusRegister.Negative;
                        }
                    });
                    break;
            }
            _state.Enqueue();
        }
    }
}