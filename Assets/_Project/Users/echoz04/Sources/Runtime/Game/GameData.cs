using Sources.Runtime.Gameplay.Character;
using UnityEngine;

namespace Sources.Runtime.Game
{
    [CreateAssetMenu(menuName = "Datas/Game", fileName = "Game Data")]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public CharacterData CharacterData { get; private set; }
    }
}