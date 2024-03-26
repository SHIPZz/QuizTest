using CodeBase.Gameplay.Answer;

namespace CodeBase.UI.Card
{
    public class CardAnswerController
    {
        private readonly AnswerService _answerService;

        public string CorrectAnswer => _answerService.Answer;

        public CardAnswerController(AnswerService answerService)
        {
            _answerService = answerService;
        }

        public void NotifyAnswerService(CardView cardView)
        {
            _answerService.NotifyAnswerPressed(cardView.ID);
        }
    }
}