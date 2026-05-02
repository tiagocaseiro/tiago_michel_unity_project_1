using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "FactionType", menuName = "Scriptable Objects/FactionType")]
public class FactionType : ScriptableObject
{
    public string DisplayName;
    public string Adjective;
    public Color FactionColor;
    public List<SpawnerData> BaseSpawns;
    
    public Sprite Flag;
}
