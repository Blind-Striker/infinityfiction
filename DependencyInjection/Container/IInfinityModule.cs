using System.ComponentModel.Composition;

using Castle.Windsor;

namespace CodeFiction.InfinityFiction.Core.BootstrapperLib
{
    [InheritedExport]
    public interface IInfinityModule
    {
        void OnRegisterDependencies(IWindsorContainer container);
    }
}
