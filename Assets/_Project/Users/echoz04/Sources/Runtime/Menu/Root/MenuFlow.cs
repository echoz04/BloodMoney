using Cysharp.Threading.Tasks;
using FMOD.Studio;
using FMODUnity;
using Sources.Runtime.Gameplay.Character;
using Sources.Runtime.Services.AudioPlayer;
using Sources.Runtime.Services.Builders.Character;
using Sources.Runtime.Services.Loaders.GameData;
using Sources.Runtime.Services.Loaders.Resources;
using UnityEngine;
using VContainer.Unity;

namespace Sources.Runtime.Menu.Root
{
    public class MenuFlow : IStartable
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly EventReference _backEventReference;

        private EventInstance _backEventInstance;

        private MenuFlow(IAudioPlayer audioPlayer, EventReference backEventReference)
        {
            _audioPlayer = audioPlayer;
            _backEventReference = backEventReference;
        }
        
        public void Start()
        {
            InitializebackMusic(_backEventReference);
        }

        private void InitializebackMusic(EventReference eventReference)
        {
            _backEventInstance = _audioPlayer.CreateEventInstance(eventReference);
            _backEventInstance.start();
        }
    }
}