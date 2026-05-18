using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using MonoBehaviours;

public class Lane : GameZone
{
    [Serializable]
    struct PlayerZone
    {
        public Faction ownerFaction;
        public SpawnZone spawnZone;
        public UnitHoldingZone reserveZone;
        public UnitHoldingZone frontlineZone;
    }

    [SerializeField] private List<PlayerZone> _playerZones;
    [SerializeField] private List<Building> _nearbyBuildings;
    // #TODO_NEXT: Units and buildings need to create health components from their health datas
    // Then lanes need to register nearby buildings + units in fighting zones' health components 
    // Making all these things targetable
    private List<HealthComponent> _targetsInLane;

    // void Start()
    // {
    //     base.Start();
    //     foreach (PlayerZone playerZone in _playerZones)
    //     {
    //         playerZone.frontlineZone.parentZone = this;
    //         playerZone.reserveZone.parentZone = this;
    //         playerZone.spawnZone.parentZone = this;
    //     }
    // }

    public SpawnZone GetSpawnZoneForFaction(Faction faction)
    {
        return _playerZones.First(pz => pz.ownerFaction == faction).spawnZone;
    }
    
    public UnitHoldingZone GetReservesForUnit(Unit unit)
    {
        Faction unitFaction = unit.ownerFaction;
        PlayerZone unitZones = _playerZones.First(pz => pz.ownerFaction == unitFaction);
        return unitZones.reserveZone;
    }

    public UnitHoldingZone GetFrontlineForUnit(Unit unit)
    {
        Faction unitFaction = unit.ownerFaction;
        PlayerZone unitZones = _playerZones.First(pz => pz.ownerFaction == unitFaction);
        return unitZones.frontlineZone;
    }
}