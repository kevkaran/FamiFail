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
            if (address > Memory.Length) return 0;
            return Memory[address];
        }

        public async Task WriteAsync(int address, int value)
        {
            if (address > Memory.Length) return;
            Memory[address] = value;
        }

        public int[] Memory { get; set; }
    }
}