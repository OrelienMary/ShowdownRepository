using UnityEngine;

public class PMovement : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    public float speed;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Debug.Log(horizontal);
        Debug.Log(vertical);

        rb.velocity = new Vector3(horizontal, 0f, vertical).normalized * speed;
    }
}
