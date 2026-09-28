using UnityEngine;

public class PlayerBootstrap : MonoBehaviour
{
    public float moveSpeed;
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Start()
    {
        playerMovement.Init(moveSpeed);
    }


}
