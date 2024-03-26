using UnityEngine;

namespace CodeBase.StaticData.Card
{
    [CreateAssetMenu(fileName = nameof(CardData), menuName = "Game/Data/Card data")]
    public class CardData : ScriptableObject
    {
         public string Id => name;
        
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}