using System.Threading.Tasks;

namespace FamiFail.Cpu.M6502.Interfaces
{
    public interface IM6502Alu
    {
        Task ProcessAsync(int instruction);
    }
}