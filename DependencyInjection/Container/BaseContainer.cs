using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace CodeFiction.InfinityFiction.Core.Container
{
    public abstract class BaseContainer
    {
        private readonly IWindsorContainer _container;

        protected BaseContainer()
        {
            _container = new WindsorContainer();
        }

        protected BaseContainer(params IWindsorInstaller[] windsorInstaller)
            : this()
        {
            _container.Install(windsorInstaller);
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }
    }
}
