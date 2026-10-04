using System;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{

  public TMP_Text textField;

  public void Init(string text, float time)
  {
    textField.text = text;


  }

  // void OnDestroy()
  // {
  //   OnDestroyDialog?.Invoke();
  // }
}