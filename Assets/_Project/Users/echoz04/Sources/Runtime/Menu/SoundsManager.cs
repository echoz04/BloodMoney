using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Sources.Runtime.Menu
{
    public class SoundsManager : MonoBehaviour
    {
        private const string MasterVolumeKey = "MasterVolume";
        private const string MusicVolumeKey = "MusicVolume";
        private const string SfxVolumeKey = "SfxVolume";
        
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private AudioMixer _masterMixer;

        private void Awake()
        {
            LoadVolumeSettings();

            if(_masterSlider == true) _masterSlider.onValueChanged.AddListener(SetMasterVolume);
            if(_musicSlider == true) _musicSlider.onValueChanged.AddListener(SetMusicVolume);
            if(_sfxSlider == true) _sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnDestroy()
        {
            if(_masterSlider == true) _masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
            if(_musicSlider == true) _musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
            if(_sfxSlider == true) _sfxSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private void LoadVolumeSettings()
        {
            float master = PlayerPrefs.GetFloat(MasterVolumeKey, 0.8f);
            float music = PlayerPrefs.GetFloat(MusicVolumeKey, 0.8f);
            float sfx = PlayerPrefs.GetFloat(SfxVolumeKey, 0.8f);

            if(_masterSlider) _masterSlider.value = master;
            if(_musicSlider) _musicSlider.value = music;
            if(_sfxSlider) _sfxSlider.value = sfx;

            SetMasterVolume(master);
            SetMusicVolume(music);
            SetSfxVolume(sfx);
        }

        private void SetMasterVolume(float value)
        {
            SetVolume("master", value, MasterVolumeKey);
        }

        private void SetMusicVolume(float value)
        {
            SetVolume("music", value, MusicVolumeKey);
        }

        private void SetSfxVolume(float value)
        {
            SetVolume("sfx", value, SfxVolumeKey);
        }

        private void SetVolume(string mixerParameter, float value, string saveKey)
        {
            float dB = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;
            _masterMixer.SetFloat(mixerParameter, dB);
            PlayerPrefs.SetFloat(saveKey, value);
        }
    }
}
