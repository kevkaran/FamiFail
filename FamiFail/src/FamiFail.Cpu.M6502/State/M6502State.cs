using FamiFail.Common.DataContracts.BusDevice.Cpu;
using FamiFail.Cpu.M6502.DTO;
using FamiFail.Cpu.M6502.Interfaces;

namespace FamiFail.Cpu.M6502.State
{
    public class M6502State : IM6502State
    {
        public M6502GenericRegister ProgramCounter { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister ARegister { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister XRegister { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister YRegister { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister StackPointer { get; set; } = new M6502GenericRegister();
        public M6502StatusRegister StatusRegister { get; set; } = new M6502StatusRegister();
    }
}