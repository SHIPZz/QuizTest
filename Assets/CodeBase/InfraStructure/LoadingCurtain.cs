using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.InfraStructure
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private float _closeDuration = 1f;
        [SerializeField] private float _initialValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private TMP_Text _loadingText;

        private Canvas _canvas;

        public event Action Closed;

        private void Awake()
        {
            _canvas = _canvasGroup.GetComponent<Canvas>();
            _loadingSlider.value = 0;
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            _loadingSlider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _loadingSlider.value = 0;
            _loadingSlider.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void Show(float loadSliderDuration)
        {
            _loadingSlider.value = _initialValue;
            _canvas.enabled = true;
            _loadingSlider.DOValue(_maxValue, loadSliderDuration).SetUpdate(true);
            _canvasGroup.alpha = 1;
        }

        public async Task Hide()
        {
            while (Mathf.Approximately(_loadingSlider.value, _maxValue) == false)
                await Task.Yield();

            _canvasGroup
                .DOFade(0, _closeDuration).SetUpdate(true)
                .OnComplete(() =>
                {
                    _canvas.enabled = false;
                    print("closed");
                    Closed?.Invoke();
                });
        }

        private void OnValueChanged(float value)
        {
            _loadingText.text = $"Loading...{value}%";
        }
    }
}