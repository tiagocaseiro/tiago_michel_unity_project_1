using UnityEngine;
public enum TargetType
{
    Building,
    RegularUnit,
    GuardUnit,
    StealthUnit
}

[System.Serializable]
public class HealthData
{
    public TargetType targetType;
    public int maxHealth;
    public int extraPriority;
    
    public int GetPriority()
    {
        switch (targetType)
        {
            case TargetType.Building:
            case TargetType.RegularUnit:
                return extraPriority;
            case TargetType.GuardUnit:
                return extraPriority + 5;
            case TargetType.StealthUnit:
                return extraPriority - 5;
        }
        
        // Log a warning
        Debug.LogWarning("Unknown target type: " + targetType);
        return extraPriority;
    }
}
