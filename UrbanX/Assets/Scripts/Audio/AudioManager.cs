using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    
    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume - 80f);
    }    
    
    public void SetMusicVolume(float volume)
    {
        masterMixer.SetFloat("MusicVolume", volume - 80f);
    }    
    
    public void SetSfxVolume(float volume)
    {
        masterMixer.SetFloat("SfxVolume", volume - 80f);
    }
}
