using Reversi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Classes
{

    public class SnapshotContainer<T> : ISnapshotContainer<T>
    {
        private Stack<T> _undoStack;
        private Stack<T> _redoStack;
        private T _initialState;

        public SnapshotContainer()
        {
            _undoStack = new Stack<T>();
            _redoStack = new Stack<T>();
        }

        public void Clear()
        {
            _undoStack.Clear();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);

            _redoStack.Clear();
        }

        public void TakeSnapShot(T item)
        {
            _undoStack.Push(item);
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);
            _redoStack.Clear();
        }

        public T Undo()
        {
            if (!_undoStack.Any())
            {
                return _initialState;
            }

            var previous = _undoStack.Pop();
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);

            _redoStack.Push(previous);

            return _undoStack.Any() ? _undoStack.Peek() : _initialState;

        }

        public T Redo()
        {
            if (!_redoStack.Any())
            {
                throw new InvalidOperationException("No action to redo.");
            }

            var next = _redoStack.Pop();

            _undoStack.Push(next);
            OnCollectionChanged(NotifyCollectionChangedAction.Reset);

            return _undoStack.Peek();
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged;

        void OnCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _undoStack.Reverse().GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _undoStack.Reverse().GetEnumerator();
        }

        public void SetInitialState(T item)
        {
            _initialState = item;
        }


        public bool CanUndo()
        {
            return _undoStack.Any();
        }

        public bool CanRedo()
        {
            return _redoStack.Any();
        }
    }


}
