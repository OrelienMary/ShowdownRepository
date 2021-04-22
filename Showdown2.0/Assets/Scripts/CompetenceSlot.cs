using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetenceSlot : MonoBehaviour
{
    public PlayerManager playerManager;

    public int inputIndex;

    public Competence competenceData;
    [HideInInspector] public Competence competenceInstance;

    // Start is called before the first frame update
    void Start()
    {
        competenceInstance = Instantiate(competenceData);

        competenceInstance.playerManager = playerManager;
    }

    // Update is called once per frame
    void Update()
    {
        playerManager.competencesStopped[inputIndex] = competenceInstance.isStopped;
        playerManager.competencesSlowed[inputIndex] = competenceInstance.isSlow;
        playerManager.competencesStunned[inputIndex] = competenceInstance.stunDuringEffect;

        if (playerManager.playerInput.competencesInputs[inputIndex] && competenceInstance.cooldownTimer <= 0 && playerManager.slowed == false && playerManager.stopped == false && competenceInstance.casting == false && competenceInstance.recovering == false)
        {
            StartCoroutine(competenceInstance.DoCast());
        }

        playerManager.playerInput.competencesInputs[inputIndex] = false;
    }
}
