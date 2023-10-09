using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public const string PlayerJoinedMessage = "OnPlayerJoined";
    public const string PlayerLeftMessage = "OnPlayerLeft";
    public GameObject[] spawnPoints;

    public static GameManager instance = null;
    public List<PlayerInput> playerlist = new List<PlayerInput>();

    public event System.Action<PlayerInput> PlayerJoinedGame;
    public event System.Action<PlayerInput> PlayerLeftGame;

    [SerializeField] InputAction joinAction;
    [SerializeField] InputAction leaveAction;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        joinAction.Enable();
        leaveAction.Enable();
    }
    private void Start()
    {
        //PlayerInputManager.instance.JoinPlayer(1, -1, null);

        //PlayerJoinedGame += OnPlayerJoined;
    }
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Joined");
        playerlist.Add(playerInput);
        if(PlayerJoinedGame != null)
        {
            PlayerJoinedGame(playerInput);
        }
    }
    public void onPlayerLeft (PlayerInput playerinput)
    {
        Debug.Log("left");
    }
    //public event Action<PlayerInput> onPlayerJoined;



}
