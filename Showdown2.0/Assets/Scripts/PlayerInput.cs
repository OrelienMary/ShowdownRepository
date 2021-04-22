using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    public int inputPattern;

    [HideInInspector] public Vector3 aimDirection;

    [HideInInspector] public bool[] competencesInputs;

    public LayerMask mouseLayer;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal" + inputPattern);
        vertical = Input.GetAxis("Vertical" + inputPattern);

        if(inputPattern == 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f,mouseLayer,QueryTriggerInteraction.Collide))
            {
                aimDirection = (hit.point - transform.position).normalized;

                aimDirection = new Vector3(aimDirection.x, aimDirection.z, 0f);
            }
        }
        else
        {
            aimDirection = new Vector2(Input.GetAxis("HorizontalRight" + inputPattern), Input.GetAxis("VerticalRight" + inputPattern)).normalized;
        }

        if(inputPattern == 0)
        {
            competencesInputs[0] = Input.GetButton("Attack1" + inputPattern);

            competencesInputs[1] = Input.GetButton("Attack2" + inputPattern);
        }
        else if (inputPattern > 0)
        {
            if(Input.GetAxis("Attack1&2" + inputPattern) < -0.5f)
                competencesInputs[0] = true;
            else
                competencesInputs[0] = false;

            if (Input.GetAxis("Attack1&2" + inputPattern) > 0.5f)
                competencesInputs[1] = true;
            else
                competencesInputs[1] = false;
        }

        competencesInputs[2] = Input.GetButton("Defense" + inputPattern);

    }
}
