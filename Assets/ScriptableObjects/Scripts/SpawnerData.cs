using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpawnWave
{
    public UnitType Type;
    public int Count;
}

[CreateAssetMenu(fileName = "Spawner", menuName = "Scriptable Objects/SpawnerData")]
public class SpawnerData : ScriptableObject
{
    public List<SpawnWave> SpawnWaves;
    public int Interval;
    public int NumSpawns = -1; // 0 = never spawn, -1 = repeat infinitely at the interval
}