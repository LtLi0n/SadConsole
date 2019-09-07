using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SadConsole
{
    public class ContainerManager
    {
        private readonly IServiceProvider _services;
        private readonly IServiceProvider _containers;

        public FontContainer Fonts => GetContainer<FontContainer>();

        public T GetContainer<T>() where T : ContainerBase => _containers.GetService<T>();

        public ContainerManager(IServiceProvider services)
        {
            _services = services;
            _containers = CreateContainers();
        }

        private IServiceProvider CreateContainers()
        {
            ServiceCollection sc = new ServiceCollection();
            
            sc.AddSingleton<FontContainer>();

            return sc.BuildServiceProvider();
        }
    }
}
