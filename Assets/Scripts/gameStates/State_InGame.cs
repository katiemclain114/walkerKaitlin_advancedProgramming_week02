using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class State_InGame : FiniteStateMachine
{
    public override void OnEnter(GameManager gameManager)
    {
        Services.EventManager.OnGameEnd += GameTimerEnd;
        Debug.Log("hello from in game");
        gameManager.startScreen.SetActive(false);
        gameManager.inGameUI.SetActive(true);
        gameManager.endScreen.SetActive(false);
        
        //generate game agents and player
        var playerGameObject = Object.Instantiate(Services.GameManager.prefab_GameAgent);
        Services.Players = new[] { new Player(playerGameObject) };
        Services.AIManager = new AIManager();
        Services.AIManager.Initialize();
    }

    public override void OnUpdate(GameManager gameManager)
    {
        //update game UI
        //update game agents
        Services.AIManager.Update();
        Services.GameManager.InGameTimer();
    }

    private void GameTimerEnd(object sender, EventArgs e)
    {
        Services.GameManager.SwitchState(Services.GameManager.StateEndGame);
        Services.EventManager.OnGameEnd -= GameTimerEnd;
    }
}
