using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceController : MonoBehaviour
{
    public CompetencesPanelController competencesPanel;

    public Image selectedImage;
    public Image[] otherSelectedImages;

    public Text buttonText;

    public Competence competence;

    private void Start()
    {
        if(competence != null)
            buttonText.text = competence.name;
    }

    public void ChooseCompetence()
    {
        selectedImage.enabled = true;

        for (int i = 0; i < otherSelectedImages.Length; i++)
        {
            otherSelectedImages[i].enabled = false;
        }

        competencesPanel.competencesButton.competenceTypes.currentBuildUI.competenceIndexes[competencesPanel.competencesButton.competenceTypes.currentBuildUI.currentCompetenceIndex].currentCompetenceText.text = competence.name;

        if (competencesPanel.competencesButton.competenceTypes.currentBuildUI.buildIndex == 0)
        {
            competencesPanel.competencesButton.competenceTypes.currentBuildUI.menuUI.playerManager.competenceSlots[competencesPanel.competencesButton.competenceTypes.currentBuildUI.currentCompetenceIndex].competenceData1 = competence;
        }
        else if (competencesPanel.competencesButton.competenceTypes.currentBuildUI.buildIndex == 1)
        {
            competencesPanel.competencesButton.competenceTypes.currentBuildUI.menuUI.playerManager.competenceSlots[competencesPanel.competencesButton.competenceTypes.currentBuildUI.currentCompetenceIndex].competenceData2 = competence;
        }
    }
}
