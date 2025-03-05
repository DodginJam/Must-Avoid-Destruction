using System.Collections;
using System.Collections.Generic;
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
}
