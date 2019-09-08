using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FamiFail.Common.DataContracts;
using FamiFail.Common.DataContracts.BusDevice;
using FamiFail.Common.DataContracts.Motherboard;

namespace FamiFail.Common.Jellybean.Services
{
    public class Bus : IBus
    {
        private readonly IMapper _mapper;
        public int Address { get; set; }
        public int Value { get; set; }
        public ICollection<IBusDevice> Devices => _mapper.Devices;

        public Bus(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> ReadAsync(int address)
        {
            return Value = await _mapper.ReadAsync(Address = address);
        }

        public async Task WriteAsync(int address, int data)
        {
            await _mapper.WriteAsync(Address = address, Value = Value);
        }
    }
}