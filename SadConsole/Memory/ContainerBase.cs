using System;
using System.Collections.Generic;

namespace SadConsole
{
    public abstract class ContainerBase
    {
        protected readonly IServiceProvider _services;

        public ContainerBase(IServiceProvider services)
        {
            _services = services;
        }
    }

    public abstract class ContainerBase<T> : ContainerBase where T : class
    {
        protected readonly Dictionary<string, T> _items;
        public IReadOnlyDictionary<string, T> Items => _items;

        public T this[string key]
        {
            get => _items.ContainsKey(key) ? _items[key] : null;
            set
            {
                if (_items.ContainsKey(key))
                {
                    _items[key] = value;
                }
                else
                {
                    _items.Add(key, value);
                }
            }
        }

        public ContainerBase(IServiceProvider services) : base(services)
        {
            _items = new Dictionary<string, T>();
        }
    }
}
