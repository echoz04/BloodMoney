using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Sources.Runtime.Services.AudioPlayer
{
    public class AudioPlayer : IAudioPlayer
    {
        public void PlayOneShot(EventReference eventReference, Vector3 worldPosition)
        {
            RuntimeManager.PlayOneShot(eventReference, worldPosition);
        }

        public EventInstance CreateEventInstance(EventReference eventReference)
        {
            return RuntimeManager.CreateInstance(eventReference);   
        }
    }
}