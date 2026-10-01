using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
  public TMP_Text textField;

  public void Init(float amount)
  {
    textField.text = "+ " + amount.ToString();
  }

  void Start()
  {
    Destroy(gameObject, 2f);
  }
}