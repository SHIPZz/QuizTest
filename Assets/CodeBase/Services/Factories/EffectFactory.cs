using CodeBase.Enums;
using CodeBase.Gameplay.Effects;
using CodeBase.Services.StaticDatas;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Services.Factories
{
    public class EffectFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly UIStaticDataService _uiStaticDataService;

        public EffectFactory(IObjectResolver objectResolver, UIStaticDataService uiStaticDataService)
        {
            _objectResolver = objectResolver;
            _uiStaticDataService = uiStaticDataService;
        }

        public Effect Create(EffectType effectType, Transform parent, Vector3 at, Quaternion rotation)
        {
            Effect prefab = _uiStaticDataService.GetEffect(effectType);

            return _objectResolver.Instantiate(prefab, at, rotation, parent);
        }
    }
}