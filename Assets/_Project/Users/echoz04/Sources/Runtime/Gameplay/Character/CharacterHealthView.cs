using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Runtime.Gameplay.Character
{
    public sealed class CharacterHealthView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        
        private CharacterHealth _health;
        private CharacterData _data;
        private readonly List<Image> _hearts = new();
        private IDisposable _subscription;

        public void Initialize(CharacterHealth health, CharacterData data)
        {
            _health = health;
            _data = data;

            CreateHearts();
            
            _subscription = _health.Current.Subscribe(OnHealthChanged);
        }

        private void CreateHearts()
        {
            for (int i = 0; i < _data.HealthCount; i++)
            {
                var instance = Instantiate(_data.HeartPrefab, _container);
                
                _hearts.Add(instance.GetComponent<Image>());
            }

            OnHealthChanged(_health.Current.Value);
        }

        private void OnHealthChanged(int currentHealth)
        {
            for (int i = 0; i < _hearts.Count; i++)
                _hearts[i].enabled = i < currentHealth;
        }

        private void OnDestroy() =>
            _subscription?.Dispose();
    }
}