using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverChild : MonoBehaviour
{
    public IInteractable ParentObject
    {  get; private set; }

    public void Awake()
    {
        ParentObject = transform.parent.GetComponent<IInteractable>();
    }

    public void OnMouseEnter()
    {
        ParentObject.OnMouseEnter();
    }

    public void OnMouseExit()
    {
        ParentObject.OnMouseExit();
    }

    public void OnMouseOver()
    {
        ParentObject.OnMouseOver();
    }
}
