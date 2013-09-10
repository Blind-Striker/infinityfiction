using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace CodeFiction.InfinityFiction.Core.Container
{
    public class Bootstrapper
    {
        private readonly IWindsorContainer _container;

        private Bootstrapper()
        {
            _container = new WindsorContainer();
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
    }
}
