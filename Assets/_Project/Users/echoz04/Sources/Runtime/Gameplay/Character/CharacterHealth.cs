using System;
using R3;
using UnityEngine.SceneManagement;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterHealth
    {
        public ReactiveProperty<int> Current { get; private set; }

        private readonly CharacterData _data;
    
        public CharacterHealth(CharacterData data)
        {
            _data = data;
            
            Current = new ReactiveProperty<int>(_data.HealthCount);
        }
        
        public void ApplyDamage(int amount)
        {
            Current.Value = Math.Max(0, Current.Value - amount);
            
            if(Current.Value <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
