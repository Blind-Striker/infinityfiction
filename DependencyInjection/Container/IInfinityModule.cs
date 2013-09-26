using Castle.Windsor;

namespace CodeFiction.InfinityFiction.Core.Container
{
    public interface IInfinityModule
    {
        void OnRegisterDependencies(IWindsorContainer container);
    }
}
