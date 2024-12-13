using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    public AudioMixerGroup mixerGroup;
    public AudioSource UIClickSFX;
    public AudioClip ButtonClick;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (UIClickSFX == null)
        {
            UIClickSFX = gameObject.AddComponent<AudioSource>();
            UIClickSFX.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        UIClickSFX.PlayOneShot(clip);
    }
}

