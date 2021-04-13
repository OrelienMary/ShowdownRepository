using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AttaqueTest : MonoBehaviourPun, IPunObservable
{
    public GameObject prefabBullet;

    public AnimationCurve velocityOverLifetime;

    public Collider col;

    public List<BulletController> bcs = new List<BulletController>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            //BulletController bc = PhotonNetwork.Instantiate(prefabBullet.name, transform.position, Quaternion.identity, 0).GetComponent<BulletController>();

            //bcs.Add( bc );
            //bcs[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {

           Attaque();
        }
    }

    public void Attaque()
    {
        Vector3 attaqueDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;

        attaqueDirection = new Vector3(attaqueDirection.x, 0f, attaqueDirection.y);

        BulletController bc = PhotonNetwork.Instantiate(prefabBullet.name, transform.position, Quaternion.identity, 0).GetComponent<BulletController>();

        bc.gameObject.SetActive(true);

        bc.transform.position = transform.position + (attaqueDirection * 1.5f);

        bc.direction = -attaqueDirection;

        bc.velocityMultiplierOverLifeTime = velocityOverLifetime;

        bc.baseVelocity = 50f;

        bc.lifeTime = 1f;

        //bc.StartCoroutine(IgnoreCollisionsFor(bc.GetComponent<Collider>(), 0.2f));

    }

    public IEnumerator IgnoreCollisionsFor(Collider c ,float seconds)
    {
        Physics.IgnoreCollision(col, c, true);

        yield return new WaitForSeconds(seconds);

        Physics.IgnoreCollision(col, c, false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            for(int i = 0; i < bcs.Count; i++)
            {
                stream.SendNext(bcs[i].photonView.InstantiationId);
                stream.SendNext(bcs[i].gameObject.activeSelf);
            }
        }
        else
        {
            int originalBcsCount = bcs.Count;

            bcs.Clear();

            for(int i = 0; i < originalBcsCount; i++)
            {
                GameObject go = PhotonView.Find((int)stream.ReceiveNext()).gameObject;
                bcs.Add(go.GetComponent<BulletController>());
                bcs[i].gameObject.SetActive((bool)stream.ReceiveNext());
            }
        }
    }
}
