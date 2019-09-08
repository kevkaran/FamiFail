using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FamiFail.Common.DataContracts.BusDevice.Cpu;
using FamiFail.Common.DataContracts.Motherboard;
using FamiFail.Cpu.M6502.Interfaces;

namespace FamiFail.Cpu.M6502.Services
{
    public class M6502Cpu : IM6502Cpu
    {
        private readonly IM6502Alu _alu;
        private readonly IM6502State _state;
        private readonly IBus _bus;

        public M6502Cpu(IM6502Alu alu, IM6502State state, IBus bus)
        {
            _alu = alu;
            _state = state;
            _bus = bus;

            ResetAsync();
        }

        public async void ResetAsync()
        {   // Pull start address from reset vector and store in program counter
            var low = await _bus.ReadAsync(0xFFFC);
            var high = await _bus.ReadAsync(0xFFFD);
            ProgramCounter = low | (high << 8);
        }

        public IRegister[] Registers => new IRegister[]
        {
            _state.ARegister,
            _state.XRegister,
            _state.YRegister,
            _state.StackPointer,
            _state.StatusRegister,
            _state.ProgramCounter
        };

        public int ProgramCounter
        {
            get => _state.ProgramCounter.Value;
            private set => _state.ProgramCounter.Value = value;
        }

        public async Task StepAsync()
        {
            if (!_state.ActionQueue.Any())
            { // In order to simulate instructions taking multiple cycles we need to build
                // a queue of things the CPU is going to do
                var instruction = await _bus.ReadAsync(ProgramCounter++);
                switch ((instruction & 0x03))
                {
                    case 0x00: // Control Operations

                        break;

                    case 0x01: // ALU Operations
                        await _alu.ProcessAsync(instruction);
                        break;

                    case 0x02: // Read Write Modify operations

                        break;

                    case 0x03: // Unofficial opcodes

                        break;
                }
            }
            else
            { //The queue has things to do, so do those things
                _state.ActionQueue.Dequeue().Invoke();
            }
        }
    }
}