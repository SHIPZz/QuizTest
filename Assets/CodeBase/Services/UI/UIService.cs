using System;
using System.Collections.Generic;
using CodeBase.StaticData.Level;
using CodeBase.UI.Card;
using CodeBase.UI.Grid;
using CodeBase.UI.Windows.Quiz;

namespace CodeBase.Services.UI
{
    public class UIService
    {
        private readonly WindowService _windowService;
        private readonly GridConfigurator _gridConfigurator;

        public UIService(WindowService windowService, GridConfigurator gridConfigurator)
        {
            _windowService = windowService;
            _gridConfigurator = gridConfigurator;
        }

        public void Init(LevelData levelData, Action<List<CardView>> onCardViewsInitialized)
        {
            var quizWindow = _windowService.Get<QuizWindow>();
            _gridConfigurator.SetGridLayoutGroup(quizWindow.GridLayoutGroup);
            _gridConfigurator.ConfigureGridForLevel(levelData, onCardViewsInitialized);
            _windowService.OpenCurrentWindow();
        }
    }
}