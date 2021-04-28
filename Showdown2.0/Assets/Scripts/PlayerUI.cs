using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public PlayerManager playerManager;

    public GameObject healthPointPrefab;
    public Transform healthPointsParent;

    public Image[] competencesCooldowns;
    Color[] competencesCooldownsColors = new Color[3];

    public Image switchCooldown;
    public Text buildText;

    List<HealthPoint> healthPoints = new List<HealthPoint>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < competencesCooldowns.Length; i++)
        {
            competencesCooldownsColors[i] = competencesCooldowns[i].color;
        }
    }

    public IEnumerator InitiateHealth()
    {
        playerManager.stunnedOverride = true;
        playerManager.invincible = true;

        for (int i = healthPoints.Count-1; i >= 0; i--)
        {
            playerManager.pMovement.rb.velocity = Vector3.zero;

            GameObject go = healthPoints[i].gameObject;

            healthPoints.RemoveAt(i);

            Destroy(go);

            yield return new WaitForSeconds(0.075f);
        }

        Vector2 lastHealthPointPosition = Vector2.zero - new Vector2(2f, 0f);

        for(int i = 0; i < playerManager.maxHealth; i++)
        {
            HealthPoint hp = Instantiate(healthPointPrefab, healthPointsParent).GetComponent<HealthPoint>();

            hp.transform.localPosition = lastHealthPointPosition + new Vector2(50f,0f);

            lastHealthPointPosition = hp.transform.localPosition;

            healthPoints.Add(hp);

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < playerManager.maxHealth; i++)
        {
            healthPoints[i].full = true;

            yield return new WaitForSeconds(0.1f);
        }

        playerManager.stunnedOverride = false;
        playerManager.invincible = false;

        StartCoroutine( playerManager.InvincibleFor(1f) );
    }

    public void UpdateHealth()
    {
        int currentHealth = playerManager.currentHealth;

        for(int i = 0; i < playerManager.maxHealth; i++)
        {
            if(currentHealth > 0)
            {
                currentHealth--;

                healthPoints[i].full = true;
            }
            else
            {
                healthPoints[i].full = false;
            }
        }
    }

    public IEnumerator ChangePhase()
    {
        Vector3 baseCooldownPosition = competencesCooldowns[0].transform.parent.parent.parent.position;

        for(float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            competencesCooldowns[0].transform.parent.parent.parent.localScale = new Vector3(0.8f - i, 0.8f - i, 0.8f - i);

            yield return new WaitForFixedUpdate();
        }

        for (float i = 0; i < playerManager.maxHealth * 0.1f * 2.5f * 0.5f; i += Time.fixedDeltaTime)
        {
            competencesCooldowns[0].transform.parent.parent.parent.position = competencesCooldowns[0].transform.parent.parent.parent.position - new Vector3(0f, 1f / (playerManager.maxHealth * 0.015f) , 0f);

            competencesCooldowns[0].transform.parent.parent.parent.rotation = Quaternion.Euler(competencesCooldowns[0].transform.parent.parent.parent.eulerAngles - new Vector3(0f, 0f, 1f / (playerManager.maxHealth * 0.015f)));

            yield return new WaitForFixedUpdate();
        }

        for (float i = 0; i < playerManager.maxHealth * 0.1f * 2.5f * 0.5f; i += Time.fixedDeltaTime)
        {
            competencesCooldowns[0].transform.parent.parent.parent.position = competencesCooldowns[0].transform.parent.parent.parent.position + new Vector3(0f, 1f / (playerManager.maxHealth * 0.015f), 0f);

            competencesCooldowns[0].transform.parent.parent.parent.rotation = Quaternion.Euler(competencesCooldowns[0].transform.parent.parent.parent.eulerAngles + new Vector3(0f, 0f, 1f / (playerManager.maxHealth * 0.015f)));

            yield return new WaitForFixedUpdate();
        }

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            competencesCooldowns[0].transform.parent.parent.parent.localScale = new Vector3(0.6f + i, 0.6f + i, 0.6f + i);

            yield return new WaitForFixedUpdate();
        }

        competencesCooldowns[0].transform.parent.parent.parent.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        competencesCooldowns[0].transform.parent.parent.parent.position = baseCooldownPosition;
        competencesCooldowns[0].transform.parent.parent.parent.rotation = Quaternion.Euler(Vector3.zero);
    }

    public IEnumerator ChangeBuild()
    {
        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            competencesCooldowns[0].transform.parent.parent.parent.localScale = new Vector3(0.8f - i, 0.8f - i, 0.8f - i);

            yield return new WaitForFixedUpdate();
        }

        for (float i = 0; i < 0.25f; i += Time.fixedDeltaTime)
        {
            competencesCooldowns[0].transform.parent.parent.rotation = Quaternion.Euler(competencesCooldowns[0].transform.parent.parent.eulerAngles - new Vector3(0f, 0f, (360f/0.25f) * Time.fixedDeltaTime));

            yield return new WaitForFixedUpdate();
        }

        for (float i = 0; i < 0.2f; i += Time.fixedDeltaTime)
        {
            competencesCooldowns[0].transform.parent.parent.parent.localScale = new Vector3(0.6f + i, 0.6f + i, 0.6f + i);

            yield return new WaitForFixedUpdate();
        }

        competencesCooldowns[0].transform.parent.parent.parent.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        competencesCooldowns[0].transform.parent.parent.rotation = Quaternion.Euler(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        switchCooldown.fillAmount = playerManager.switchCooldownTimer / playerManager.switchCooldown;
        buildText.text = playerManager.buildNumber.ToString();

        for(int i = 0; i < competencesCooldowns.Length; i++)
        {
            if(playerManager.competencesCompetencing[i])
            {
                competencesCooldowns[i].color = Color.white;

                competencesCooldowns[i].transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                competencesCooldowns[i].fillAmount = 1;
            }
            else
            {
                competencesCooldowns[i].color = competencesCooldownsColors[i];

                competencesCooldowns[i].transform.localScale = new Vector3(1f, 1f, 1f);

                competencesCooldowns[i].fillAmount = (playerManager.competenceSlots[i].currentCompetenceInstance.cooldownTime - playerManager.competenceSlots[i].currentCompetenceInstance.cooldownTimer) / playerManager.competenceSlots[i].currentCompetenceInstance.cooldownTime;
            }
        }
    }
}
