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

    public void GoldPickUp(int redOrBlue)
    {
        OnGoldPickedUp?.Invoke(this, new OnGoldPickedUpArgs {blueOrRed = redOrBlue});
    }
}
