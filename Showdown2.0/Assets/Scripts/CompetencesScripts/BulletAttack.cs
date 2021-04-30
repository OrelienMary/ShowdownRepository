using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "BulletAttack")]
public class BulletAttack : Competence
{
    public float baseSpeed;
    public AnimationCurve speedOverLifetime;

    public float size;
    public float lifeTime;

    public MultiDimensionalArray[] bulletsAngle;

    public float intervalleTime;

    public BulletAttack bulletToInstantiate;

    public GameObject bulletPrefab;



    public override IEnumerator DoCompetence()
    {
        stunDuringEffect = true;

        

        if (bulletsAngle.Length == 0)
        {
            bulletsAngle = new MultiDimensionalArray[1];
            bulletsAngle[0] = new MultiDimensionalArray();
        }

        for (int x = 0; x < bulletsAngle.Length; x++)
        {
            Vector3 attaqueDirection = playerManager.playerInput.aimDirection;

            for (int i = 0; i < bulletsAngle[x].floatArray.Length; i++)
            {
                BulletController bc = Instantiate(bulletPrefab, playerManager.transform.position, Quaternion.identity).GetComponent<BulletController>();

                float angleFinal = Vector3.Angle(playerManager.transform.forward, attaqueDirection);

                if (attaqueDirection.x < 0)
                {
                    angleFinal = -angleFinal;
                }

                angleFinal += bulletsAngle[x].floatArray[i];

                bc.transform.rotation = Quaternion.Euler(0, angleFinal, 0);

                bc.direction = bc.transform.forward;

                bc.transform.rotation = Quaternion.Euler(Vector3.zero);

                bc.transform.position = playerManager.transform.position + (bc.direction * 1.5f);

                bc.velocityMultiplierOverLifeTime = speedOverLifetime;

                bc.baseVelocity = baseSpeed;

                bc.lifeTime = lifeTime;

                bc.size = size;

                bc.StartCoroutine(IgnoreCollisionsFor(bc.GetComponent<Collider>(), 0.2f));
            }

            yield return new WaitForSeconds(intervalleTime);
        }

        stunDuringEffect = false;

        yield return base.DoCompetence();
    }

    public IEnumerator IgnoreCollisionsFor(Collider c ,float seconds)
    {
        Physics.IgnoreCollision(playerManager.hitCollider, c, true);

        yield return new WaitForSeconds(seconds);

        Physics.IgnoreCollision(playerManager.hitCollider, c, false);
    }
}
