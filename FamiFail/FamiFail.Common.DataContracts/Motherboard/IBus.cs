using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FamiFail.Common.DataContracts.Motherboard
{
    public interface IBus
    {
        int Address { get; set; }
        int Value { get; set; }

        bool Read { get; set; }

    }
}
