using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PMovement pMovement;
    public Phase2Controller phase2Controller;

    public Collider hitCollider;

    public Animator anim;

    public CompetenceSlot[] competenceSlots = new CompetenceSlot[3];

    [HideInInspector] public bool[] competencesSlowed = new bool[3];
    [HideInInspector] public bool[] competencesStopped = new bool[3];
    [HideInInspector] public bool[] competencesStunned = new bool[3];
    [HideInInspector] public bool[] competencesCompetencing = new bool[3];

    [HideInInspector] public bool slowed;
    [HideInInspector] public bool stopped;
    [HideInInspector] public bool stunned;

    [HideInInspector] public bool stunnedOverride;

    [HideInInspector] public bool invincible;

    [HideInInspector] public float speedBoosting;

    public int maxHealth;
    int baseMaxHealth;

    [HideInInspector] public int buildNumber;
    public float switchCooldown;
    public float switchCooldownTimer;

    [Range(0,1)] public float percentageSpeedHealth;

    [HideInInspector] public int currentHealth;

    public new Renderer renderer;

    public Material normalMaterial;
    public Material slowedMaterial;
    public Material stoppedMaterial;
    public Material invincibleMaterial;
    public Material speedingMaterial;

    public PlayerUI playerUI;

    public Text healthText;

    private void Start()
    {
        baseMaxHealth = maxHealth;
        currentHealth = maxHealth;

        InitiateValues();
    }


    private void Update()
    {
        bool canSwitch = true;

        for(int i = 0; i < competencesCompetencing.Length; i++)
        {
            if (competencesCompetencing[i] == true)
            {
                canSwitch = false;
                break;
            }
        }

        if(playerInput.switchInput && switchCooldownTimer >= switchCooldown && canSwitch)
        {
            switchCooldownTimer = 0f;

            StartCoroutine(playerUI.ChangeBuild());

            for(int i = 0; i < competenceSlots.Length; i++)
            {
                if(buildNumber == 1)
                {
                    buildNumber = 2;
                    competenceSlots[i].currentCompetenceInstance = competenceSlots[i].competenceInstance2;
                }
                else if (buildNumber == 2)
                {
                    buildNumber = 1;
                    competenceSlots[i].currentCompetenceInstance = competenceSlots[i].competenceInstance1;
                }

            }
        }
        else if(switchCooldownTimer < switchCooldown)
        {
            switchCooldownTimer += Time.deltaTime;
        }

        healthText.text = currentHealth.ToString();

        slowed = false;
        stopped = false;
        stunned = false;

        for(int i = 0; i < competencesSlowed.Length; i++)
        {
            if(competencesSlowed[i] == true)
            {
                slowed = true;
                break;
            }
        }

        for (int i = 0; i < competencesStopped.Length; i++)
        {
            if (competencesStopped[i] == true)
            {
                stopped = true;
                break;
            }
        }

        if(!stunnedOverride)
        {
            for (int i = 0; i < competencesStunned.Length; i++)
            {
                if (competencesStunned[i] == true)
                {
                    stunned = true;
                    break;
                }
            }
        }
        else
        {
            stunned = true;
        }
        

        if (stopped == true || stunned == true)
            renderer.material = stoppedMaterial;
        else if (slowed == true)
            renderer.material = slowedMaterial;
        else if (invincible == true)
            renderer.material = invincibleMaterial;
        else if (speedBoosting > 0)
            renderer.material = speedingMaterial;
        else
            renderer.material = normalMaterial;
    }

    public void TakeDamage()
    {
        if (invincible)
            return;

        currentHealth -= 1;

        playerUI.UpdateHealth();

        if(currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibleFor(0.5f));
        }
    }

    public IEnumerator InvincibleFor(float seconds)
    {
        invincible = true;

        yield return new WaitForSeconds(seconds);

        invincible = false;
    }

    public void Die()
    {
        Debug.Log("Dead");
    }

    public void InitiateValues()
    {
        buildNumber = 1;
        switchCooldownTimer = switchCooldown;

        if (!phase2Controller.inPhase2)
            maxHealth = Mathf.RoundToInt( baseMaxHealth + 2f* baseMaxHealth * (1 - percentageSpeedHealth) );
        else
            maxHealth = Mathf.RoundToInt(1.25f *baseMaxHealth + 2f * baseMaxHealth * (1 - percentageSpeedHealth));

        pMovement.Initiate();

        currentHealth = maxHealth;

        for(int i = 0; i < competenceSlots.Length; i++)
        {
            competenceSlots[i].Initiate();
        }

        StartCoroutine(playerUI.InitiateHealth());
    }
}
