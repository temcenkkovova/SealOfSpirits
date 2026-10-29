using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
  public List<UpgradeSkillConfig> upgradeSkillConfigs;

  public WaveTimer waveTimer;
  public event Action OnShowUpgrade;
  private bool IsShowingUpgrade = false;

  void Start()
  {
    if (waveTimer != null)
      waveTimer.OnTimerChanged += TryShowUpgrades;
  }

  void Update()
  {
    Time.timeScale = IsShowingUpgrade ? 0 : 1;
  }

  public void TryShowUpgrades(float time)
  {
    if (time >= 10 && !IsShowingUpgrade)
    {
      IsShowingUpgrade = true;
      OnShowUpgrade?.Invoke();
    }
    else if (time >= 60 && !IsShowingUpgrade)
    {
      IsShowingUpgrade = true;
      OnShowUpgrade?.Invoke();
    }
    else if (time >= 120 && !IsShowingUpgrade)
    {
      IsShowingUpgrade = true;
      OnShowUpgrade?.Invoke();
    }
  }
  public void ChangeShowingUpgradeStatus()
  {
    IsShowingUpgrade = !IsShowingUpgrade;
  }
  void OnDisable()
  {
    if (waveTimer != null)
      waveTimer.OnTimerChanged -= TryShowUpgrades;
  }
}