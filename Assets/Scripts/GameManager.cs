using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Services
{
    public static GameManager GameManager;
    public static AIManager AIManager { get; set; }

    private static Player[] _players;

    public static Player[] Players
    {
        get
        {
            Debug.Assert(_players != null);
            return _players;
        }
        set => _players = value;
    }
}

public class GameManager : MonoBehaviour
{
    public const int GameSetting_PlayersPerTeam = 5;
    public const int GameSetting_GoldShowerAmount = 20;
    public const float GameSetting_GameTimerTotal = 60;


    public Material[] teamMaterials;
    public Material playerMaterial;
    public GameObject prefab_GameAgent;
    public Gold prefab_Gold;

    public GameObject parent_gameAgents;

    private void Awake()
    {
        Services.GameManager = this;
    }
    
    private void Start()
    {
        var playerGameObject = Instantiate(Services.GameManager.prefab_GameAgent);
        Services.Players = new[] { new Player(playerGameObject) };
        Services.AIManager = new AIManager();
        Services.AIManager.Initialize();
    }

    private void Update()
    {
        Services.AIManager.Update();
    }
}

