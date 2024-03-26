using CodeBase.GameInitSystem;
using CodeBase.Gameplay.Answer;
using CodeBase.Gameplay.Level;
using CodeBase.Services.Factories;
using CodeBase.Services.Level;
using CodeBase.Services.Providers;
using CodeBase.Services.UI;
using CodeBase.UI.Card;
using CodeBase.UI.Grid;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Scopes
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private LocationProvider _locationProvider;
        [SerializeField] private CameraProvider _cameraProvider;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            BindCardFactory(builder);
            BindLevelService(builder);
            BindGridConfigurator(builder);
            BindGameInit(builder);
            BindAnswerGenerator(builder);
            BindLevelLoader(builder);
            BindCardAnswerHandler(builder);
            BindLocationProvider(builder);
            BindEffectFactory(builder);
            BindUIServices(builder);
            BindCameraProvider(builder);
        }

        private void BindCameraProvider(IContainerBuilder builder)
        {
            builder.RegisterComponent(_cameraProvider);
        }

        private void BindUIServices(IContainerBuilder builder)
        {
            builder.Register<WindowService>(Lifetime.Singleton);
            builder.Register<UIFactory>(Lifetime.Singleton);
            builder.Register<UIService>(Lifetime.Singleton);
        }

        private void BindEffectFactory(IContainerBuilder builder)
        {
            builder.Register<EffectFactory>(Lifetime.Singleton);
        }
        
        private void BindLocationProvider(IContainerBuilder builder)
        {
            builder.RegisterComponent(_locationProvider);
        }

        private void BindCardAnswerHandler(IContainerBuilder builder)
        {
            builder.Register<CardAnswerController>(Lifetime.Singleton).AsSelf();
        }

        private void BindLevelLoader(IContainerBuilder builder)
        {
            builder.Register<LevelLoader>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        private void BindAnswerGenerator(IContainerBuilder builder)
        {
            builder.Register<AnswerService>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
        }

        private void BindGameInit(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameInit>();
        }

        private void BindGridConfigurator(IContainerBuilder builder)
        {
            builder.Register<GridConfigurator>(Lifetime.Singleton);
        }

        private void BindLevelService(IContainerBuilder builder)
        {
            builder.Register<LevelService>(Lifetime.Singleton);
        }

        private void BindCardFactory(IContainerBuilder builder)
        {
            builder.Register<CardFactory>(Lifetime.Singleton);
        }
    }
}