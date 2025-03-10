using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class ScreenTitleDisplay : ScreenDisplay
{
    public override void DisplayText<T>(T type)
    {
        Crises_SO crisis = null;
        if (type != null)
        {
            crisis = type as Crises_SO;
        }
        else
        {
            Debug.LogError("Error");

        }

        if (crisis != null)
        {
            TitleTextDisplay.text = crisis.CrisisTitle;
            DescriptionTextDisplay.text = crisis.CrisisDescription;
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}
