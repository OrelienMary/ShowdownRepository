using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Controller : MonoBehaviour
{
    public PlayerManager playerManager;

    public Competence[] newCompetences1;
    public Competence[] newCompetences2;
    [Range(0,1)] public float newPercentageSpeedHealth;

    public bool inPhase2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerManager.playerInput.phaseInput && inPhase2 == false)
        {
            inPhase2 = true;

            for(int i = 0; i < newCompetences1.Length; i++)
            {
                //playerManager.competenceSlots[i].competenceData1 = newCompetences1[i];
                //playerManager.competenceSlots[i].competenceData2 = newCompetences2[i];
            }

            playerManager.pMovement.rb.velocity = Vector3.zero;

            //playerManager.percentageSpeedHealth = newPercentageSpeedHealth;

            StartCoroutine(playerManager.playerUI.ChangePhase());

            playerManager.InitiateValues();
        }
    }
}
