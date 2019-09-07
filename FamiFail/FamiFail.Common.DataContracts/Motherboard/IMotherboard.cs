using System;
using System.Collections.Generic;
using System.Text;
using FamiFail.Common.DataContracts.Device;
using FamiFail.Common.DataContracts.Device.Cpu;

namespace FamiFail.Common.DataContracts.Motherboard
{
    public interface IMotherboard
    {
        ICollection<IBus> Buses { get; }
        ICollection<ICpu> Cpus { get; }

        ICollection<IDevice>
    }
}
