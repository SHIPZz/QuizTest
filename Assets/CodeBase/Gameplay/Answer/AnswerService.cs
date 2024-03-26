using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.WorldDataSystem;
using CodeBase.UI.Card;
using UnityEngine;

namespace CodeBase.Gameplay.Answer
{
    public class AnswerService
    {
        private readonly IWorldDataService _worldDataService;

        public string Answer { get; private set; }

        public event Action<string> AnswerCreated;
        public event Action<bool> IsCorrectedUserAnswer;
        public event Action AllAnswered;

        public AnswerService(IWorldDataService worldDataService)
        {
            _worldDataService = worldDataService;
        }

        public void SetAnswer(List<CardView> createdCardViews)
        {
            GenerateNewAnswer(createdCardViews);
        }

        public void NotifyAnswerPressed(string answer)
        {
            if (answer == Answer)
            {
                _worldDataService.WorldData.UsedAnswers.Add(answer);
                IsCorrectedUserAnswer?.Invoke(true);
                return;
            }

            IsCorrectedUserAnswer?.Invoke(false);
        }

        public void NotifyAllAnswered()
        {
            AllAnswered?.Invoke();
        }

        private void GenerateNewAnswer(List<CardView> createdCardViews)
        {
            var filteredCardViews =
                createdCardViews.Where(x => !_worldDataService.WorldData.UsedAnswers.Contains(x.ID)).ToList();

            if (filteredCardViews.Count == 0)
            {
                AllAnswered?.Invoke();
                Debug.LogWarning("there are no answers");
                return;
            }

            CardView randomCardView = filteredCardViews[UnityEngine.Random.Range(0, filteredCardViews.Count)];

            Answer = randomCardView.ID;
            AnswerCreated?.Invoke(randomCardView.ID);
        }
    }
}