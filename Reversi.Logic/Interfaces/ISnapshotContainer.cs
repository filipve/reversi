using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Interfaces
{
    public interface ISnapshotContainer<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        void SetInitialState(T item);
        void Clear();
        T Redo();
        void TakeSnapShot(T item);
        T Undo();
        bool CanUndo();
        bool CanRedo();
    }
}