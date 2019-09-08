using System.Collections.Generic;
using System.Threading.Tasks;

namespace FamiFail.Common.DataContracts.BusDevice.Cpu
{
    public interface ICpu
    {
        IRegister[] Registers { get; }

        int ProgramCounter { get; }

        Task StepAsync();
    }
}