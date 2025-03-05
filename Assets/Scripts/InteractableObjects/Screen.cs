using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public float InteractionDistance
    { get; set; }

    public bool IsInteractionEnabled
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteraction()
    {
        Debug.Log("Interaction Acheieved.");
    }
}
