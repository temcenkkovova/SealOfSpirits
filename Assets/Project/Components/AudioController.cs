using UnityEngine;

public class AudioController : MonoBehaviour
{
  private AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    audioSource.ignoreListenerPause = true;
  }

}