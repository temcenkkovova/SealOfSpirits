using UnityEngine;

public class IdolBootstrap : MonoBehaviour, IInteractable
{

  public IdolConfig idolConfig;
  public void Interact()
  {
    Debug.Log("Hi , I`m + " + gameObject.name);
  }
}