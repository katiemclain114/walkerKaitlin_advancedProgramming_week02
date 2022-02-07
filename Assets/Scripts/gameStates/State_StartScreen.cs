using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_StartScreen : FiniteStateMachine
{
    public override void OnEnter(GameManager gameManager)
    {
        Debug.Log("hello from the startScreen");
        gameManager.startScreen.SetActive(true);
        gameManager.inGameUI.SetActive(false);
        gameManager.endScreen.SetActive(false);
    }

    public override void OnUpdate(GameManager gameManager)
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("clicked a button\nstart game");
            gameManager.SwitchState(gameManager.StateInGame);
        }
    }
}
