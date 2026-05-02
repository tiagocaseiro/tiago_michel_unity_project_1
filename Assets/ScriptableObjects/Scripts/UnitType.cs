using UnityEngine;

[CreateAssetMenu(fileName = "UnitType", menuName = "Scriptable Objects/UnitType")]
public class UnitType : ScriptableObject
{
    public string DisplayName;
    
    public int Speed;
    public int MaxHealth;
    public int AttackValue;
    public int AttackSpeed;
    public int AttackRange;
    
    public Sprite Icon;
}
