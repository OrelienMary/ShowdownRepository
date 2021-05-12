using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager rM;

    public PlayerManager[] playerManagers;

    Competence[][][] playerCompetences;

    float[] percentagesSpeedHealth;

    private void Awake()
    {
        if(rM == null)
        {
            rM = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        percentagesSpeedHealth = new float[playerManagers.Length];

        playerCompetences = new Competence[playerManagers.Length][][];

        for (int i = 0; i < playerCompetences.Length; i++)
        {
            playerCompetences[i] = new Competence[2][];

            for (int x = 0; x < playerCompetences[i].Length; x++)
            {
                playerCompetences[i][x] = new Competence[3];
            }
        }
    }

    public void Respawn()
    {
        for(int i = 0; i < playerCompetences.Length; i++)
        {
            percentagesSpeedHealth[i] = playerManagers[i].percentageSpeedHealth;

            for(int x = 0; x < playerCompetences[i].Length; x++)
            {
                for (int z = 0; z < playerCompetences[i][x].Length; z++)
                {
                    if(x == 0)
                        playerCompetences[i][x][z] = playerManagers[i].competenceSlots[z].competenceData1;
                    else
                        playerCompetences[i][x][z] = playerManagers[i].competenceSlots[z].competenceData2;
                }
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Invoke("DistributeCompetencesToNewPlayers", 0.3f);
        
    }

    public void DistributeCompetencesToNewPlayers()
    {
        for (int i = 0; i < playerManagers.Length; i++)
        {
            playerManagers[i] = GameObject.Find("Player" + (i + 1)).GetComponent<PlayerManager>();
        }

        for (int i = 0; i < playerCompetences.Length; i++)
        {
            playerManagers[i].percentageSpeedHealth = percentagesSpeedHealth[i];
            playerManagers[i].menuUI.sliderSpeedLife.value = percentagesSpeedHealth[i] * 4f;

            for (int x = 0; x < playerCompetences[i].Length; x++)
            {
                for (int z = 0; z < playerCompetences[i][x].Length; z++)
                {
                    if (x == 0)
                    {
                        playerManagers[i].competenceSlots[z].competenceData1 = playerCompetences[i][x][z];
                    }
                    else
                    {
                        playerManagers[i].competenceSlots[z].competenceData2 = playerCompetences[i][x][z];
                    }
                    
                    playerManagers[i].menuUI.builds[x].competenceIndexes[z].currentCompetenceText.text = playerCompetences[i][x][z].name;
                }
            }
        }

        /*for (int i = 0; i < playerManagers.Length; i++)
        {
            playerManagers[i].InitiateValues();
        }*/
    }
}
