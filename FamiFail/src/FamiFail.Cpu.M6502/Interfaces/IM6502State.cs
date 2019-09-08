using System;
using System.Collections.Generic;
using FamiFail.Common.DataContracts.BusDevice.Cpu;
using FamiFail.Cpu.M6502.DTO;

namespace FamiFail.Cpu.M6502.Interfaces
{
    public interface IM6502State
    {
        M6502GenericRegister ProgramCounter { get; set; }
        M6502GenericRegister ARegister { get; set; }
        M6502GenericRegister XRegister { get; set; }
        M6502GenericRegister YRegister { get; set; }
        M6502GenericRegister StackPointer { get; set; }
        M6502StatusRegister StatusRegister { get; set; }

        Queue<Action> ActionQueue { get; }
        M6502GenericRegister AluScratch { get; }

        void Add(Action action);

        void Combine(Action action);

        void Enqueue();
    }
}