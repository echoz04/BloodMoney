using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public class CharacterRoot : MonoBehaviour
    {
        private CharacterData _data;
        private CharacterMovement _movement;
        
        public void Initialize(CharacterData data)
        {
            _movement = new CharacterMovement(data);
        }
    }
}