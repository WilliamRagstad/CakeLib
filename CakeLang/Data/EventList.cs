using System;
using System.Collections.Generic;
using System.Text;

namespace CakeLang.Data
{
    public class EventList<T> : List<T>
    {
        public EventList() : base() { }

        public EventList(IEnumerable<T> collection) : base(collection) { }

        public EventList(int capacity) : base(capacity) { }

        public void SetOnAdd(Action<T> action) => OnAdd = action;
        private Action<T> OnAdd;
        new public void Add(T item)
        {
            base.Add(item);
            OnAdd(item);
        }
    }
}
