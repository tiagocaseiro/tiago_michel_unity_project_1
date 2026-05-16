using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner
{
    private SpawnerData _staticData;
    private int _spawnsLeft;
    private float _timeElapsed;

    public Spawner(SpawnerData data)
    {
        _staticData = data;
        _spawnsLeft = data.NumSpawns;
        _timeElapsed = 0;
    }

    public List<UnitType> UnitsToSpawn(float deltaTime)
    {
        List<UnitType> result = new();
        
        if (_spawnsLeft == 0)
        {
            return result;
        }
        
        _timeElapsed += deltaTime;
        if (_timeElapsed >= _staticData.Interval)
        {
            _timeElapsed = 0f;
            if (_spawnsLeft > 0)
            {
                _spawnsLeft--;
            }

            foreach (var spawnWave in _staticData.SpawnWaves)
            {
                for (int i = 0; i < spawnWave.Count; i++)
                {
                    result.Add(spawnWave.Type);
                }
            }
        }
        
        return result;
    }
}

public class FactionSpawner : MonoBehaviour
{
    public Faction OwnerFaction;
    [HideInInspector]
    public List<Spawner> Spawners;
    public List<Unit> SpawnableUnitPrefabs;

    private int _timeElapsed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawners = new();
        foreach (var spawnerData in OwnerFaction.StaticData.BaseSpawns)
        {
            Spawners.Add(new(spawnerData));
        }
    }

    // Update is called once per frame
    void Update()
    {
        var unitsToSummonThisTurn = new List<UnitType>();
        foreach (var spawner in Spawners)
        {
            unitsToSummonThisTurn.AddRange(spawner.UnitsToSpawn(Time.deltaTime));
        }

        var availableSpawnZones = OwnerFaction.SpawnZones.Where(sz => !sz.IsFull).ToList();
        List<Tuple<Unit, SpawnZone>> spawnedUnits  = new();
        foreach (var unitType in unitsToSummonThisTurn)
        {
            if (availableSpawnZones.Count == 0)
            {
                break;
            }
            Unit summonablePrefab = SpawnableUnitPrefabs.First(sup => sup.staticData == unitType);
            summonablePrefab.ownerFaction = OwnerFaction;
            SpawnZone zoneToSpawnIn = availableSpawnZones[Random.Range(0, availableSpawnZones.Count)];
            Unit spawnedUnit = Instantiate(summonablePrefab, zoneToSpawnIn.transform.position, Quaternion.identity);
            if (zoneToSpawnIn.TryAddUnit(spawnedUnit))
            {
                spawnedUnits.Add(new(spawnedUnit, zoneToSpawnIn));
                if (zoneToSpawnIn.IsFull)
                {
                    availableSpawnZones.Remove(zoneToSpawnIn);
                }
            }
        }
        spawnedUnits.ForEach(kvp => kvp.Item1.Init(kvp.Item2.parentLane));
    }
}
