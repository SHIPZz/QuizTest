using System;
using System.Collections.Generic;
using CodeBase.Services.StaticDatas;
using CodeBase.Services.WorldDataSystem;
using CodeBase.StaticData.Level;
using UnityEngine;

namespace CodeBase.Services.Level
{
    public class LevelService
    {
        private readonly  IWorldDataService _worldDataService;
        private readonly UIStaticDataService _uiStaticDataService;

        private int _currentLevel = -1;

        public event Action<LevelData> LoadRequested;

        public LevelService(IWorldDataService worldDataService, UIStaticDataService uiStaticDataService)
        {
            _worldDataService = worldDataService;
            _uiStaticDataService = uiStaticDataService;
        }

        public LevelData GetLevelData(int id)
        {
            IReadOnlyList<LevelData> levels = _uiStaticDataService.GetLevelDatas();

            if (id >= levels.Count)
            {
                Debug.LogWarning($"id is more than level count");
                return levels[0];
            }
            
            return _uiStaticDataService.GetLevelDatas()[id];
        }

        public void RequestLoadNextLevel()
        {
            IReadOnlyList<LevelData> levels = _uiStaticDataService.GetLevelDatas();

            _currentLevel = Mathf.Clamp(_currentLevel++, 0, levels.Count);
            
            LevelData level = levels[_currentLevel];
            
            LoadRequested?.Invoke(level);
        }

    }
}