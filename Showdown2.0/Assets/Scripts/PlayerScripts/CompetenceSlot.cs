using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetenceSlot : MonoBehaviour
{
    public PlayerManager playerManager;

    public int inputIndex;

    public Competence competenceData1;
    public Competence competenceData2;
    [HideInInspector] public Competence currentCompetenceInstance;
    [HideInInspector] public Competence competenceInstance1;
    [HideInInspector] public Competence competenceInstance2;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initiate()
    {
        competenceInstance1 = Instantiate(competenceData1);
        competenceInstance2 = Instantiate(competenceData2);

        currentCompetenceInstance = competenceInstance1;

        competenceInstance1.playerManager = playerManager;
        competenceInstance2.playerManager = playerManager;
    }

    // Update is called once per frame
    void Update()
    {
        playerManager.competencesStopped[inputIndex] = currentCompetenceInstance.isStopped;
        playerManager.competencesSlowed[inputIndex] = currentCompetenceInstance.isSlow;
        playerManager.competencesStunned[inputIndex] = currentCompetenceInstance.stunDuringEffect;
        playerManager.competencesCompetencing[inputIndex] = currentCompetenceInstance.competencing;

        if (playerManager.playerInput.competencesInputs[inputIndex] && currentCompetenceInstance.cooldownTimer <= 0 && currentCompetenceInstance.competencing == false && playerManager.stunned == false && playerManager.stopped == false && playerManager.slowed == false)
        {
            StartCoroutine(currentCompetenceInstance.DoCast());
        }
    }
}
