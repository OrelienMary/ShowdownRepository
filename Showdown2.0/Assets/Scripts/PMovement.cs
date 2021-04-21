using UnityEngine;

public class PMovement : MonoBehaviour
{
    public PlayerManager playerManager;

    public float speed;
    [HideInInspector] public float speedMultiplier;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerManager.stopped)
        {
            speedMultiplier = 0f;

        }
        else if(playerManager.slowed)
        {
            speedMultiplier = 0.5f;
        }
        else
        {
            speedMultiplier = 1f;
        }
        

        rb.velocity = new Vector3(playerManager.playerInput.horizontal, 0f, playerManager.playerInput.vertical).normalized * speed * speedMultiplier;
    }
}
