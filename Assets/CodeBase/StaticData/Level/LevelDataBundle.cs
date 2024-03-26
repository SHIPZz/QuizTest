using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(fileName = nameof(LevelDataBundle), menuName = "Game/Data/Level data bundle")]
    public class LevelDataBundle : ScriptableObject
    {
        [SerializeField] private List<LevelData> _levelDatas;

        public IReadOnlyList<LevelData> LevelDatas => _levelDatas;
    }
}