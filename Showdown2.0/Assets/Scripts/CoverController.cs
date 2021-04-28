using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverController : MonoBehaviour
{
    public int life = 0;

    public float size;

    private void Update()
    {
        transform.localScale = new Vector3(size, size, size);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bullet")
        {
            life -= 1;

            if (life == 0)
                Destroy(gameObject);
        }
    }
}
