using CodeBase.Gameplay.Answer;
using CodeBase.Services.Level;
using CodeBase.Services.UI;
using CodeBase.StaticData.Level;
using VContainer.Unity;

namespace CodeBase.GameInitSystem
{
    public class GameInit : IInitializable
    {
        private readonly LevelService _levelService;
        private readonly AnswerService _answerService;
        private readonly UIService _uiService;

        public GameInit(LevelService levelService, AnswerService answerService, UIService uiService)
        {
            _uiService = uiService;
            _answerService = answerService;
            _levelService = levelService;
        }

        public void Initialize()
        {
            LevelData levelData = _levelService.GetLevelData(0);
            _uiService.Init(levelData, createdCardViews => _answerService.SetAnswer(createdCardViews));
        }
    }
}