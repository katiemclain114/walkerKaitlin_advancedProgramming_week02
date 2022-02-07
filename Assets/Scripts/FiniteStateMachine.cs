using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiniteStateMachine
{
    public abstract void OnEnter(GameManager gameManager);

    public abstract void OnUpdate(GameManager gameManager);
}
