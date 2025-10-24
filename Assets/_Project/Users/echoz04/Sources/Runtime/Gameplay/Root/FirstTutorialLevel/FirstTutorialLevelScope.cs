using Sources.Runtime.Gameplay.Tutorial;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Root.FirstTutorialLevel
{
    public class FirstTutorialLevelScope : LifetimeScope
    {
        [SerializeField] private GameObject _moveTextPopup;
        [SerializeField] private GameObject _jumpTextPopup;
        [SerializeField] private Animator _doorAnimator;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterTutorials(builder);
            RegisterEntryPoint(builder);
        }
        
        private void RegisterTutorials(IContainerBuilder builder)
        {
            builder.Register<MoveTutorial>(Lifetime.Singleton)
                .WithParameter(_moveTextPopup)
                .WithParameter(_doorAnimator)
                .As<ITutorial>()
                .AsSelf()
                .As<ITickable>();
            
            builder.Register<JumpTutorial>(Lifetime.Singleton)
                .WithParameter(_jumpTextPopup)
                .AsSelf();
        }
        
        private void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<FirstTutorialLevelFlow>();
        }
    }
}
