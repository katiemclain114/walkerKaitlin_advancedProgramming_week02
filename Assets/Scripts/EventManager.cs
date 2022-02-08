using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventManager
{
    public event EventHandler<OnGoldPickedUpArgs> OnGoldPickedUp;

    public class OnGoldPickedUpArgs : EventArgs
    {
        public int blueOrRed;
    }

    public event EventHandler OnGameStart;
    public event EventHandler OnGameEnd;

    public void GoldPickUp(int redOrBlue)
    {
        OnGoldPickedUp?.Invoke(this, new OnGoldPickedUpArgs {blueOrRed = redOrBlue});
    }

    public void GameStart()
    {
        OnGameStart?.Invoke(this, EventArgs.Empty);
    }

    public void GameTimeOut()
    {
        OnGameEnd?.Invoke(this, EventArgs.Empty);
    }
}
