using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetenceSlot : MonoBehaviour
{
    public PlayerManager playerManager;

    public int inputIndex;

    public Competence competenceData;
    Competence competenceInstance;

    // Start is called before the first frame update
    void Start()
    {
        competenceInstance = Instantiate(competenceData);

        competenceInstance.playerManager = playerManager;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerManager.playerInput.competencesInputs[inputIndex])
        {
            StartCoroutine(competenceInstance.DoCast());
        }
    }
}
