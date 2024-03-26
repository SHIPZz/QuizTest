using CodeBase.UI.Windows.Restart;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Scopes
{
    public class UIScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            BindRestartWindowController(builder);
        }

        private void BindRestartWindowController(IContainerBuilder builder)
        {
            builder.Register<RestartWindowController>(Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
        }
    }
}