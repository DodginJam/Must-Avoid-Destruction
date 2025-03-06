using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public abstract class ScreenDisplay : MonoBehaviour, IDisplayable
{
    [field: SerializeField]
    public TextMeshProUGUI TitleTextDisplay
    { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI DescriptionTextDisplay
    { get; set; }

    [field: SerializeField]
    public MeshRenderer DisplayMeshRenderer
    { get; set; }

    public abstract void DisplayText<T>(T type);

    public void ClearText()
    {
        TitleTextDisplay.text = string.Empty;
        DescriptionTextDisplay.text = string.Empty;
    }

    public void ToggleDisplay(bool setActiveState)
    {
        if (setActiveState)
        {
            DisplayMeshRenderer.material.EnableKeyword("_EMISSION");
        }
        else
        {
            DisplayMeshRenderer.material.DisableKeyword("_EMISSION");
        }

        TitleTextDisplay.enabled = setActiveState;
        DescriptionTextDisplay.enabled = setActiveState;
    }
}
