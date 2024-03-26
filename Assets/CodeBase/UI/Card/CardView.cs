using System.Collections.Generic;
using CodeBase.Animations;
using CodeBase.Enums;
using CodeBase.Services.Factories;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.UI.Card
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private string _id;
        [SerializeField] private Button _button;
        [SerializeField] private BouncingAnimation _bouncingAnimation;
        [SerializeField] private BouncingAnimation _iconBouncingAnimation;
        [SerializeField] private EffectType _effectType;
        [SerializeField] private TweenFadeAnimation _tweenFadeAnimations;
        [SerializeField] private List<Image> _cardImages;

        private CardAnswerController _cardAnswerController;
        private EffectFactory _effectFactory;

        public string ID => _id;

        [Inject]
        private void Construct(CardAnswerController cardAnswerController, EffectFactory effectFactory)
        {
            _effectFactory = effectFactory;
            _cardAnswerController = cardAnswerController;
        }

        private void Start() => 
            _button.onClick.AddListener(OnViewPressed);

        private void OnDestroy() => 
            _button.onClick.RemoveListener(OnViewPressed);

        public void Show() => 
            _tweenFadeAnimations.FadeImages(_cardImages);

        public void Hide() => 
            _cardImages.ForEach(x=>x.DOFade(0,0));

        public void Init(Sprite icon, string id)
        {
            _icon.sprite = icon;
            _id = id;
        }

        private void OnViewPressed()
        {
            if (_cardAnswerController.CorrectAnswer != _id)
            {
                _bouncingAnimation.Play();
            }
            else
            {
                _iconBouncingAnimation.Play();
                _effectFactory.Create(_effectType, null, transform.position, Quaternion.identity);
            }

            _cardAnswerController.NotifyAnswerService(this);
        }
    }
}