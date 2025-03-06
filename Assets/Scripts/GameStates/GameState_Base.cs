using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState_Base
{
    public abstract void EnterState(GameManager gameManager);

    public abstract void UpdateState(GameManager gameManager);

    public abstract void ExitState(GameManager gameManager);
}
