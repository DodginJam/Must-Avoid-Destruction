using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public interface IDisplayable
{
    public TextMeshProUGUI TitleTextDisplay
    { get; set; }

    public TextMeshProUGUI DescriptionTextDisplay
    { get; set; }

    public MeshRenderer DisplayMeshRenderer
    { get; set; }

    public void ToggleDisplay(bool setActiveState);

    public void DisplayText<T>(T crisisOrResolutionOption);

    public void ClearText();
}
