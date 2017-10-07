using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter.Utils
{
    public class UndoRedo<TState>
    {
        public class LoadStateEventArgs : EventArgs
        {
            public TState NewState { get; private set; }
       
            public LoadStateEventArgs(TState NewState)
            {
                this.NewState = NewState;
            }
        }

        private Stack<TState> StateStack = new Stack<TState>();
        private Stack<TState> OldStateStack = new Stack<TState>();

        public delegate void LoadStateDelegate(object sender, LoadStateEventArgs e);
        public event LoadStateDelegate LoadState = delegate { };

        public void NewState(TState newState, bool load = false)
        {
            StateStack.Push(newState);
            OldStateStack.Clear();

            if (load)
                LoadState(this, new LoadStateEventArgs(newState));
        }

        public void ClearAll()
        {
            StateStack.Clear();
            OldStateStack.Clear();
        }

        public void Undo()
        {
            if (StateStack.Count < 2)
                return;

            var curr = StateStack.Pop();
            OldStateStack.Push(curr);

            var next = StateStack.Peek();

            LoadState(this, new LoadStateEventArgs(next));
        }
        
        public void Redo()
        {
            if (!OldStateStack.Any())
                return;

            var old = OldStateStack.Pop();
            StateStack.Push(old);

            LoadState(this, new LoadStateEventArgs(old));
        }
    }
}
