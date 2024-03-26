using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace CodeBase.InfraStructure
{
    public class LevelLoadState : IState, IEnter
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly LifetimeScope _lifetimeScope;

        public LevelLoadState(ILoadingCurtain loadingCurtain, LifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _loadingCurtain = loadingCurtain;
        }

        public async void Enter()
        {
            _loadingCurtain.Show(1.5f);

            using (LifetimeScope.EnqueueParent(_lifetimeScope))
            {
                AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);

                while (asyncOperation.isDone == false)
                    await Task.Yield();

                _loadingCurtain.Hide();
            }
        }
    }
}