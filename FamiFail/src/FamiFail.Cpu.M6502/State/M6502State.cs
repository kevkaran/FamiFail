using System;
using System.Collections.Generic;
using System.Linq;
using FamiFail.Common.DataContracts.BusDevice.Cpu;
using FamiFail.Cpu.M6502.DTO;
using FamiFail.Cpu.M6502.Interfaces;

namespace FamiFail.Cpu.M6502.State
{
    public class M6502State : IM6502State
    {
        public M6502GenericRegister ProgramCounter { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister ARegister { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister XRegister { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister YRegister { get; set; } = new M6502GenericRegister();
        public M6502GenericRegister StackPointer { get; set; } = new M6502GenericRegister();
        public M6502StatusRegister StatusRegister { get; set; } = new M6502StatusRegister();

        public Queue<Action> ActionQueue { get; } = new Queue<Action>();
        public M6502GenericRegister AluScratch { get; } = new M6502GenericRegister();

        private readonly List<Action> _scratchActions = new List<Action>();

        public void Add(Action action)
        {
            _scratchActions.Add(action);
        }

        public void Combine(Action action)
        {
            var lastAction = _scratchActions.Last();
            if (lastAction == null)
                _scratchActions.Add(action);
            else
            {
                _scratchActions.Remove(lastAction);
                _scratchActions.Add(() => { lastAction.Invoke(); action.Invoke(); });
            }
        }

        public void Enqueue()
        {
            foreach (var action in _scratchActions)
            {
                ActionQueue.Enqueue(action);
            }
            _scratchActions.Clear();
        }
    }
}