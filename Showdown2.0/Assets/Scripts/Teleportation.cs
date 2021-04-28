using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Teleportation")]
public class Teleportation : Competence
{
    public float distance;

    public override IEnumerator DoCompetence()
    {
        Vector3 dashDirection = new Vector3(playerManager.playerInput.horizontal, 0f, playerManager.playerInput.vertical).normalized;

        playerManager.transform.position += dashDirection * distance;

        yield return base.DoCompetence();
    }
}
