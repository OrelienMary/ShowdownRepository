using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class BulletController : MonoBehaviourPun, IPunObservable
{
    public Rigidbody rb;

    public Vector3 direction;

    public float lifeTime;
    public float baseVelocity;
    public AnimationCurve velocityMultiplierOverLifeTime;

    public PhotonTransformView ptv;
    public PhotonRigidbodyView ptr;

    float originalLifeTime;

    private void Start()
    {
        originalLifeTime = lifeTime;

        //StartCoroutine(StreamPosition(0.1f));
    }

    IEnumerator StreamPosition(float seconds)
    {
        ptv.enabled = true;
        ptr.enabled = false;

        yield return new WaitForSeconds(seconds);

        ptv.enabled = false;
        ptr.enabled = true;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * baseVelocity * velocityMultiplierOverLifeTime.Evaluate(originalLifeTime - lifeTime);

        lifeTime -= Time.fixedDeltaTime;

        if(lifeTime < 0)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("DoDamage");

            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
                //gameObject.SetActive(false);
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(lifeTime);
            stream.SendNext(originalLifeTime);
        }
        else
        {
            // Network player, receive data
            lifeTime = (float)stream.ReceiveNext();
            originalLifeTime = (float)stream.ReceiveNext();
        }
    }
}
