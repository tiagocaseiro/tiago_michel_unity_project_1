using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Faction : MonoBehaviour
{
    public FactionType StaticData;
    [HideInInspector]
    public List<SpawnZone> SpawnZones = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     var allLanes = GameManager.Instance.AllLanes;
    //     foreach (var lane in allLanes)
    //     {
    //         SpawnZones.Add(lane.GetSpawnZoneForFaction(this));
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTroops()
    {
    }
}
