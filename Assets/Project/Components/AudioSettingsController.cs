using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsController : MonoBehaviour
{
  [SerializeField] private AudioMixer mixer;

  public float MasterVolume { get; private set; }
  public float MusicVolume { get; private set; }
  public float SfxVolume { get; private set; }

  private void Awake()
  {
    MasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
    MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
    SfxVolume = PlayerPrefs.GetFloat("SfxVolume", 1f);
  }

  private void Start()
  {
    ApplyVolume("MasterVolume", MasterVolume);
    ApplyVolume("MusicVolume", MusicVolume);
    ApplyVolume("SfxVolume", SfxVolume);
  }

  public void SetMasterVolume(float value)
  {
    MasterVolume = Mathf.Clamp01(value);
    ApplyVolume("MasterVolume", MasterVolume);
    PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
  }

  public void SetMusicVolume(float value)
  {
    MusicVolume = Mathf.Clamp01(value);
    ApplyVolume("MusicVolume", MusicVolume);
    PlayerPrefs.SetFloat("MusicVolume", MusicVolume);
  }

  public void SetSfxVolume(float value)
  {
    SfxVolume = Mathf.Clamp01(value);
    ApplyVolume("SfxVolume", SfxVolume);
    PlayerPrefs.SetFloat("SfxVolume", SfxVolume);
  }

  private void ApplyVolume(string parameter, float value)
  {
    float decibels = value <= 0f
        ? -80f
        : Mathf.Log10(value) * 20f;

    mixer.SetFloat(parameter, decibels);
  }

  public void SaveSettings()
  {
    PlayerPrefs.Save();
  }
}