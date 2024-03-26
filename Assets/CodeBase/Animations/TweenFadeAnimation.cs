using System;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Animations
{
    public class TweenFadeAnimation : MonoBehaviour
    {
        [SerializeField] private float _targetAlpha = 1;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Ease _ease = Ease.InBounce;

        private Tween _fadeTween;

        public void UnFadeText(TMP_Text text, float duration = 0)
        {
            Fade(text, 0, duration);
        }

        public void FadeText(TMP_Text text, [CanBeNull] Action onCompleted = null)
        {
            Fade(text, _targetAlpha, _duration, onCompleted);
        }

        public void FadeImages(List<Image> images, [CanBeNull] Action onCompleted = null)
        {
            _fadeTween?.Kill(true);

            foreach (Image image in images)
            {
                _fadeTween = image.DOFade(_targetAlpha, _duration)
                    .OnComplete(() => onCompleted?.Invoke())
                    .SetEase(_ease)
                    .SetUpdate(true);
            }
        }

        private void Fade(TMP_Text text, float value, float duration, Action onCompleted = null)
        {
            _fadeTween?.Kill(true);

            _fadeTween = text.DOFade(value, duration)
                .OnComplete(() => onCompleted?.Invoke())
                .SetEase(_ease);
        }
    }
}