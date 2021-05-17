using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace NeedsVsWants.MenuSystem
{
    public class AudioTab : OptionsTab
    {
        [SerializeField]
        AudioMixer _AudioMixer;

        [SerializeField]
        Slider _MasterVolume;
        [SerializeField]
        Slider _MusicVolume;
        [SerializeField]
        Slider _SfxVolume;

        float VolumeClamp(float value) => Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20;

        protected override void Start() 
        {
            base.Start();

            _MasterVolume.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
            _MasterVolume.onValueChanged.AddListener(volume => 
            {
                PlayerPrefs.SetFloat("MasterVolume", volume);

                _AudioMixer.SetFloat("MasterVolume", VolumeClamp(volume));
            });

            _MusicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            _MusicVolume.onValueChanged.AddListener(volume => 
            {
                PlayerPrefs.SetFloat("MusicVolume", volume);

                _AudioMixer.SetFloat("MusicVolume", VolumeClamp(volume));
            });

            _SfxVolume.value = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
            _SfxVolume.onValueChanged.AddListener(volume => 
            {
                PlayerPrefs.SetFloat("SfxVolume", volume);

                _AudioMixer.SetFloat("SfxVolume", VolumeClamp(volume));
            });
        }
    }
}