using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    [CreateAssetMenu(menuName = "Datas/Character", fileName = "Character Data")]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [field: SerializeField] public float JumpForce { get; private set; } = 5f;
        [field: SerializeField] public float GroundStickForce { get; private set; } = -2f;
        [field: SerializeField] public float Gravity { get; private set; } = -9.81f;
    }
}