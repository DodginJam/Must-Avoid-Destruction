using UnityEngine;

public class Advisor : MonoBehaviour, IInteractable
{
    [field: SerializeField]
    public Advisor_SO AdvisorScriptableObject
    {  get; private set; }

    public bool IsInteractionEnabled
    { get; set; } = true;

    [field: SerializeField]
    public float InteractionDistance
    { get; set; } = 20f;


    [field: SerializeField]
    public Material HighLightMaterial
    { get; set; }

    public Material DefaultMaterial
    { get; set; }

    public Animator Animator
    { get; set; }

    public void Awake()
    {
        if (AdvisorScriptableObject == null)
        {
            Debug.LogError("Must assign a advisor_SO to an NPC");
        }

        if (Animator == null)
        {
            Animator = GetComponentInChildren<Animator>();
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
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("wave") && !Animator.IsInTransition(0))
        {
            Animator.SetTrigger("wave");
        }

        Debug.Log($"Interacted with {AdvisorScriptableObject.Name}, who's role is {AdvisorScriptableObject.TitleRole}");
    }

    public void OnMouseEnter()
    {

    }

    public void OnMouseExit()
    {

    }

    public void OnMouseOver()
    {
        
    }
}
