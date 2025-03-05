using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class DisplayOptions : MonoBehaviour
{
    [field: SerializeField]
    public GameManager GameManagerScript
    {  get; private set; }

    [field: SerializeField]
    public List<ScreenOptionDisplay> ScreenDisplays
    { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayResolutionOptions(ResolutionOption[] resolutionOptions)
    {
        // For each resolution options.
        // Chose the one of the screens in order, place information about that resolutionn options.
        // Enable it to be interactable.
    }
}
