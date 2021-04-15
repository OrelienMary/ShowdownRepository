using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttaqueTest : MonoBehaviour
{
    public PlayerInput playerInput;

    public GameObject prefabBullet;

    public AnimationCurve velocityOverLifetime;

    public Collider col;

    public int inputIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.competencesInputs[inputIndex])
        {
           Attaque();
        }

        playerInput.competencesInputs[inputIndex] = false;
    }

    public void Attaque()
    {
        Vector3 attaqueDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;

        attaqueDirection = new Vector3(attaqueDirection.x, 0f, attaqueDirection.y);

        BulletController bc = Instantiate(prefabBullet, transform.position, Quaternion.identity).GetComponent<BulletController>();

        bc.gameObject.SetActive(true);

        bc.transform.position = transform.position + (attaqueDirection * 1.5f);

        bc.direction = attaqueDirection;

        bc.velocityMultiplierOverLifeTime = velocityOverLifetime;

        bc.baseVelocity = 50f;

        bc.lifeTime = 1f;

        bc.StartCoroutine(IgnoreCollisionsFor(bc.GetComponent<Collider>(), 0.2f));

    }

    public IEnumerator IgnoreCollisionsFor(Collider c ,float seconds)
    {
        Physics.IgnoreCollision(col, c, true);

        yield return new WaitForSeconds(seconds);

        Physics.IgnoreCollision(col, c, false);
    }

}
