using System;
using CodeBase.InfraStructure;
using CodeBase.Services.SaveSystem;
using CodeBase.Services.StaticDatas;
using CodeBase.Services.WorldDataSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Scopes
{
    public class BootScope : LifetimeScope
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            BindGameStateMachine(builder);
            BindStaticDataServices(builder);
            BindWorldDataService(builder);
            BindSaveSystem(builder);
        }

        private void Start()
        {
            _gameStateMachine.ChangeState<BootstrapState>();
        }

        private void BindStaticDataServices(IContainerBuilder builder)
        {
            builder.Register<UIStaticDataService>(Lifetime.Singleton);
        }

        private void BindWorldDataService(IContainerBuilder builder)
        {
            builder.Register<IWorldDataService, WorldDataService>(Lifetime.Singleton);
        }

        private void BindSaveSystem(IContainerBuilder builder)
        {
            builder.Register<ISaveSystem, PlayerPrefsSaveSystem>(Lifetime.Singleton);
        }

        private void BindGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<BootstrapState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<LevelLoadState>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<IStateFactory, StateFactory>(Lifetime.Singleton);
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
            builder.RegisterComponent(_loadingCurtain).AsImplementedInterfaces();
        }
    }
}