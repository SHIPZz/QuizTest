using System;
using CodeBase.Animations;
using CodeBase.Services.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.UI.Windows
{
    [RequireComponent(typeof(CanvasAnimator))]
    public abstract class WindowBase : MonoBehaviour
    {
        [SerializeField] protected Button CloseButton;
        [SerializeField] protected CanvasAnimator CanvasAnimator;
        
        protected WindowService WindowService;

        [Inject]
        private void Construct(WindowService windowService) => 
            WindowService = windowService;

        private void Awake()
        {
            if (CloseButton != null)
                CloseButton.onClick.AddListener(Close);
        }

        public virtual void Open(Action onComplete = null)
        {
            CanvasAnimator.FadeInCanvas(onComplete);
        }

        public virtual void Close()
        {
            CanvasAnimator.FadeOutCanvas(() => Destroy(gameObject));
        }

        public virtual void Show()
        {
            
        }

        public virtual void Hide()
        {
            
        }
    }
}
