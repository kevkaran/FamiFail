using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FamiFail.Common.DataContracts.BusDevice;
using FamiFail.Common.DataContracts.BusDevice.Memory;
using FamiFail.Common.DataContracts.Motherboard;
using FamiFail.Common.Jellybean.Services;
using FamiFail.Nes.DataContracts;

namespace FamiFail.Nes.Mappers
{
    /// <summary>
    /// This is a super basic mapper for testing. It has a ram at x0000 to x3999 and a rom from x4000 to xFFFF
    /// </summary>
    public class TestMapper : INesMapper
    {
        //todo the mappers should be data driven, hard coding this one for testing the CPU
        private readonly Ram _ram;

        private readonly Rom _rom;

        public TestMapper(Ram ram, Rom rom)
        {
            _ram = ram;
            _rom = rom;
        }

        public async Task<int> ReadAsync(int address)
        {
            if (address < 0x4000)
            {
                return await _ram.ReadAsync(address);
            }
            else if (address >= 0x4000 || address <= 0xFFFF)
            {
                return await _rom.ReadAsync(address);
            }

            return 0;
        }

        public async Task WriteAsync(int address, int value)
        {
            if (address < 0x4000)
            {
                await _ram.WriteAsync(address, value);
            }
            else if (address >= 0x4000 || address <= 0xFFFF)
            {
                await _rom.WriteAsync(address, value);
            }
        }

        public ICollection<IBusDevice> Devices
        {
            get => new IBusDevice[] { _ram, _rom };
            set => throw new NotSupportedException();
        }
    }
}