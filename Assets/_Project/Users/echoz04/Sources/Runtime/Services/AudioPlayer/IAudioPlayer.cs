using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Sources.Runtime.Services.AudioPlayer
{
    public interface IAudioPlayer
    {
        void PlayOneShot(EventReference eventReference, Vector3 worldPosition);
        
        EventInstance CreateEventInstance(EventReference eventReference);
    }
}