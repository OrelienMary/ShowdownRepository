using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public PlayerManager[] playerManagers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool bothReady = true;

        for(int i = 0; i< playerManagers.Length; i++)
        {
            if(playerManagers[i].menuUI.ready == false)
            {
                bothReady = false;
                return;
            }
        }

        if(bothReady == true)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        for (int i = 0; i < playerManagers.Length; i++)
        {
            playerManagers[i].inGame = true;
        }

        for (float i = 0; i < 3f; i += Time.fixedDeltaTime)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.fixedDeltaTime * 0.05f, transform.position.z);

            yield return new WaitForFixedUpdate();
        }
    }
}
