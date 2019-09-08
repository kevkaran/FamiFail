using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FamiFail.Common.DataContracts.BusDevice.Memory;

namespace FamiFail.Common.Jellybean.Services
{
    public class Rom : IMemory
    {
        public async Task<int> ReadAsync(int address)
        {
            return Memory[address];
        }

        public Task WriteAsync(int address, int value)
        {
            return Task.CompletedTask; // Do nothing
        }

        public int[] Memory { get; set; }
    }
}