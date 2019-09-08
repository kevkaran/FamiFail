using System.Threading.Tasks;

namespace FamiFail.Common.DataContracts.BusDevice
{
    public interface IBusDevice
    {
        Task<int> ReadAsync(int address);

        Task WriteAsync(int address, int value);
    }
}