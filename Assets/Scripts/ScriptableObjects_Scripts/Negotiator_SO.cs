using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Negotiator", menuName = "Negotiator")]
public class Negotiator_SO : ScriptableObject
{
    [field: SerializeField]
    public string NegotiatorName
    { get; set; }

    [field: SerializeField]
    public Image Picture
    { get; set; }

    /// <summary>
    /// The seriousness of a response the negotiator will respond with on failure i.e. the next crisis servivity.
    /// </summary>
    public enum MilitantLevel
    {
        Low,
        Medium, 
        High
    }

    /// <summary>
    /// The seriousness of a response the negotiator will respond with i.e. the next crisis servivity.
    /// </summary>
    [field: SerializeField]
    public MilitantLevel MilitantTendency 
    { get; set; }

    /// <summary>
    /// The job role influences the type of resolution that they prefer and confers benefits or disadvantages.
    /// </summary>
    public enum JobRole
    {
        Diplomat,
        Military,
        Politician,
        None_UsedForCrisisResolutionOnly
    }

    /// <summary>
    /// The job role influences the type of resolution that they prefer and confers benefits or disadvantages.
    /// </summary>
    [field: SerializeField]
    public JobRole Position
    { get; set; }

    [field: SerializeField]
    public JobRole DislikePositions
    { get; set; }

    public string ReturnInformationDescription()
    {
        string firstName = NegotiatorName.Split(' ')[0];

        string jobDescription = $"Reports indicate that {firstName} holds the position of {Position.ToString()} within the enemy government. ";

        if (Position == JobRole.Diplomat)
        {
            jobDescription += "Diplomatic means would be the best approuch for such an individual.";
        }
        else if (Position == JobRole.Military)
        {
            jobDescription += "Believing in might makes right, they will respect the awe of military power.";
        }
        else if (Position == JobRole.Politician)
        {
            jobDescription += "Seen as a keen ladder climber, any political solution that leads to their benefit would make a agreement easier.";
        }

        string disLikeDescription = string.Empty;

        if (DislikePositions == JobRole.Diplomat)
        {
            jobDescription += "They are not keen on diplomatic solutions and respect other methods.";
        }
        else if (DislikePositions == JobRole.Military)
        {
            jobDescription += "A distrust towards those weilding the guns means little thought is given to military means.";
        }
        else if (DislikePositions == JobRole.Politician)
        {
            jobDescription += "This person has no respect for political intrigue so a wide berth from such matters would be wise.";
        }

        string militantDescription = string.Empty;

        if (MilitantTendency == MilitantLevel.Low)
        {
            militantDescription = "They have been identified as a potential sympathiser within the enemy regime and will not be as harsh as other members of government in response to escalation.";
        }
        else if (MilitantTendency == MilitantLevel.Medium)
        {
            militantDescription = "They have been seen to often support rhetoric in punitive action in response to threats that are deemed aggressive or weak.";
        }
        else if (MilitantTendency == MilitantLevel.High)
        {
            militantDescription = "Seen as a fire-brand, this extremist is known for severse escalation and provocation against weak or overly aggressive state.";
        }

        string crisisRespectAndDislikeDescription = string.Empty;

        return $"{jobDescription} \n{disLikeDescription} \n{militantDescription} \n{crisisRespectAndDislikeDescription}";
    }
}
