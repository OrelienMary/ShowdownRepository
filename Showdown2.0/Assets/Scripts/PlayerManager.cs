using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PMovement pMovement;

    public Collider hitCollider;

    [HideInInspector] public bool[] competencesSlowed = new bool[3];
    [HideInInspector] public bool[] competencesStopped = new bool[3];
    [HideInInspector] public bool[] competencesStunned = new bool[3];

    [HideInInspector] public bool slowed;
    [HideInInspector] public bool stopped;
    [HideInInspector] public bool stunned;

    public int maxHealth;
    [HideInInspector] public int currentHealth;

    public new Renderer renderer;

    public Material normalMaterial;
    public Material slowedMaterial;
    public Material stoppedMaterial;

    public Text healthText;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
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

        for (int i = 0; i < competencesStunned.Length; i++)
        {
            if (competencesStunned[i] == true)
            {
                stunned = true;
                break;
            }
        }

        if (stopped == true)
            renderer.material = stoppedMaterial;
        else if (slowed == true)
            renderer.material = slowedMaterial;
        else
            renderer.material = normalMaterial;
    }

    public void TakeDamage()
    {
        currentHealth -= 1;

        Debug.Log(currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Dead");
    }
}
