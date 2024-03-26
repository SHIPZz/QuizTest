using DG.Tweening;
using UnityEngine;

namespace CodeBase.Animations
{
    public class BouncingAnimation : MonoBehaviour
    {
        [SerializeField] private float _shiftDistance = 0.5f;  
        [SerializeField] private float _shiftDuration = 0.3f; 
        [SerializeField] private Ease _ease = Ease.InBounce;

        private Tween _tween;

        public void Play()
        {
            _tween?.Kill(true);
        
            Sequence sequence = DOTween.Sequence();
        
            sequence.Append(transform.DOMoveX(transform.position.x - _shiftDistance, _shiftDuration)
                    .SetEase(_ease))
                .Append(transform.DOMoveX(transform.position.x + _shiftDistance, _shiftDuration)
                    .SetEase(_ease))
                .Append(transform.DOMoveX(transform.position.x, _shiftDuration) 
                    .SetEase(_ease));
        
            _tween = sequence;
        }
    }
}