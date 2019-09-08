using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FamiFail.Common.DataContracts.BusDevice;

namespace FamiFail.Common.DataContracts.Motherboard
{
    public interface IBus : IBusDevice
    {
        int Address { get; set; }
        int Value { get; set; }

        ICollection<IBusDevice> Devices { get; }
    }
}