using UnityEngine;

public class PMovement : MonoBehaviour
{
    public PlayerInput playerInput;

    public float speed;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(playerInput.horizontal, 0f, playerInput.vertical).normalized * speed;
    }
}
