using System;
using System.Collections.Generic;
using System.Text;
using FamiFail.Common.DataContracts.BusDevice.Cpu;

namespace FamiFail.Cpu.M6502.DTO
{
    public class M6502GenericRegister : IRegister
    {
        public int Value { get; set; }
    }
}