using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildUIController : MonoBehaviour
{
    public MenuUI menuUI;

    public int buildIndex;
    
    public CompetenceIndexController[] competenceIndexes;
    public GameObject[] otherBuildsObjects;

    public GameObject competences;
    public Transform target;

    Vector3 originalPosition;

    public int currentCompetenceIndex;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void StartSelected()
    {
        menuUI.StartCoroutine(Selected());
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
        menuUI.StartCoroutine(Deselected());
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

    public void ChangeCompetenceIndex(int index)
    {
        if (Mathf.Abs(currentCompetenceIndex - index) <= 1)
        {
            StartCoroutine(RotateXDegree((index - currentCompetenceIndex) * 120f));
        }
        else
        {
            if (currentCompetenceIndex == 0)
            {
                StartCoroutine(RotateXDegree(-120f));
            }
            else if (currentCompetenceIndex == 2)
            {
                StartCoroutine(RotateXDegree(120f));
            }
        }

        currentCompetenceIndex = index;
        
    }

    bool rotating = false;

    public IEnumerator RotateXDegree(float degrees)
    {
        while(rotating == true)
        {
            yield return null;
        }

        rotating = true;

        float originalRotation = competences.transform.eulerAngles.z;

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = competences.transform.eulerAngles.z + Time.fixedDeltaTime * degrees * 5f;

            competences.transform.rotation = Quaternion.Euler( new Vector3(0f, 0f, v) );

            yield return new WaitForFixedUpdate();
        }

        competences.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, originalRotation + degrees));

        rotating = false;
    }

    public IEnumerator GoToTargetPosition(Vector3 originalPosition, Vector3 positionToGoTo)
    {
        float d = Vector3.Distance(positionToGoTo, originalPosition);

        Vector3 direction = (positionToGoTo - originalPosition).normalized;

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = Time.fixedDeltaTime * 5f *d;

            transform.localPosition = transform.localPosition + direction * v;

            yield return new WaitForFixedUpdate();
        }

        transform.localPosition = positionToGoTo;
    }

    public void StartOpenCompetencesIndex()
    {
        StartCoroutine(OpenCompetencesIndex());
    }

    public IEnumerator OpenCompetencesIndex()
    {
        menuUI.inCompetenceIndexLayer = true;
        menuUI.inBuildLayer = false;
        menuUI.currentBuild = this;

        for(int i = 0; i <otherBuildsObjects.Length; i++)
        {
            otherBuildsObjects[i].SetActive(false);
        }

        for (int i = 0; i < competenceIndexes.Length; i++)
        {
            competenceIndexes[i].StartDeselected();
        }

        StartCoroutine(RotateXDegree(-60f));

        StartCoroutine(GoToTargetPosition(originalPosition, target.localPosition));

        for(float i = 0; i < 0.2f; i+=Time.fixedDeltaTime)
        {
            float v = transform.localScale.x + Time.fixedDeltaTime * 5f;

            transform.localScale = new Vector3(v, v, v);

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = new Vector3(2f, 2f, 2f);

        menuUI.myEventSystem.SetSelectedGameObject( competenceIndexes[0].gameObject );
    }

    public IEnumerator CloseCompetenceIndex()
    {
        menuUI.inCompetenceIndexLayer = false;
        menuUI.inBuildLayer = true;

        for (int i = 0; i < otherBuildsObjects.Length; i++)
        {
            otherBuildsObjects[i].SetActive(true);
        }

        for (int i = 0; i < competenceIndexes.Length; i++)
        {
            StartCoroutine(competenceIndexes[i].Selected());
        }

        if(currentCompetenceIndex == 0)
            StartCoroutine(RotateXDegree(60f));
        else if(currentCompetenceIndex == 1)
            StartCoroutine(RotateXDegree(-60f));
        else if (currentCompetenceIndex == 2)
            StartCoroutine(RotateXDegree(-180f));

        currentCompetenceIndex = 0;

        StartCoroutine(GoToTargetPosition(target.localPosition, originalPosition));

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            float v = transform.localScale.x - Time.fixedDeltaTime * 5f;

            transform.localScale = new Vector3(v, v, v);

            yield return new WaitForFixedUpdate();
        }

        transform.localScale = new Vector3(1f, 1f, 1f);

        menuUI.myEventSystem.SetSelectedGameObject(menuUI.buildSelected.gameObject);
    }


}
