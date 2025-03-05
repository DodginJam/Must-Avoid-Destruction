using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Advisor", menuName = "Advisor")]
public class Advisor_SO : ScriptableObject
{
    public Crises_SO CurrentCrisis
    { get; set; }

    [field: SerializeField]
    public string Name
    { get; private set; }

    [field: SerializeField]
    public AdvisorRole TitleRole
    { get; private set; }

    public enum AdvisorRole
    {
        ChiefOfStaff,
        PersonalAdvisor,
        HeadOfArmedForces,
        IntelligenceChief,
        SecretaryOfStateForDefence,
        SecretaryOfStateForEnvironment,
        SecretaryOfStateForEnergy,
        DeputyLeader
    }
}
