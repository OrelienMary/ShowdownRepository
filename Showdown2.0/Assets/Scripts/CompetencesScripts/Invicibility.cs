using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Invinciblités")]
public class Invicibility : Competence
{
    public float duration;

    public override IEnumerator DoCompetence()
    {
        playerManager.invincible = true;

        yield return new WaitForSeconds(duration);

        playerManager.invincible = false;

        yield return base.DoCompetence();
    }
}
