using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Competence : ScriptableObject
{
    [HideInInspector] public PlayerManager playerManager;

    public float cooldownTime;

    public float castTime;
    public int castEffect;

    [HideInInspector] public bool casting;

    public bool stunDuringEffect;

    public float recoveryTime;
    public int recoveryEffect;

    [HideInInspector] public bool recovering;

    [HideInInspector] public bool isSlow;
    [HideInInspector] public bool isStopped;

    public IEnumerator DoCast()
    {
        casting = true;

        for(float i = 0; i < castTime; i += Time.fixedDeltaTime)
        {
            if(castEffect == 1)
            {
                isSlow = true;
            }
            else if(castEffect == 2)
            {
                isStopped = true;
            }
            Debug.Log("casting");
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

        for (float i = 0; i < castTime; i += Time.fixedDeltaTime)
        {
            if (castEffect == 1)
            {
                isSlow = true;
            }
            else if (castEffect == 2)
            {
                isStopped = true;
            }
            Debug.Log("recovering");
            yield return new WaitForFixedUpdate();
        }

        isSlow = false;
        isStopped = false;

        recovering = false;
    }
}
