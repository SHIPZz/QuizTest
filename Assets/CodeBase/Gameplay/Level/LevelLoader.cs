using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Answer;
using CodeBase.Services.StaticDatas;
using CodeBase.StaticData.Level;
using CodeBase.UI.Grid;
using UnityEngine;
using VContainer.Unity;

namespace CodeBase.Gameplay.Level
{
    public class LevelLoader : IInitializable, IDisposable
    {
        private readonly GridConfigurator _gridConfigurator;
        private readonly AnswerService _answerService;
        private readonly UIStaticDataService _uiStaticDataService;

        private int _currentLevelId;

        public LevelLoader(GridConfigurator gridConfigurator,
            AnswerService answerService,
            UIStaticDataService uiStaticDataService)
        {
            _uiStaticDataService = uiStaticDataService;
            _answerService = answerService;
            _gridConfigurator = gridConfigurator;
        }

        public void Initialize() =>
            _answerService.IsCorrectedUserAnswer += OnUserAnswerGot;

        public void Dispose() =>
            _answerService.IsCorrectedUserAnswer -= OnUserAnswerGot;

        private void OnUserAnswerGot(bool isAnswerCorrected)
        {
            if (!isAnswerCorrected)
                return;

            IReadOnlyList<LevelData> levelDatas = _uiStaticDataService.GetLevelDatas();

            _currentLevelId++;

            if (_currentLevelId >= levelDatas.Count)
            {
                Debug.LogWarning("Reached the end of level data list.");
                _answerService.NotifyAllAnswered();
                return;
            }

            LevelData nextLevel = levelDatas[_currentLevelId];
            _gridConfigurator.ConfigureGridForLevel(nextLevel, _answerService.SetAnswer);
        }
    }
}