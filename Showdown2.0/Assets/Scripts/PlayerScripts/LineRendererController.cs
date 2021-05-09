using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    Vector3[] oldPositions = new Vector3[7];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < oldPositions.Length; i++)
        {
            oldPositions[i] = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetOldPosition(0.45f, transform.position, 0));
        StartCoroutine(SetOldPosition(0.375f, transform.position, 1));
        StartCoroutine(SetOldPosition(0.3f, transform.position, 2)) ;
        StartCoroutine(SetOldPosition(0.225f, transform.position, 3));
        StartCoroutine(SetOldPosition(0.15f, transform.position, 4));
        StartCoroutine(SetOldPosition(0.075f, transform.position, 5));
        StartCoroutine(SetOldPosition(0f, transform.position, 6));

        lineRenderer.SetPositions(oldPositions);
    }

    IEnumerator SetOldPosition(float oldTime, Vector3 position, int positionIndex)
    {
        yield return new WaitForSeconds(oldTime);

        oldPositions[positionIndex] = position;
    }
    
}
