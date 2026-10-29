using System;
using System.Collections.Generic;
using UnityEngine;

public class CardsController : MonoBehaviour
{
  public List<UpgradeCardUI> upgradeCardsUI = new List<UpgradeCardUI>();
  private ActiveCard currentActiveCard;
  private UpgradeSkillConfig currentActiveCardUpgradeConfig;
  public event Action<bool> OnCurrentActiveCard;

  public event Action OnConfirmedUpgrade;
  public UpgradeManager upgradeManager;
  public void AddNewCards(UpgradeCardUI newCard)
  {
    if (!upgradeCardsUI.Contains(newCard))
      upgradeCardsUI.Add(newCard);
    ActiveCard activeCard = newCard.GetComponent<ActiveCard>();
    activeCard.OnClickCard += HandleClickOnCard;

  }

  public void RemoveCardsList()
  {
    if (upgradeCardsUI.Count > 0)
      upgradeCardsUI.RemoveRange(0, upgradeCardsUI.Count);

    currentActiveCard = null;
    OnCurrentActiveCard?.Invoke(false);
  }
  public void HandleClickOnCard(ActiveCard activeCard)
  {
    currentActiveCard = activeCard;
    OnCurrentActiveCard?.Invoke(true);
    UpgradeCardUI upgradeCardUI = currentActiveCard.GetComponent<UpgradeCardUI>();
    currentActiveCardUpgradeConfig = upgradeCardUI.config;
    foreach (UpgradeCardUI card in upgradeCardsUI)
    {
      ActiveCard active = card.GetComponent<ActiveCard>();
      if (activeCard == active)
      {
        active.SetActiveStatus(true);
      }
      else
      {
        active.SetActiveStatus(false);
      }
    }
  }

  void OnDisable()
  {
    if (upgradeCardsUI != null)
    {
      foreach (UpgradeCardUI card in upgradeCardsUI)
      {
        if (card != null)
        {
          ActiveCard activeCard = card.GetComponent<ActiveCard>();

          activeCard.OnClickCard -= HandleClickOnCard;
        }

      }
    }
  }

  public void HandleConfirmTheCard()
  {
    if (currentActiveCard != null && currentActiveCard.isActiveAndEnabled)
    {
      upgradeManager.AddSkillUpgrade(currentActiveCardUpgradeConfig);
      OnConfirmedUpgrade?.Invoke();
    }
  }
}