using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public string DisplayName;
    public int MaxHealth;
    public bool IsCritical;
    public Sprite Shape;
}