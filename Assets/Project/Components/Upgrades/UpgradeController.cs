using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
  public List<UpgradeSkillConfig> upgradeSkillConfigs;

  public WaveTimer waveTimer;
  public event Action OnShowUpgrade;
  private bool IsShowingUpgrade = false;
  public CardsController cardsController;

  private readonly float[] upgradesTime = { 10, 60, 120, 180 };
  private int nextUpgradeTime = 0;

  void Start()
  {
    if (waveTimer != null)
      waveTimer.OnTimerChanged += TryShowUpgrades;

    if (cardsController != null) cardsController.OnConfirmedUpgrade += ChangeShowingUpgradeStatus;
  }



  public void TryShowUpgrades(float time)
  {
    if (IsShowingUpgrade) return;

    if (nextUpgradeTime >= upgradesTime.Length) return;

    if (time >= upgradesTime[nextUpgradeTime])
    {
      nextUpgradeTime++;
      IsShowingUpgrade = true;
      Time.timeScale = 0f;
      OnShowUpgrade?.Invoke();
    }

  }
  public void ChangeShowingUpgradeStatus()
  {
    IsShowingUpgrade = false;
    Time.timeScale = 1f;
  }
  void OnDisable()
  {
    if (waveTimer != null)
      waveTimer.OnTimerChanged -= TryShowUpgrades;

    if (cardsController != null) cardsController.OnConfirmedUpgrade -= ChangeShowingUpgradeStatus;
  }
}