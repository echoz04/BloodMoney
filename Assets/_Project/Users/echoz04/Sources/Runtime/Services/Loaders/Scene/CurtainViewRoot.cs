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
        [SerializeField] private CanvasGroup _canvasGroup;
        
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

        public void Show()
        {
            _animator.SetTrigger(ShowHash);
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _animator.SetTrigger(HideHash);
            _canvasGroup.alpha = 0;
        }

        private void OnDestroy()
        {
            _sceneLoader.OnLoadingStarted -= Show;
            _sceneLoader.OnLoadingEnded -= Hide;
        }
    }
}
