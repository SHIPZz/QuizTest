using System;
using System.Collections.Generic;
using CodeBase.Services.Factories;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.StaticData.Level;
using CodeBase.UI.Card;
using Object = UnityEngine.Object;

namespace CodeBase.UI.Grid
{
    public class GridConfigurator
    {
        private readonly CardFactory _cardFactory;
        private GridLayoutGroup _gridLayoutGroup;
        private List<CardView> _cardViews = new List<CardView>();

        public GridConfigurator(CardFactory cardFactory)
        {
            _cardFactory = cardFactory;
        }

        public void SetGridLayoutGroup(GridLayoutGroup gridLayoutGroup) =>
            _gridLayoutGroup = gridLayoutGroup;
        
        public void ConfigureGridForLevel(LevelData levelData, Action<List<CardView>> onConfiguredCallback = null)
        {
            ClearGrid();
            
            _gridLayoutGroup.constraintCount = levelData.Columns;

            for (int i = 0; i < levelData.Rows * levelData.Columns; i++)
            {
                CardView cardView = _cardFactory.CreateRandomCardView(_gridLayoutGroup.transform,Vector3.zero, Quaternion.identity);

                if ((i + 1) % levelData.Columns == 0)
                {
                    cardView.transform.SetAsLastSibling();
                }
                
                _cardViews.Add(cardView);
            }
            
            onConfiguredCallback?.Invoke(_cardViews);
        }

        private void ClearGrid()
        {
            foreach (CardView cardView in _cardViews)
            {
                Object.Destroy(cardView.gameObject);
            }

            _cardFactory.ClearCreatedViews();
            _cardViews.Clear();
        }
    }
}