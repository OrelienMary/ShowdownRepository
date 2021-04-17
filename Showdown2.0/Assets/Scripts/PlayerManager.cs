using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PMovement pMovement;

    [HideInInspector] public bool slowed;
    [HideInInspector] public bool stopped;

    public int maxHealth;
    [HideInInspector] public int currentHealth;
}
