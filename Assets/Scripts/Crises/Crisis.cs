using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crisis", menuName = "Crisis")]
public class Crises : ScriptableObject
{
    public Negotiator Negotiator
    { get; set; }

    [field: SerializeField]
    public string CrisisTitle
    { get; set; }


    [field: SerializeField]
    public string CrisisDescription
    { get; set; }

    /// <summary>
    /// The seriousnes of the crisis - has a multiplication effect on alert level on faliure to resolve.
    /// </summary>
    public enum Severity
    {
        Low,
        Medium, 
        High,
        MAD
    }

    [field: SerializeField]
    public Severity SeverityLevel
    { get; set; }

    public bool HasBeenResolved
    { get; set; }

    public bool CalculateAcceptableSolution(ResolutionOption option, Negotiator negotiator)
    {
        int solutionScore = (int)option.ResolutionLevel;

        int negotiatorBonusScore = negotiator.SeniorityPosition == option.JobRoleAppeal ? 1 : 0;
        int negotiatorNegativeScore = negotiator.SeniorityPosition == option.JobRoleDisgust ? -1 : 0;

        solutionScore += negotiatorBonusScore + negotiatorNegativeScore;

        int solutionGoal = (int)SeverityLevel;

        if (solutionScore >= solutionGoal)
        {
            HasBeenResolved = true;
            return true;
        }
        else
        {
            HasBeenResolved = false;
            return false;
        }
    }

    [Serializable]
    public class ResolutionOption
    {
        [field: SerializeField]
        public string Title
        { get; set; }

        [field: SerializeField]
        public string Detail
        { get; set; }

        public bool HasBeenSelected
        {  get; set; }

        public enum LevelOfResolution
        {  
            Low, 
            Medium, 
            High,
            Complete
        }

        [field: SerializeField]
        public LevelOfResolution ResolutionLevel
        { get; set; }

        [field: SerializeField]
        public Negotiator.JobRole JobRoleAppeal
        { get; set; }

        [field: SerializeField]
        public Negotiator.JobRole JobRoleDisgust
        { get; set; }
    }

    [field: SerializeField]
    public ResolutionOption[] OptionsForResolution
    { get; set; }
}
