using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Services.WorldDataSystem;
using CodeBase.UI.Card;
using CodeBase.UI.Grid;
using Random = UnityEngine.Random;

public class AnswerGenerator : IDisposable
{
    private readonly IWorldDataService _worldDataService;
    private readonly GridConfigurator _gridConfigurator;

    public event Action<string> AnswerCreated;

    public AnswerGenerator(IWorldDataService worldDataService, GridConfigurator gridConfigurator)
    {
        _gridConfigurator = gridConfigurator;
        _worldDataService = worldDataService;
    }

    public void Initialize(List<CardView> createdCardViews)
    {
        _gridConfigurator.Configured += CreateNewAnswer;

        GenerateNewAnswer(createdCardViews);
    }

    public void Dispose()
    {
        _gridConfigurator.Configured -= CreateNewAnswer;
    }

    private void CreateNewAnswer(List<CardView> createdCardViews)
    {
        GenerateNewAnswer(createdCardViews);
    }

    private void GenerateNewAnswer(List<CardView> createdCardViews)
    {
        var filteredCardViews =
            createdCardViews.Where(x => !_worldDataService.WorldData.UsedAnswers.Contains(x.ID)).ToList();

        CardView randomCardView = filteredCardViews[Random.Range(0, filteredCardViews.Count)];

        AnswerCreated?.Invoke(randomCardView.ID);
    }
}