using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetencesButtonController : MonoBehaviour
{
    public CompetenceTypesController competenceTypes;

    public CompetencesPanelController competencesPanel;

    public Text buttonText;

    public Image selectedImage;

    public Image[] otherSelectedImages;

    private void Start()
    {
        buttonText.text = gameObject.name;
    }

    public void StartOpenCompetences()
    {
        competenceTypes.currentCompetencesButton = this;

        competencesPanel.competencesButton = this;
        StartCoroutine(competencesPanel.OpenCompetences());
    }
}
