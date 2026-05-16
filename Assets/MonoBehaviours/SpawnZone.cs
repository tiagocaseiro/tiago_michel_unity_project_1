using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using MonoBehaviours;

public class SpawnZone : UnitHoldingZone
{
    public Faction Owner;
    public Lane parentLane => parentZone as Lane;

    private List<Vector3> _lastChosenSpawnPositions;
    
    protected override void Start()
    {
        base.Start();
        Owner.SpawnZones.Add(this);
    }
    
    public Vector3 GetSpawnPosition()
    {
        while (true)
        {
            Vector3 spawnPos = GenerateSpawnPosition();
            if (!IsNewSpawnPositionTooCloseToExistingSpawnPositions(spawnPos))
            {
                _lastChosenSpawnPositions.Add(spawnPos);
                if (_lastChosenSpawnPositions.Count > 10)
                {
                    _lastChosenSpawnPositions.RemoveAt(0);
                }
                return spawnPos;
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 forward = transform.up;
        Vector3 right = transform.right;
        Vector3 center = transform.position;

        Vector3 halfForward = forward * _halfLength;
        Vector3 halfRight = right * _halfWidth;
        
        float randomX = Random.Range(center.x - halfRight.x, center.x + halfRight.x);
        float randomY = Random.Range(center.y - halfForward.y, center.y + halfForward.y);

        return new Vector3(randomX, randomY, center.z - 1);
    }

    public bool IsNewSpawnPositionTooCloseToExistingSpawnPositions(Vector3 newSpawnPosition)
    {
        return _lastChosenSpawnPositions.Any(sp => Vector3.Distance(sp, newSpawnPosition) <= 0.03f);
    }
}
