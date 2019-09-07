using FamiFail.Common.DataContracts.Motherboard;

namespace FamiFail.Common.DataContracts.Device
{
    public interface IDevice
    {
        IBus Bus { get; set; }
    }
}
