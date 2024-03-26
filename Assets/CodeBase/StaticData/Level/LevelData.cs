using UnityEngine;

namespace CodeBase.StaticData.Level
{
    [CreateAssetMenu(fileName = nameof(LevelData), menuName = "Game/Data/Level data")]
    public class LevelData : ScriptableObject
    {
        [field: SerializeField] public int Rows { get; private set; }
        
        [field: SerializeField] public int Columns { get; private set; }
    }
}