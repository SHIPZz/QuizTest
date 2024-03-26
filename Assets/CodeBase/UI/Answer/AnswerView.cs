using CodeBase.Animations;
using CodeBase.Gameplay.Answer;
using TMPro;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.Answer
{
    public class AnswerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _answerText;
        [SerializeField] private TweenFadeAnimation _tweenFadeAnimation;

        private AnswerService _answerService;

        [Inject]
        private void Construct(AnswerService answerService) => 
            _answerService = answerService;

        private void Start()
        {
            SetAnswerText(_answerService.Answer);
            _answerService.AnswerCreated += SetAnswerText;
        }

        private void OnDestroy() => 
            _answerService.AnswerCreated -= SetAnswerText;

        public void Show() => 
            _tweenFadeAnimation.FadeText(_answerText);

        public void Hide() => 
            _tweenFadeAnimation.UnFadeText(_answerText);

        private void SetAnswerText(string answer) => 
            _answerText.text = $"Find: {answer}";
    }
}