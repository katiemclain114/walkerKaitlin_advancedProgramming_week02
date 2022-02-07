using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_EndGame : FiniteStateMachine
{
    public override void OnEnter(GameManager gameManager)
    {
        Debug.Log("hello end screen");
        gameManager.startScreen.SetActive(false);
        gameManager.inGameUI.SetActive(false);
        gameManager.endScreen.SetActive(true);
        
        Services.AIManager.DestroyAllObjects();
    }

    public override void OnUpdate(GameManager gameManager)
    {
        if (Input.anyKeyDown)
        {
            gameManager.SwitchState(gameManager.StateStartScreen);
        }
    }
}
