using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SpawnZone : MonoBehaviour
{
    public Faction Owner;
    [HideInInspector]
    public Lane ParentLane;

    void Awake()
    {
        ParentLane = GetComponentInParent<Lane>();
    }

    void Start()
    {
        Owner.SpawnZones.Add(this);
    }

    void Update()
    {
        
    }

    public void SpawnTroops()
    {
    }
}
