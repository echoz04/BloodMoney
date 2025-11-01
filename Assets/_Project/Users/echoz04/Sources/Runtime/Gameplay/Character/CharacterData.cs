using UnityEngine;
using UnityEngine.UI;

namespace Sources.Runtime.Gameplay.Character
{
    [CreateAssetMenu(menuName = "Datas/Character", fileName = "Character Data")]
    public class CharacterData : ScriptableObject
    {
        [field: SerializeField] public int HealthCount { get; private set; } = 3;
        [field: SerializeField] public GameObject HeartPrefab { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [field: SerializeField] public float JumpForce { get; private set; } = 5f;
        [field: SerializeField] public float GroundStickForce { get; private set; } = -2f;
        [field: SerializeField] public float Gravity { get; private set; } = -9.81f;
        [field: SerializeField] public float CoyoteTime { get; private set; } = 0.15f;
        [field: SerializeField] public float JumpBufferTime { get; private set; } = 0.1f;
    }
}