using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/BuildingData")]
public class BuildingData : ScriptableObject
{
    public string displayName;
    public HealthData healthData;
}