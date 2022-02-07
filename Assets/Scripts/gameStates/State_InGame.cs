using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_InGame : FiniteStateMachine
{
    public override void OnEnter(GameManager gameManager)
    {
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
        //preform game timer stuff
        if (gameManager.InGameTimer())
        {
            gameManager.SwitchState(gameManager.StateEndGame);
        }
    }
}
