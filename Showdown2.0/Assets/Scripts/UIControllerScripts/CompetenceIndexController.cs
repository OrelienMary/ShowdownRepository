using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceIndexController : MonoBehaviour
{
    public BuildUIController buildUI;
    public int competenceIndex;

    public Text currentCompetenceText;

    public CompetenceTypesController competenceTypes;

    private void Update()
    {
        currentCompetenceText.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void StartSelected()
    {
        buildUI.ChangeCompetenceIndex(competenceIndex);

        StartCoroutine(Selected());
    }

    public IEnumerator Selected()
    {
        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = transform.localScale.x + Time.fixedDeltaTime;

            transform.localScale = new Vector3(v, v, v);

            yield return new WaitForFixedUpdate();
        }
    }

    public void StartDeselected()
    {
        StartCoroutine(Deselected());
    }

    public IEnumerator Deselected()
    {
        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = transform.localScale.x - Time.fixedDeltaTime;

            transform.localScale = new Vector3(v, v, v);

            yield return new WaitForFixedUpdate();
        }
    }

    public void StartOpenCompetenceTypes()
    {
        competenceTypes.currentBuildUI = buildUI;

        StartCoroutine(competenceTypes.OpenCompetenceTypes());
    }
}
