using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using FamiFail.Common.DataContracts;
using FamiFail.Common.DataContracts.BusDevice.Cpu;
using FamiFail.Common.DataContracts.BusDevice.Memory;
using FamiFail.Common.DataContracts.Motherboard;
using FamiFail.Common.Jellybean.Services;
using FamiFail.Cpu.M6502.Interfaces;
using FamiFail.Cpu.M6502.Registration;
using FamiFail.Nes.Mappers;

namespace CpuTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ram = new Ram { Memory = new int[0x4000] };
            var rom = new Rom { Memory = new int[0xFFFF] }; // For simplicity the first 0x4000 is wasted
            rom.Memory[0xFFFD] = 0x40; // Set reset vector to 4000 to start PC at top of ROM
            rom.Memory[0xFFFC] = 0x00;
            rom.Memory[0x4000] = 0xA9; //LDA
            rom.Memory[0x4001] = 0x01;

            var serviceProvider = new ServiceCollection()
                .AddScoped<IBus, Bus>()
                .AddScoped<IMapper>(f => new TestMapper(ram, rom))
                .RegisterM6502();

            var provider = serviceProvider.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var cpu = scope.ServiceProvider.GetService<IM6502Cpu>();
                while (true)
                {
                    cpu.StepAsync();
                }
            }
        }
    }
}