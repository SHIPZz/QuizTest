using CodeBase.InfraStructure;
using VContainer;

namespace CodeBase.UI.Windows.Restart
{
    public class RestartWindow : WindowBase
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public override void Close()
        {
            _gameStateMachine.ChangeState<BootstrapState>();
            base.Close();
        }
    }
}