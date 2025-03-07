using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState_AwaitPlayerAnswer : GameState_Base
{
    public override void EnterState(GameManager gameManager)
    {
        gameManager.CurrentCrisis.SelectedResolution = null;
    }

    public override void UpdateState(GameManager gameManager)
    {
        if (gameManager.CurrentCrisis.SelectedResolution == null)
        {
            // Countdown timer here?!
        }
        else if (gameManager.CurrentCrisis.SelectedResolution != null)
        {
            // Pass the resolution information to the GameManager.
            gameManager.CurrentCrisis.HasBeenResolved = gameManager.CurrentCrisis.CalculateAcceptableSolution(gameManager.CurrentCrisis.SelectedResolution, gameManager.CurrentCrisis.Negotiator);

            // Set the screens to be no-more interactable.
            IInteractable.EnableInteraction(gameManager.ScreenDisplays.ScreenOptionDisplays.ToArray(), false);

            Debug.Log($"Successfully selected current solution: {gameManager.CurrentCrisis.SelectedResolution.Title}\nHas been resolved?: {gameManager.CurrentCrisis.HasBeenResolved}");

            // Switch state.
            gameManager.SwitchState(gameManager.AllGameStates.ProcessAnswer);
        }
    }

    public override void ExitState(GameManager gameManager)
    {
        gameManager.CurrentCrisis.SelectedResolution = null;
    }
}
