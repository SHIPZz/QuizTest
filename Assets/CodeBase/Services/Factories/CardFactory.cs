using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.StaticDatas;
using CodeBase.Services.WorldDataSystem;
using CodeBase.StaticData.Card;
using CodeBase.UI.Card;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Services.Factories
{
    public class CardFactory
    {
        private readonly UIStaticDataService _uiStaticDataService;
        private readonly IObjectResolver _objectResolver;
        private readonly IWorldDataService _worldDataService;

        private HashSet<string> _usedCardIds = new HashSet<string>();
        private List<CardData> _availableCardDatas;
        private List<CardView> _createdViews = new List<CardView>();

        public IReadOnlyList<CardView> CreatedViews => _createdViews;

        public CardFactory(UIStaticDataService uiStaticDataService, IObjectResolver objectResolver,
            IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
            _uiStaticDataService = uiStaticDataService;
            _objectResolver = objectResolver;

            _availableCardDatas = _uiStaticDataService.GetCardDatas().ToList();
        }

        public CardView CreateRandomCardView(Transform parent, Vector3 at, Quaternion rotation)
        {
            FilterAvailableCards();

            CardView prefab = _uiStaticDataService.GetCardView();

            var availableCards = _availableCardDatas.Where(card => !_usedCardIds.Contains(card.Id)).ToList();
            
            if (availableCards.Count == 0)
            {
                Debug.LogError("No available cards left.");
                return null;
            }

            int randomIndex = Random.Range(0, availableCards.Count);
            CardData randomCardData = availableCards[randomIndex];

            CardView createdCardView = _objectResolver.Instantiate(prefab, at, rotation, parent);
            createdCardView.Init(randomCardData.Sprite, randomCardData.Id);
            _createdViews.Add(createdCardView);
            _usedCardIds.Add(randomCardData.Id);
            return createdCardView;
        }

        private void FilterAvailableCards()
        {
            foreach (string usedCardId in _worldDataService.WorldData.UsedAnswers)
            {
                _usedCardIds.Add(usedCardId);
            }
        }

        public void ClearCreatedViews()
        {
            _createdViews.Clear();
        }
    }
}
