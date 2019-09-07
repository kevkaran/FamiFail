using System.Collections.Generic;

namespace FamiFail.Common.DataContracts.Device.Cpu
{
    public interface ICpu
    {
        ICollection<IRegister> Registers { get; }

        int ProgramCounter { get; }

        void Step();
    }
}
