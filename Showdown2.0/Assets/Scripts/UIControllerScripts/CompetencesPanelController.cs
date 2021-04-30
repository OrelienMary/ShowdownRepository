using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetencesPanelController : MonoBehaviour
{
    [HideInInspector] public CompetencesButtonController competencesButton;

    public GameObject firstSelected;

    public IEnumerator OpenCompetences()
    {
        competencesButton. competenceTypes.currentBuildUI.menuUI.inCompetenceTypeLayer = false;
        competencesButton. competenceTypes.currentBuildUI.menuUI.inCompetenceLayer = true;

        transform.position = competencesButton.transform.position;

        competencesButton.selectedImage.enabled = true;

        for (int i = 0; i < competencesButton.otherSelectedImages.Length; i++)
        {
            competencesButton. otherSelectedImages[i].enabled = false;
        }

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = transform.localScale.x + Time.fixedDeltaTime * 5f;

            transform.localScale = new Vector3(v, v, v);

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = new Vector3(1f, 1f, 1f);

        competencesButton. competenceTypes.currentBuildUI.menuUI.myEventSystem.SetSelectedGameObject(firstSelected);
    }

    public IEnumerator CloseCompetences()
    {
        competencesButton. competenceTypes.currentBuildUI.menuUI.inCompetenceTypeLayer = true;
        competencesButton. competenceTypes.currentBuildUI.menuUI.inCompetenceLayer = false;

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = transform.localScale.x - Time.fixedDeltaTime * 5f;

            transform.localScale = new Vector3(v, v, v);

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = new Vector3(0f, 0f, 0f);

        competencesButton. competenceTypes.currentBuildUI.menuUI.myEventSystem.SetSelectedGameObject(competencesButton.gameObject);
    }
}
