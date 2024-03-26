using System;
using CodeBase.Gameplay.Answer;
using CodeBase.Services.UI;
using CodeBase.UI.Windows.Quiz;
using UnityEngine;
using VContainer.Unity;

namespace CodeBase.UI.Windows.Restart
{
    public class RestartWindowController : IInitializable, IDisposable
    {
        private readonly WindowService _windowService;
        private readonly AnswerService _answerService;

        public RestartWindowController(WindowService windowService, AnswerService answerService)
        {
            _answerService = answerService;
            _windowService = windowService;
        }

        public void Initialize() => 
            _answerService.AllAnswered += OpenRestartWindow;

        public void Dispose() => 
            _answerService.AllAnswered -= OpenRestartWindow;

        private void OpenRestartWindow() => 
            _windowService.Open<RestartWindow>();
    }
}