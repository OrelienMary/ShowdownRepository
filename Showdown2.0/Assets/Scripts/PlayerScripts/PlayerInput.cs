using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerManager playerManager;
    public LineRenderer aimRenderer;

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    public int inputPattern;

    [HideInInspector] public Vector3 aimDirection;

    public bool[] competencesInputs;

    public bool switchInput;
    public bool phaseInput;

    public bool menuInput;

    public LayerMask mouseLayer;

    private void Start()
    {
        aimDirection = Vector3.forward;

        Invoke("ValidateMenuInput", 0.2f);
    }

    public void ValidateMenuInput()
    {
        menuInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.dead == true)
            return;

        if (playerManager.inGame == false)
        {

            if (menuInput == false)
                menuInput = Input.GetButtonDown("Cancel" + inputPattern);

            if (playerManager.menuUI != null)
                if (playerManager.menuUI.inMenu == true)
                {
                    horizontal = 0f;
                    vertical = 0f;

                    for (int i = 0; i < competencesInputs.Length; i++)
                    {
                        competencesInputs[i] = false;
                    }

                    switchInput = false;
                    phaseInput = false;

                    return;
                }
                else
                {
                    if (Input.GetButtonDown("Submit" + inputPattern))
                    {
                        playerManager.menuUI.ready = true;
                    }
                }
        }    

        horizontal = Input.GetAxis("Horizontal" + inputPattern);
        vertical = Input.GetAxis("Vertical" + inputPattern);

        if(inputPattern == 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f,mouseLayer,QueryTriggerInteraction.Collide))
            {
                aimDirection = (hit.point - transform.position).normalized;
            }
        }
        else
        {
            if(new Vector3(Input.GetAxis("HorizontalRight" + inputPattern), 0f, Input.GetAxis("VerticalRight" + inputPattern)).magnitude > 0.1f)
                aimDirection = new Vector3(Input.GetAxis("HorizontalRight" + inputPattern), 0f, Input.GetAxis("VerticalRight" + inputPattern)).normalized;
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

        switchInput = Input.GetButton("Switch" + inputPattern);

        if(Input.GetButton("Phase1"+inputPattern) && Input.GetButton("Phase2" + inputPattern))
        {
            phaseInput = true;
        }
        else
        {
            phaseInput = false;
        }

        if (competencesInputs[0] || competencesInputs[1])
        {
            playerManager.anim.SetBool("Casting", true);
        }
        else
        {
            playerManager.anim.SetBool("Casting", false);
        }
        //+new Vector3(0f, 0f, -1f)
        aimRenderer.SetPosition(0, transform.position + (aimDirection * 30f));
        aimRenderer.SetPosition(1, transform.position );
    }
}
