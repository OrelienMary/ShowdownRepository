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

    public override IEnumerator DoCompetence()
    {
        Debug.Log("bullet attack");

        yield return base.DoCompetence();
    }
}
