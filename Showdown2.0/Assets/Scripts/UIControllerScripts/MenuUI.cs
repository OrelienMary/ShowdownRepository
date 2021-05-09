using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    public PlayerManager playerManager;

    public MyEventSystem myEventSystem;
    public StandaloneInputModule standaloneInput;

    public BuildUIController buildSelected;
    public BuildUIController[] builds;

    public Slider sliderSpeedLife;

    [HideInInspector] public BuildUIController currentBuild;

    [HideInInspector] public bool ready;
    public Text waitingOrReadyText;

    [HideInInspector] public bool inMenu;
    [HideInInspector] public bool inBuildLayer;
    [HideInInspector] public bool inCompetenceIndexLayer;
    [HideInInspector] public bool inCompetenceTypeLayer;
    [HideInInspector] public bool inCompetenceLayer;

    public void Initiate()
    {
        standaloneInput.horizontalAxis = "Horizontal" + playerManager.playerInput.inputPattern;
        standaloneInput.verticalAxis = "Vertical" + playerManager.playerInput.inputPattern;

        standaloneInput.submitButton = "Submit" + playerManager.playerInput.inputPattern;
        standaloneInput.cancelButton = "Cancel" + playerManager.playerInput.inputPattern;
    }

    private void Update()
    {
        if(playerManager.inGame)
        {
            waitingOrReadyText.text = "";
            return;
        }

        if(inMenu)
        {
            waitingOrReadyText.text = "";
        }
        else if(ready == false)
        {
            waitingOrReadyText.text = "Waiting";
        }
        else if (ready == true)
        {
            waitingOrReadyText.text = "Ready!";
        }

        if (playerManager.playerInput.menuInput == true)
        {
            if (!inMenu)
            {
                if(ready)
                {
                    ready = false;
                }
                else
                {
                    OpenBuilds();
                }
            }
            else
            {
                if(inBuildLayer)
                {
                    CloseBuilds();
                }
                else if(inCompetenceIndexLayer)
                {
                    StartCoroutine(currentBuild.CloseCompetenceIndex());
                }
                else if(inCompetenceTypeLayer)
                {
                    StartCoroutine(currentBuild.competenceIndexes[currentBuild.currentCompetenceIndex].competenceTypes.CloseCompetencesTypes());
                }
                else if (inCompetenceLayer)
                {
                    StartCoroutine(currentBuild.competenceIndexes[currentBuild.currentCompetenceIndex].competenceTypes.currentCompetencesButton.competencesPanel.CloseCompetences());
                }
            }
        }
        
        playerManager.playerInput.menuInput = false;
    }

    void OpenBuilds()
    {
        inMenu = true;

        inBuildLayer = true;

        buildSelected.transform.parent.gameObject.SetActive(true);

        playerManager.playerUI.healthPointsParent.gameObject.SetActive(false);
        playerManager.playerUI.competencesCooldowns[0].transform.parent.parent.parent.gameObject.SetActive(false);

        myEventSystem.SetSelectedGameObject(buildSelected.gameObject);

        buildSelected.StartSelected();
    }

    void CloseBuilds()
    {
        inMenu = false;

        inBuildLayer = false;

        buildSelected.transform.parent.gameObject.SetActive(false);

        playerManager.playerUI.healthPointsParent.gameObject.SetActive(true);
        playerManager.playerUI.competencesCooldowns[0].transform.parent.parent.parent.gameObject.SetActive(true);

        buildSelected.StartDeselected();

        playerManager.InitiateValues();
    }
}
