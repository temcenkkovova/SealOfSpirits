using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
  [SerializeField] private AudioSettingsController settings;

  [SerializeField] private Slider masterSlider;
  [SerializeField] private Slider musicSlider;
  [SerializeField] private Slider sfxSlider;

  private void OnEnable()
  {
    masterSlider.SetValueWithoutNotify(settings.MasterVolume);
    musicSlider.SetValueWithoutNotify(settings.MusicVolume);
    sfxSlider.SetValueWithoutNotify(settings.SfxVolume);

    masterSlider.onValueChanged.AddListener(settings.SetMasterVolume);
    musicSlider.onValueChanged.AddListener(settings.SetMusicVolume);
    sfxSlider.onValueChanged.AddListener(settings.SetSfxVolume);
  }

  private void OnDisable()
  {
    masterSlider.onValueChanged.RemoveListener(settings.SetMasterVolume);
    musicSlider.onValueChanged.RemoveListener(settings.SetMusicVolume);
    sfxSlider.onValueChanged.RemoveListener(settings.SetSfxVolume);

    settings.SaveSettings();
  }
}