using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
  public List<UpgradeSkillConfig> upgradesConfigs;
  public WaveTimer waveTimer;
  public event Action<List<UpgradeSkillConfig>> OnShowUpgrade;
  private bool IsShowingUpgrade = false;
  public CardsController cardsController;

  private readonly float[] upgradesTime = { 10, 30, 60, 120, 180 };
  private int nextUpgradeTime = 0;

  void Start()
  {
    if (waveTimer != null)
      waveTimer.OnTimerChanged += TryShowUpgrades;

    if (cardsController != null) cardsController.OnConfirmedUpgrade += ChangeShowingUpgradeStatus;
  }


  public void RemoveUpgradeConfig(UpgradeSkillConfig config)
  {

    upgradesConfigs.Remove(config);

  }
  public void TryShowUpgrades(float time)
  {
    List<UpgradeSkillConfig> currentList = new List<UpgradeSkillConfig>();
    int lastIndex = 0;
    if (IsShowingUpgrade) return;

    if (nextUpgradeTime >= upgradesTime.Length) return;

    if (time >= upgradesTime[nextUpgradeTime])
    {
      for (int i = 0; i < 3; i++)
      {

        int newRandomIndex;
        do
        {
          newRandomIndex = UnityEngine.Random.Range(0, upgradesConfigs.Count);
        }
        while (newRandomIndex == lastIndex);



        currentList.Add(upgradesConfigs[newRandomIndex]);
        lastIndex = newRandomIndex;
      }

      nextUpgradeTime++;
      IsShowingUpgrade = true;
      Time.timeScale = 0f;
      AudioListener.pause = true;

      OnShowUpgrade?.Invoke(currentList);
    }

  }
  public void ChangeShowingUpgradeStatus()
  {
    IsShowingUpgrade = false;
    Time.timeScale = 1f;
    AudioListener.pause = false;
  }
  void OnDisable()
  {
    if (waveTimer != null)
      waveTimer.OnTimerChanged -= TryShowUpgrades;

    if (cardsController != null) cardsController.OnConfirmedUpgrade -= ChangeShowingUpgradeStatus;
  }
}