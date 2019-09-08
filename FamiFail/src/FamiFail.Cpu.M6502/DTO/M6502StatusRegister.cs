using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using FamiFail.Common.DataContracts.BusDevice.Cpu;

namespace FamiFail.Cpu.M6502.DTO
{
    public class M6502StatusRegister : IRegister
    {
        private BitVector32 _bitVector = new BitVector32() { [5] = true };

        public int Value
        {
            get => _bitVector.Data;
            set => _bitVector = new BitVector32(value) { [5] = true };
        }

        public bool Negative
        {
            get => _bitVector[7];
            set => _bitVector[7] = value;
        }

        public bool Overflow
        {
            get => _bitVector[6];
            set => _bitVector[6] = value;
        }

        public bool Break
        {
            get => _bitVector[4];
            set => _bitVector[4] = value;
        }

        public bool Decimal
        {
            get => _bitVector[3];
            set => _bitVector[3] = value;
        }

        public bool Interrupt
        {
            get => _bitVector[2];
            set => _bitVector[2] = value;
        }

        public bool Zero
        {
            get => _bitVector[1];
            set => _bitVector[1] = value;
        }

        public bool Carry
        {
            get => _bitVector[0];
            set => _bitVector[0] = value;
        }
    }
}