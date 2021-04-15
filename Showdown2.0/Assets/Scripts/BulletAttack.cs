using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletAttack : Competence
{
    public float baseSpeed;
    public AnimationCurve speedOverLifetime;

    public float size;

    public BulletAttack[] bulletAttack;
}
