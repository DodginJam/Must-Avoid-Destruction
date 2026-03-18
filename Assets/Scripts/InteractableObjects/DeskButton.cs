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

    public MeshRenderer[] PartsMeshRenderer
    { get; set; }

    public Material[] PartsDefaultMaterials
    { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsInteractionEnabled = true;

        PartsMeshRenderer = GetComponentsInChildren<MeshRenderer>();
        PartsDefaultMaterials = new Material[PartsMeshRenderer.Length];

        for (int i = 0; i < PartsMeshRenderer.Length; i++)
        {
            PartsDefaultMaterials[i] = PartsMeshRenderer[i].material;
        }

        // Being assigned but not used at moment due to need to rework interactiable interface.
        DefaultMaterial = PartsMeshRenderer[0].material;
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
            for (int i = 0; i < PartsMeshRenderer.Length; i++)
            {
                IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], HighLightMaterial);
            }
        }
    }

    public void OnMouseExit()
    {
        if (IsInteractionEnabled)
        {
            for (int i = 0; i < PartsMeshRenderer.Length; i++)
            {
                IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], PartsDefaultMaterials[i]);
            }
        }
    }

    public void OnMouseOver()
    {
        if (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), transform.position) <= InteractionDistance)
        {
            if (IsInteractionEnabled)
            {
                foreach (MeshRenderer mesh in PartsMeshRenderer)
                {
                    IInteractable.ChangeInteractableMaterial(mesh, HighLightMaterial);
                }
            }
            else
            {
                for (int i = 0; i < PartsMeshRenderer.Length; i++)
                {
                    IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], PartsDefaultMaterials[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < PartsMeshRenderer.Length; i++)
            {
                IInteractable.ChangeInteractableMaterial(PartsMeshRenderer[i], PartsDefaultMaterials[i]);
            }
        }
    }
}
