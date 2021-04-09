using UnityEngine;
using Photon.Pun;

public class PMovement : MonoBehaviourPun
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
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontal, 0f, vertical).normalized * speed;
    }
}
