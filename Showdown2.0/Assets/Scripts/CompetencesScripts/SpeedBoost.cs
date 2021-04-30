using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Augmentation de Vitesse")]
public class SpeedBoost : Competence
{
    public float boostMultiplier;

    public float duration;

    public override IEnumerator DoCompetence()
    {
        playerManager.speedBoosting = boostMultiplier;

        yield return new WaitForSeconds(duration);

        playerManager.speedBoosting = 0;

        yield return base.DoCompetence();
    }
} 
