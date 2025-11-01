using FMODUnity;
using Sources.Runtime.Gameplay.Character;
using UnityEngine;

namespace Sources.Runtime.Game
{
    [CreateAssetMenu(menuName = "Datas/GameEventReferences", fileName = "Game Event References")]
    public class GameEventReferences : ScriptableObject
    {
        [field: SerializeField] public EventReference CharacterFootsteps { get; private set; }
        [field: SerializeField] public EventReference CharacterJump { get; private set; }
    }
}