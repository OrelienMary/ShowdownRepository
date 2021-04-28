using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cover")]
public class Covers : Competence
{
    public GameObject coverPrefab;

    public float size;

    public float distance;

    public int coverLife;

    public override IEnumerator DoCompetence()
    {
        Vector3 attaqueDirection = playerManager.playerInput.aimDirection;

        float angleFinal = Vector3.Angle(playerManager.transform.forward, attaqueDirection);

        if (attaqueDirection.x < 0)
        {
            angleFinal = -angleFinal;
        }

        CoverController cc = Instantiate(coverPrefab).GetComponent<CoverController>();

        cc.life = coverLife;

        cc.size = size;

        cc.transform.position = playerManager.transform.position + (attaqueDirection * distance);

        cc.transform.rotation = Quaternion.Euler(0, angleFinal, 0);

        yield return base.DoCompetence();
    }
}
