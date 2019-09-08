using System;
using FamiFail.Common.DataContracts;
using FamiFail.Cpu.M6502.Interfaces;
using FamiFail.Cpu.M6502.Services;
using FamiFail.Cpu.M6502.State;
using Microsoft.Extensions.DependencyInjection;

namespace FamiFail.Cpu.M6502.Registration
{
    public static class M6502ServiceRegistrar
    {
        public static IServiceCollection RegisterM6502(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IM6502Alu, M6502Alu>();
            serviceCollection.AddTransient<IM6502Cpu, M6502Cpu>();
            serviceCollection.AddScoped<IM6502State, M6502State>();
            return serviceCollection;
        }
    }
}