using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NegotiatorDisplay : ScreenDisplay
{
    public Negotiator_SO NegotiatorDisplayed
    { get; private set; }

    public override void DisplayText<T>(T type)
    {
        Negotiator_SO negotiator = null;
        if (type != null)
        {
            negotiator = type as Negotiator_SO;
        }
        else
        {
            Debug.LogError("Error");

        }

        if (negotiator != null)
        {
            TitleTextDisplay.text = negotiator.NegotiatorName;
            DescriptionTextDisplay.text = negotiator.ReturnInformationDescription();
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    public void DisplayNegotiatorInformation(Negotiator_SO negotiator)
    {
        // Null check the parameter passed.
        if (negotiator == null)
        {
            Debug.LogError("The negotiator passed as a parameter is null.");
            return;
        }

        // Display the negotiator information to the relevent display screen and store the reference to the negotiator the screen display script.
        DisplayText(negotiator);
        NegotiatorDisplayed = negotiator;

        if (NegotiatorDisplayed != null && NegotiatorDisplayed == negotiator)
        {
            Debug.Log("Successful assignment of Negotiator to NegotiatorDisplay script");
        }
        else
        {
            Debug.LogError("Error in assignment of Negotiator to NegotiatorDisplay script");
        }
    }
}
