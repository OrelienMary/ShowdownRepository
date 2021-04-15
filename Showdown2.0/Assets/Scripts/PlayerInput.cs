using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    public int inputPattern;

    [HideInInspector] public Vector3 aimDirection;

    public bool[] competencesInputs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal" + inputPattern);
        vertical = Input.GetAxis("Vertical" + inputPattern);

        if(inputPattern == 0)
        {
            aimDirection = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        }
        else
        {
            aimDirection = new Vector2(Input.GetAxis("HorizontalRight" + inputPattern), Input.GetAxis("VerticalRight" + inputPattern));
        }

        if(competencesInputs[0] == false)
            competencesInputs[0] = Input.GetButtonDown("Attack1" + inputPattern);
        if(competencesInputs[1] == false)
            competencesInputs[1] = Input.GetButtonDown("Attack2" + inputPattern);

        if(competencesInputs[2] == false)
            competencesInputs[2] = Input.GetButtonDown("Defense" + inputPattern);
    }
}
