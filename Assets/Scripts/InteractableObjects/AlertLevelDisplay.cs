using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class AlertLevelDisplay : ScreenDisplay
{
    public override void DisplayText<T>(T type)
    {
        AlertLevels alertLevels = null;
        if (type != null)
        {
            alertLevels = type as AlertLevels;
        }
        else
        {
            Debug.LogError("Error");

        }

        if (alertLevels != null)
        {
            DescriptionTextDisplay.text = alertLevels.CurrentAlertLevel.ToString();
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}
