using System;
using System.Collections.Generic;
using System.Text;
using FamiFail.Common.DataContracts.BusDevice;
using FamiFail.Common.DataContracts.Motherboard;

namespace FamiFail.Common.DataContracts
{
    public interface IMapper : IBusDevice
    {
        ICollection<IBusDevice> Devices { get; set; }
    }
}