using System;
using UnityEngine;
using VContainer;

namespace Sources.Runtime.Services.Loaders.Scene
{
    public class CurtainViewRoot : MonoBehaviour
    {
        private static readonly int ShowHash = Animator.StringToHash("Show");
        private static readonly int HideHash = Animator.StringToHash("Hide");
        
        [SerializeField] private Animator _animator;
        
        private ISceneLoader _sceneLoader;

        private void OnValidate()
        {
            _animator ??= GetComponentInChildren<Animator>();
        }

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            Hide();
            
            _sceneLoader.OnLoadingStarted += Show;
            _sceneLoader.OnLoadingEnded += Hide;
        }

        private void Show()
        {
            Debug.Log("Show CurtaionViewRoot");
            
            _animator.SetTrigger(ShowHash);
        }

        private void Hide()
        {
            Debug.Log("Hide CurtaionViewRoot");
            
            _animator.SetTrigger(HideHash);
        }

        private void OnDestroy()
        {
            _sceneLoader.OnLoadingStarted -= Show;
            _sceneLoader.OnLoadingEnded -= Hide;
        }
    }
}
