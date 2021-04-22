using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dash")]
public class Dash : Competence
{
    public float speed;
    public float duration;

    public override IEnumerator DoCompetence()
    {
        Vector3 dashDirection = new Vector3(playerManager.playerInput.horizontal,0f, playerManager.playerInput.vertical).normalized;

        for(float i = 0; i < duration; i += Time.fixedDeltaTime)
        {
            stunDuringEffect = true;

            playerManager.pMovement.rb.velocity = dashDirection * speed;

            yield return new WaitForFixedUpdate();
        }

        stunDuringEffect = false;

        yield return base.DoCompetence();
    }
}
