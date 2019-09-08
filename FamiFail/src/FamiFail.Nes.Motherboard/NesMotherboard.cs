using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using FamiFail.Common.DataContracts.BusDevice;
using FamiFail.Common.DataContracts.Motherboard;
using FamiFail.Cpu.M6502.Interfaces;
using FamiFail.Cpu.M6502.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FamiFail.Nes.Motherboard
{
    public class NesMotherboard : IMotherboard
    {
        private readonly IM6502Cpu _cpu;
        private readonly Timer cpuClock = new Timer();
        public ICollection<IBus> Buses { get; }
        public ICollection<IBusDevice> Devices { get; }

        public NesMotherboard(IM6502Cpu cpu)
        {
            _cpu = cpu;
        }
    }
}