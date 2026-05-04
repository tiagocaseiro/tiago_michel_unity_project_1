using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner
{
    public SpawnerData StaticData;
    private int _spawnsLeft;
    private float _timeElapsed;

    public Spawner(SpawnerData data)
    {
        StaticData = data;
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
        if (_timeElapsed >= StaticData.Interval)
        {
            _timeElapsed = 0f;
            if (_spawnsLeft > 0)
            {
                _spawnsLeft--;
            }

            foreach (var spawnWave in StaticData.SpawnWaves)
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

        foreach (var unitType in unitsToSummonThisTurn)
        {
            Unit summonablePrefab = SpawnableUnitPrefabs.First(sup => sup.StaticData == unitType);
            summonablePrefab.OwnerFaction = OwnerFaction;
            SpawnZone zoneToSpawnIn = OwnerFaction.SpawnZones[Random.Range(0, OwnerFaction.SpawnZones.Count)];
            Instantiate(summonablePrefab, zoneToSpawnIn.GetSpawnPosition(), Quaternion.identity);
        }
    }
}
