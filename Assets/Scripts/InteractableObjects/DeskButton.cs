using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskButton : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public float InteractionDistance
    { get; set; }

    public bool IsInteractionEnabled
    { get; set; }

    public MeshRenderer[] Parts
    { get; set; }

    [field: SerializeField]
    public Material HighLightMaterial
    { get; set; }

    public Material DefaultMaterial
    { get; set; }

    [field: SerializeField]
    public GameManager GameManagerScript
    { get; set; }

    [field: SerializeField]
    public AudioSource AudioSource 
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsInteractionEnabled = true;

        Parts = GetComponentsInChildren<MeshRenderer>();

        DefaultMaterial = Parts[0].material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// On button press, if the game is at it's starting point, then switch the game state to the first problem.
    /// </summary>
    public void OnInteraction()
    {
        if (GameManagerScript.CurrentGameState == GameManagerScript.AllGameStates.StartGame)
        {
            GameManagerScript.SwitchState(GameManagerScript.AllGameStates.DisplayProblem);
            IsInteractionEnabled = false;
            AudioSource.Play();
        }
    }

    public void OnMouseEnter()
    {
        if (IsInteractionEnabled && Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) <= InteractionDistance)
        {
            foreach (MeshRenderer mesh in Parts)
            {
                IInteractable.ChangeInteractableMaterial(mesh, HighLightMaterial);
            }
        }
    }

    public void OnMouseExit()
    {
        if (IsInteractionEnabled)
        {
            foreach (MeshRenderer mesh in Parts)
            {
                IInteractable.ChangeInteractableMaterial(mesh, DefaultMaterial);
            }
        }
    }

    public void OnMouseOver()
    {
        if (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) <= InteractionDistance)
        {
            if (IsInteractionEnabled)
            {
                foreach (MeshRenderer mesh in Parts)
                {
                    IInteractable.ChangeInteractableMaterial(mesh, HighLightMaterial);
                }
            }
            else
            {
                foreach (MeshRenderer mesh in Parts)
                {
                    IInteractable.ChangeInteractableMaterial(mesh, DefaultMaterial);
                }
            }
        }
        else
        {
            foreach (MeshRenderer mesh in Parts)
            {
                IInteractable.ChangeInteractableMaterial(mesh, DefaultMaterial);
            }
        }
    }
}
