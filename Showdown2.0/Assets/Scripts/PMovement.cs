using UnityEngine;

public class PMovement : MonoBehaviour
{
    public PlayerManager playerManager;

    public float speed;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(playerManager.playerInput.horizontal, 0f, playerManager.playerInput.vertical).normalized * speed;
    }
}
