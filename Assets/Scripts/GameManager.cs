using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
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

    public static EventManager EventManager;
}

public class GameManager : MonoBehaviour
{
    public const int GameSetting_PlayersPerTeam = 5;
    public const int GameSetting_GoldShowerAmount = 20;
    public const float GameSetting_GameTimerTotal = 60;
    private float _gameTimer = 0;
    
    [Header("Mats")]
    public Material[] teamMaterials;
    public Material playerMaterial;
    
    [Header("Prefabs")]
    public GameObject prefab_GameAgent;
    public Gold prefab_Gold;

    [Header("Game Objects for Reference")]
    public GameObject startScreen;
    public GameObject inGameUI;
    public GameObject endScreen;
    public GameObject parentGameAgents;

    [Header("UI")] 
    public Text timerText;
    public Text redTeamScoreText;
    public Text blueTeamScoreText;
    public Text redTeamScoreFinalText;
    public Text blueTeamScoreFinalText;
    
    //state machine variables
    public FiniteStateMachine currentState;
    public State_StartScreen StateStartScreen = new State_StartScreen();
    public State_InGame StateInGame = new State_InGame();
    public State_EndGame StateEndGame = new State_EndGame();

    private void Awake()
    {
        Services.GameManager = this;
        Services.EventManager = new EventManager();
    }
    
    private void Start()
    {
        Services.EventManager.OnGoldPickedUp += UpdateTeamScore;
        Services.EventManager.OnGameEnd += UpdateFinalScores;
        currentState = StateStartScreen;

        currentState.OnEnter(this);
        
    }

    private void Update()
    {
        currentState.OnUpdate(this);
    }

    public void SwitchState(FiniteStateMachine state)
    {
        currentState = state;
        state.OnEnter(this);
    }

    public void InGameTimer()
    {
        if (_gameTimer > GameSetting_GameTimerTotal)
        {
            Debug.Log("timer done");
            _gameTimer = 0;
            Services.EventManager.GameTimeOut();
            return;
        }

        _gameTimer += Time.deltaTime;
        float timeLeftInGame = GameSetting_GameTimerTotal - _gameTimer;
        timerText.text = timeLeftInGame.ToString("F0");
    }

    private void UpdateTeamScore(object sender, EventManager.OnGoldPickedUpArgs e)
    {
        if (e.blueOrRed == 0)
        {
            blueTeamScoreText.text = Services.AIManager.blueScore.ToString();
        }
        else
        {
            redTeamScoreText.text = Services.AIManager.redScore.ToString();
        }
        
    }
    
    private void UpdateFinalScores(object sender, EventArgs e)
    {
        blueTeamScoreFinalText.text = "Final Score:\n" + Services.AIManager.blueScore;
        redTeamScoreFinalText.text = "Final Score:\n" + Services.AIManager.redScore;
    }

    private void OnDestroy()
    {
        Services.EventManager.OnGoldPickedUp -= UpdateTeamScore;
        Services.EventManager.OnGameEnd -= UpdateFinalScores;
    }
}

