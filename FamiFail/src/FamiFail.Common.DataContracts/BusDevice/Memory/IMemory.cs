namespace FamiFail.Common.DataContracts.BusDevice.Memory
{
    public interface IMemory : IBusDevice
    {
        int[] Memory { get; set; }
    }
}