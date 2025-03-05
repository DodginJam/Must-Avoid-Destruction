using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advisor : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public Advisor_SO AdvisorScriptableObject
    {  get; private set; }

    public bool IsInteractionEnabled
    { get; set; }

    [field: SerializeField]
    public float InteractionDistance
    { get; set; } = 20f;

    public void Awake()
    {
        if (AdvisorScriptableObject == null)
        {
            Debug.LogError("Must assign a advisor_SO to an NPC");
        }
    }

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
        Debug.Log($"Interacted with {AdvisorScriptableObject.Name}, who's role is {AdvisorScriptableObject.TitleRole}");
    }
}
