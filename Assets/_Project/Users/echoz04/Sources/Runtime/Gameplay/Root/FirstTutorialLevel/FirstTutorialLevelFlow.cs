using Sources.Runtime.Gameplay.Tutorial;
using VContainer.Unity;

namespace Sources.Runtime.Gameplay.Root.FirstTutorialLevel
{
    public class FirstTutorialLevelFlow : IStartable
    {
        private readonly MoveTutorial _moveTutorial;

        public FirstTutorialLevelFlow(MoveTutorial moveTutorial)
        {
            _moveTutorial = moveTutorial;
        }
        
        public void Start()
        {
            _moveTutorial.Show();
        }
    }
}