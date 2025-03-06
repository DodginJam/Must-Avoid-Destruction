using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AlertLevels
{
    public int[] AlertLevelsArray
    { get; private set; } = new int[] { 1, 2, 3, 4, 5 };

    [field: SerializeField]
    public Color[] AlertLevelColours
    { get; private set; } = new Color[5];

    public Color CurrentColorDisplay
    { get; private set; }

    public int CurrentAlertLevel
    { get; private set; }

    /// <summary>
    /// Change the alert level by a given amount and then return the new alert level.
    /// </summary>
    /// <param name="incrementValue"></param>
    /// <returns></returns>
    public int ModifyAlertLevel(int incrementValue)
    {
        CurrentAlertLevel = Mathf.Clamp(CurrentAlertLevel + incrementValue, AlertLevelsArray[0], AlertLevelsArray[AlertLevelsArray.Length - 1]);
        return CurrentAlertLevel;
    }

    public void ModifyAlertLevelColour(int newAlertLevel)
    {

    }
}
