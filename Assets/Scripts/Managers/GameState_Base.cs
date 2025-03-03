using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState_Base
{
    protected abstract void EnterState(GameManager gameManager);

    protected abstract void UpdateState(GameManager gameManager);

    protected abstract void ExitState(GameManager gameManager);
}
