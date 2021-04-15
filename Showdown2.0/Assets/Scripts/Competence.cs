using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competence
{
    public float cooldownTime;

    public float castTime;
    public int castEffect;

    public bool stunDuringEffect;

    public float recoveryTime;
    public int recoveryEffect;

    public virtual IEnumerator DoCast()
    {
        yield return null;
    }

    public virtual IEnumerator DoCompetence()
    {
        yield return null;
    }
}
