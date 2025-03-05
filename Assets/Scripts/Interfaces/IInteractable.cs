using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public float InteractionDistance
    { get; set; }

    public bool IsInteractionEnabled
    { get; set; }

    public abstract void OnInteraction();

    public static float GetInteractionDistance(Vector3 startLocation, Vector3 endLocation)
    {
        return Vector3.Distance(startLocation, endLocation);
    }

    /// <summary>
    /// Check if the interaction distance is less then the specified IInteractable InteractionDistance, and if so return true, else return false (also checks if the object allow interaction by default).
    /// </summary>
    /// <param name="distanceOfInteraction"></param>
    /// <returns></returns>
    public bool CheckIfInteractionAllowed(float distanceOfInteraction)
    {
        if (distanceOfInteraction <= InteractionDistance && IsInteractionEnabled)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
