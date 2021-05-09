using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PMovement : MonoBehaviour
{
    public PlayerManager playerManager;

    public float speed;
    float baseSpeed;
    [HideInInspector] public float speedMultiplier;

    public Rigidbody rb;

    float originalPlayerY;
    public float idleDuration;
    public AnimationCurve idleFloatting;

    public GameObject aimTurn;
    public GameObject movementTilt;

    float horizontalHeavy;
    float verticalHeavy;

    // Start is called before the first frame update
    void Awake()
    {
        baseSpeed = speed;

        originalPlayerY = playerManager.anim.transform.localPosition.y;

        Invoke("StartFloatIdle", Random.Range(0f, 1f));
    }

    public void Initiate()
    {
        if(!playerManager.phase2Controller.inPhase2)
            speed = baseSpeed + 1.7f * baseSpeed * (playerManager.percentageSpeedHealth);
        else
            speed = 1.25f * baseSpeed + 1.7f * baseSpeed * (playerManager.percentageSpeedHealth);

        speedMultiplier = 1f;
    }

    void StartFloatIdle()
    {
        StartCoroutine(FloatIdle());
    }

    IEnumerator FloatIdle()
    {
        for(float i = 0f; i < idleDuration; i += Time.fixedDeltaTime)
        {
            playerManager.anim.transform.localPosition = new Vector3(0f, originalPlayerY + idleFloatting.Evaluate(i/idleDuration), 0f);

            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(FloatIdle());
    }

    // Update is called once per frame
    void Update()
    {
        bool competencing = false;

        for (int i = 0; i < playerManager.playerInput.competencesInputs.Length; i++)
        {
            if (playerManager.playerInput.competencesInputs[i])
            {
                competencing = true;
            }
        }

        float horizontalTarget = playerManager.playerInput.horizontal;
        float verticalTarget = playerManager.playerInput.vertical;

        if (competencing)
        {
            horizontalTarget = 0;
            verticalTarget = 0;
        }

        if(horizontalHeavy < horizontalTarget - 0.1f)
        {
            horizontalHeavy += Time.deltaTime * 4f;
        }
        else if (horizontalHeavy > horizontalTarget + 0.1f)
        {
            horizontalHeavy -= Time.deltaTime * 4f;
        }

        if (verticalHeavy < verticalTarget - 0.1f)
        {
            verticalHeavy += Time.deltaTime * 4f;
        }
        else if (verticalHeavy > verticalTarget + 0.1f)
        {
            verticalHeavy -= Time.deltaTime * 4f;
        }

        playerManager.anim.transform.localRotation = Quaternion.LookRotation(playerManager.playerInput.aimDirection, Vector3.up);

        Vector2 v = new Vector2(horizontalHeavy, verticalHeavy);

        if (v.magnitude > 1f)
            v = v.normalized;

        playerManager.anim.transform.parent.localRotation = Quaternion.Euler(v.y * 20f,0f, -v.x * 20f);

        if (playerManager.stopped)
        {
            speedMultiplier = 0.15f;

        }
        else if(playerManager.slowed)
        {
            speedMultiplier = 0.5f;
        }
        else if(playerManager.speedBoosting > 0f)
        {
            speedMultiplier = playerManager.speedBoosting;
        }
        else
        {
            speedMultiplier = 1f;
        }
        
        if(playerManager.stunned == false)
            rb.velocity = new Vector3(playerManager.playerInput.horizontal, 0f, playerManager.playerInput.vertical).normalized * speed * speedMultiplier;
    }
}
