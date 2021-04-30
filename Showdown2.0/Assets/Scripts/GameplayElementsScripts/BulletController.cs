using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rb;

    public Vector3 direction;

    public float lifeTime;
    public float baseVelocity;
    public AnimationCurve velocityMultiplierOverLifeTime;
    public float size;

    float originalLifeTime;

    

    private void Start()
    {
        originalLifeTime = lifeTime;

        transform.localScale = new Vector3(size, size, size);

        //StartCoroutine(StreamPosition(0.1f));
    }

    private void FixedUpdate()
    {
        transform.localScale = new Vector3(size, size, size);

        rb.velocity = direction * baseVelocity * velocityMultiplierOverLifeTime.Evaluate(originalLifeTime - lifeTime);

        lifeTime -= Time.fixedDeltaTime;

        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerManager pm = other.GetComponent<PlayerManager>();

            pm.TakeDamage();    

            Destroy(gameObject);
        }

        if (other.tag == "Cover")
        {
            Destroy(gameObject);
        }
    }

}
