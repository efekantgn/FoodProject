using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    private const string PREF_KEY_MUSIC = "Music"; // PlayerPrefs anahtarı
    private const string PREF_KEY_SFX = "SFX"; // PlayerPrefs anahtarı
    public Button toggleMusic; // Unity'deki buton referansı
    public Button toggleSFX; // Unity'deki buton referansı
    public Sprite MusicOn;
    public Sprite MusicOff;
    public Sprite SFXOn;
    public Sprite SFXOff;
    private bool isOnMusic; // Durum değişkeni
    private bool isOnSFX; // Durum değişkeni


    public AudioMixer mixer;

    public bool IsOnMusic
    {
        get => isOnMusic; set
        {
            isOnMusic = value;
            mixer.SetFloat("MusicVolume", value ? 0 : -80);
        }
    }
    public bool IsOnSFX
    {
        get => isOnSFX; set
        {
            isOnSFX = value;
            mixer.SetFloat("SFXVolume", value ? 0 : -80);
        }
    }

    void Start()
    {
        // Durumu PlayerPrefs'ten al (varsayılan olarak kapalı)
        IsOnMusic = PlayerPrefs.GetInt(PREF_KEY_MUSIC, 0) == 1;
        IsOnSFX = PlayerPrefs.GetInt(PREF_KEY_SFX, 0) == 1;

        // Butona tıklama olayını dinle
        toggleMusic.onClick.AddListener(ToggleStateMusic);
        toggleSFX.onClick.AddListener(ToggleStateSFX);

        // UI güncelle
        UpdateButtonUI(toggleMusic, IsOnMusic, MusicOn, MusicOff);
        UpdateButtonUI(toggleSFX, IsOnSFX, SFXOn, SFXOff);
    }

    void ToggleStateMusic()
    {
        IsOnMusic = !IsOnMusic; // Durumu değiştir

        // Yeni durumu PlayerPrefs'e kaydet
        PlayerPrefs.SetInt(PREF_KEY_MUSIC, IsOnMusic ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Current State: " + (IsOnMusic ? "On" : "Off")); // Konsola yaz

        // UI güncelle
        UpdateButtonUI(toggleMusic, IsOnMusic, MusicOn, MusicOff);

    }
    void ToggleStateSFX()
    {
        IsOnSFX = !IsOnSFX; // Durumu değiştir

        // Yeni durumu PlayerPrefs'e kaydet
        PlayerPrefs.SetInt(PREF_KEY_SFX, IsOnSFX ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("Current State: " + (IsOnSFX ? "On" : "Off")); // Konsola yaz

        // UI güncelle
        UpdateButtonUI(toggleSFX, IsOnSFX, SFXOn, SFXOff);
    }

    void UpdateButtonUI(Button button, bool value, Sprite on, Sprite off)
    {
        // UI metni değiştir (isteğe bağlı)
        Image buttonImage = button.GetComponentInChildren<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = value ? off : on;
        }
    }
}
