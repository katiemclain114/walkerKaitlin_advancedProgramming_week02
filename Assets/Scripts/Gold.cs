using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Contains("GameAgent")) return;

        var teamID = int.Parse(other.tag.Split('_')[1]);
        Services.AIManager.DestroyGold(this);
    }
}
