using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletAttack")]
public class BulletAttack : Competence
{
    public float baseSpeed;
    public AnimationCurve speedOverLifetime;

    public float size;

    public BulletAttack bulletToInstantiate;

    public GameObject bulletPrefab;

    public override IEnumerator DoCompetence()
    {
        Vector3 attaqueDirection = playerManager.playerInput.aimDirection;

        attaqueDirection = new Vector3(attaqueDirection.x, 0f, attaqueDirection.y);

        BulletController bc =  Instantiate(bulletPrefab, playerManager.transform.position, Quaternion.identity).GetComponent<BulletController>();

        bc.gameObject.SetActive(true);

        bc.transform.position = playerManager.transform.position + (attaqueDirection * 1.5f);

        bc.direction = attaqueDirection;

        bc.velocityMultiplierOverLifeTime = speedOverLifetime;

        bc.baseVelocity = 50f;

        bc.lifeTime = 1f;

        bc.StartCoroutine(IgnoreCollisionsFor(bc.GetComponent<Collider>(), 0.2f));

        yield return base.DoCompetence();
    }

    public IEnumerator IgnoreCollisionsFor(Collider c ,float seconds)
    {
        Physics.IgnoreCollision(playerManager.hitCollider, c, true);

        yield return new WaitForSeconds(seconds);

        Physics.IgnoreCollision(playerManager.hitCollider, c, false);
    }
}
