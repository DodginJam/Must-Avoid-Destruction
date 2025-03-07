using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static Crises_SO;

public class NamePlates : MonoBehaviour, IDisplayable
{
    [field: SerializeField]
    public TextMeshProUGUI TitleTextDisplay
    { get; set; }

    [field: SerializeField]
    public TextMeshProUGUI DescriptionTextDisplay
    { get; set; }

    [field: SerializeField]
    public MeshRenderer DisplayMeshRenderer
    { get; set; }

    [field: SerializeField]
    public Advisor AdvisorReference
    { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DisplayText(AdvisorReference.AdvisorScriptableObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDisplay(bool setActiveState)
    {
        TitleTextDisplay.enabled = setActiveState;
        DescriptionTextDisplay.enabled = setActiveState;
    }

    public void DisplayText<T>(T type)
    {
        Advisor_SO option = null;
        if (type != null)
        {
            option = type as Advisor_SO;
        }
        else
        {
            Debug.LogError("Error");
        }

        if (option != null)
        { 
            TitleTextDisplay.text = option.Name;
            DescriptionTextDisplay.text = option.TitleRole.ToString();

            // ChatGPT helped with the adding of spaces when an interal capital letter is detected.
            StringBuilder result = new StringBuilder();

            foreach (char c in DescriptionTextDisplay.text)
            {
                if (char.IsUpper(c) && result.Length > 0)
                {
                    result.Append(' ');
                }
                result.Append(c);
            }

            DescriptionTextDisplay.text = result.ToString();
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    public void ClearText()
    {
        TitleTextDisplay.text = string.Empty;
        DescriptionTextDisplay.text = string.Empty;
    }

}
