using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Competence : ScriptableObject
{
    [HideInInspector] public PlayerManager playerManager;

    public float cooldownTime;
    [HideInInspector] public float cooldownTimer;

    public float castTime;
    public int castEffect;

    [HideInInspector] public bool casting;

    [HideInInspector] public bool stunDuringEffect;

    public float recoveryTime;
    public int recoveryEffect;

    [HideInInspector] public bool recovering;

    [HideInInspector] public bool competencing;

    [HideInInspector] public bool isSlow;
    [HideInInspector] public bool isStopped;

    public IEnumerator DoCast()
    {
        competencing = true;

        casting = true;

        for(float i = 0; i <= castTime; i += Time.fixedDeltaTime)
        {
            if(castEffect == 1)
            {
                isSlow = true;
            }
            else if(castEffect == 2)
            {
                isStopped = true;
            }
            yield return new WaitForFixedUpdate();
        }

        playerManager.StartCoroutine(DoCompetence());

        isSlow = false;
        isStopped = false;

        casting = false;
    }

    public virtual IEnumerator DoCompetence()
    {
        playerManager.StartCoroutine(DoRecovery());

        yield return null;
    }

    public IEnumerator DoRecovery()
    {
        recovering = true;

        for (float i = 0; i <= recoveryTime; i += Time.fixedDeltaTime)
        {
            if (recoveryEffect == 1)
            {
                isSlow = true;
            }
            else if (recoveryEffect == 2)
            {
                isStopped = true;
            }

            yield return new WaitForFixedUpdate();
        }

        isSlow = false;
        isStopped = false;

        recovering = false;

        competencing = false;

        playerManager.StartCoroutine(DoCooldown());
    }

    public IEnumerator DoCooldown()
    {
        for (cooldownTimer = cooldownTime; cooldownTimer > 0f; cooldownTimer -= Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }


    }
}
