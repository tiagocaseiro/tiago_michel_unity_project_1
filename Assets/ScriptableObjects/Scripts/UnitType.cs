using UnityEngine;

public enum TargeterType
{
    BuildingFavourer,
    Neutral,
    UnitFavourer
}

[CreateAssetMenu(fileName = "UnitType", menuName = "Scriptable Objects/UnitType")]
public class UnitType : ScriptableObject
{
    public string displayName;
    
    public int speed;
    public HealthData health;
    public TargeterType targeterType;
    public int attackDamage;
    public float attackIntervalSecs;
}
