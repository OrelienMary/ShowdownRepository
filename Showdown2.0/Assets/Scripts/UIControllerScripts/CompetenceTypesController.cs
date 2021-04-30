using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceTypesController : MonoBehaviour
{
    [HideInInspector] public BuildUIController currentBuildUI;

    [HideInInspector] public CompetencesButtonController currentCompetencesButton;

    public GameObject firstSelected;

    public IEnumerator OpenCompetenceTypes()
    {
        currentBuildUI.menuUI.inCompetenceTypeLayer = true;
        currentBuildUI.menuUI.inCompetenceIndexLayer = false;

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = transform.localScale.x + Time.fixedDeltaTime * 5f;

            transform.localScale = new Vector3(v, v, v);

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = new Vector3(1f, 1f, 1f);

        currentBuildUI.menuUI.myEventSystem.SetSelectedGameObject(firstSelected);
    }

    public IEnumerator CloseCompetencesTypes()
    {
        currentBuildUI.menuUI.inCompetenceTypeLayer = false;
        currentBuildUI.menuUI.inCompetenceIndexLayer = true;

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = transform.localScale.x - Time.fixedDeltaTime * 5f;

            if(v >= 0f)
                transform.localScale = new Vector3(v, v, v);
            else
                transform.localScale = new Vector3(0f, 0f, 0f);

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = new Vector3(0f, 0f, 0f);

        currentBuildUI.menuUI.myEventSystem.SetSelectedGameObject(currentBuildUI.competenceIndexes[currentBuildUI.currentCompetenceIndex].gameObject);
    }
}
