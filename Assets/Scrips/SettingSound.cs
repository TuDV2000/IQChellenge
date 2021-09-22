using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingSound : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    private void Start()
    {
        if (slider != null)
        {
            float currentVolume;
            audioMixer.GetFloat("volume", out currentVolume);
            slider.value = currentVolume;
        }
    }
}
