using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Constant;
using CodeBase.Enums;
using CodeBase.Gameplay.Effects;
using CodeBase.StaticData.Card;
using CodeBase.StaticData.Level;
using CodeBase.UI.Card;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Services.StaticDatas
{
    public class UIStaticDataService
    {
        private readonly Dictionary<string, CardData> _cardDatas;
        private readonly CardView _getCardView;
        private readonly LevelDataBundle _levelDataBundle;
        private readonly Dictionary<Type, WindowBase> _windows;
        private readonly Dictionary<EffectType, Effect> _effects;

        public UIStaticDataService()
        {
            _cardDatas = Resources.LoadAll<CardData>(AssetPath.CardData)
                .ToDictionary(x => x.Id, x => x);

            _getCardView = Resources.Load<CardView>(AssetPath.CardView);

            _levelDataBundle = Resources.Load<LevelDataBundle>(AssetPath.LevelDataBundle);

            _windows = Resources.LoadAll<WindowBase>(AssetPath.Windows)
                .ToDictionary(x => x.GetType(), x => x);

            _effects = Resources.LoadAll<Effect>(AssetPath.Effects)
                .ToDictionary(x => x.EffectType, x => x);
        }

        public Effect GetEffect(EffectType effectType) =>
            _effects[effectType];
        
        public T GetWindow<T>() where T : WindowBase =>
            (T)_windows[typeof(T)];

        public IReadOnlyList<LevelData> GetLevelDatas()
        {
            return _levelDataBundle.LevelDatas;
        }

        public CardData GetCardData(string id) =>
            _cardDatas[id];

        public IEnumerable<CardData> GetCardDatas() =>
            _cardDatas.Values;

        public CardView GetCardView() => _getCardView;
    }
}