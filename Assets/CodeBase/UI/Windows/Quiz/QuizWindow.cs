using CodeBase.InfraStructure;
using CodeBase.Services.Factories;
using CodeBase.UI.Answer;
using CodeBase.UI.Card;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.UI.Windows.Quiz
{
    public class QuizWindow : WindowBase
    {
        [field: SerializeField] public GridLayoutGroup GridLayoutGroup { get; private set; }

        [SerializeField] private AnswerView _answerView;
        
        private ILoadingCurtain _loadingCurtain;
        private CardFactory _cardFactory;

        [Inject]
        private void Construct(ILoadingCurtain loadingCurtain, CardFactory cardFactory)
        {
            _cardFactory = cardFactory;
            _loadingCurtain = loadingCurtain;
        }

        private void Start()
        {
            _answerView.Hide();
            
            foreach (CardView cardView in _cardFactory.CreatedViews) 
                cardView.Hide();
            
            _loadingCurtain.Closed += ShowParts;
        }

        private void OnDestroy()
        {
            _loadingCurtain.Closed -= ShowParts;
        }

        private void ShowParts()
        {
            _answerView.Show();

            foreach (CardView cardView in _cardFactory.CreatedViews) 
                cardView.Show();
        }
    }
}