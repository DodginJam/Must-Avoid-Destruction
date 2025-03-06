using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class GameState_StartGame : GameState_Base
{
    [field: SerializeField]
    public DeskButton InteractiveButtonToStart
    {  get; private set; }

    public override void EnterState(GameManager gameManager)
    {
        gameManager.SetAlertStatus(gameManager.StartingAlertLevel);

        if (InteractiveButtonToStart != null)
        {
            InteractiveButtonToStart.IsInteractionEnabled = true;
        }
        else
        {
            Debug.LogError("Unable to start game without an interactive start button.");
        }
    }

    public override void UpdateState(GameManager gameManager)
    {

    }

    public override void ExitState(GameManager gameManager)
    {

    }
}
