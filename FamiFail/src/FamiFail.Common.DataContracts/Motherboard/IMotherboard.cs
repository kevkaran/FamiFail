using System;
using System.Collections.Generic;
using System.Text;
using FamiFail.Common.DataContracts.BusDevice;

namespace FamiFail.Common.DataContracts.Motherboard
{
    public interface IMotherboard
    {
        ICollection<IBus> Buses { get; }
        ICollection<IBusDevice> Devices { get; }
    }
}