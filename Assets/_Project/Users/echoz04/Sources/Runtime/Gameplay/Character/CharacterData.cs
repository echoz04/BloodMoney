using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    [CreateAssetMenu(menuName = "Datas/Character", fileName = "Character Data")]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
    }
}