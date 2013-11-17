using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace CodeFiction.InfinityFiction.Core.BootstrapperLib
{
    public class Bootstrapper
    {
        private readonly IWindsorContainer _container;

        private static readonly Dictionary<string, IInfinityModule> LoadedModules = new Dictionary<string, IInfinityModule>();

        private readonly List<ComposablePartCatalog> _catalogs;
        private readonly List<CompositionBatch> _compositionBatches;
        private CompositionContainer _compositionContainer;

        private Bootstrapper()
        {
            _container = new WindsorContainer();

            _catalogs = new List<ComposablePartCatalog>();
            _compositionBatches = new List<CompositionBatch>();
        }

        public IWindsorContainer WindsorContainer
        {
            get
            {
                return _container;
            }
        }

        public static Bootstrapper Create()
        {
            return new Bootstrapper();
        }

        public Bootstrapper RegisterInstaller(IWindsorInstaller windsorInstaller)
        {
            _container.Install(windsorInstaller);
            return this;
        }

        public Bootstrapper RegisterAssembly(Assembly assembly)
        {
            var assemblyCatalog = new AssemblyCatalog(assembly);
            _catalogs.Add(assemblyCatalog);
            return this;
        }

        public Bootstrapper RegisterPath(string path, string searchPattern)
        {
            if (Directory.Exists(path))
            {
                var directoryCatalog = new DirectoryCatalog(path, searchPattern);
                _catalogs.Add(directoryCatalog);
            }

            return this;
        }

        public Bootstrapper RegisterCatalog(ComposablePartCatalog catalog)
        {
            _catalogs.Add(catalog);
            return this;
        }

        public Bootstrapper RegisterModule<T>() where T : IInfinityModule
        {
            var compositionBatch = new CompositionBatch();
            compositionBatch.AddPart(Activator.CreateInstance<T>());

            _compositionBatches.Add(compositionBatch);

            return this;
        }

        public Bootstrapper InitializeApplication()
        {
            var aggregateCatalog = new AggregateCatalog();

            foreach (var catalog in _catalogs)
            {
                aggregateCatalog.Catalogs.Add(catalog);
            }

            _compositionContainer = new CompositionContainer(aggregateCatalog);

            foreach (var compositionBatch in _compositionBatches)
            {
                _compositionContainer.Compose(compositionBatch);
            }

            IEnumerable<IInfinityModule> modules = _compositionContainer.GetExportedValues<IInfinityModule>();

            LoadModules(modules);

            return this;
        }

        private void LoadModules(IEnumerable<IInfinityModule> modules)
        {
            foreach (var module in modules)
            {
                Type moduleType = module.GetType();
                if (!LoadedModules.ContainsKey(module.GetType().FullName) && !(moduleType.IsAbstract || moduleType.IsInterface))
                {
                    LoadedModules.Add(module.GetType().FullName, module);
                    module.OnRegisterDependencies(_container);
                }
            }
        }
    }
}
