using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FamiFail.Common.DataContracts.BusDevice.Memory;

namespace FamiFail.Common.Jellybean.Services
{
    public class Ram : IMemory
    {
        public async Task<int> ReadAsync(int address)
        {
            return Memory[address];
        }

        public async Task WriteAsync(int address, int value)
        {
            Memory[address] = value;
        }

        public int[] Memory { get; set; }
    }
}