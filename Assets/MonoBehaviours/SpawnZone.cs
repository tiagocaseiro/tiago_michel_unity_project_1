using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SpawnZone : MonoBehaviour
{
    public Faction Owner;
    [HideInInspector]
    public Lane ParentLane;
    private float _length = 3f;
    private float _width = 3f;

    private List<Vector3> _lastChosenSpawnPositions;

    void Awake()
    {
        ParentLane = GetComponentInParent<Lane>();
        _lastChosenSpawnPositions = new List<Vector3>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        _length = renderer.bounds.extents.y;
        _width = renderer.bounds.extents.x;
    }

    void Start()
    {
        Owner.SpawnZones.Add(this);
    }

    void Update()
    {
        
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

        Vector3 halfForward = forward * (_length / 2f);
        Vector3 halfRight = right * (_width / 2f);
        
        float randomX = Random.Range(center.x - halfRight.x, center.x + halfRight.x);
        float randomY = Random.Range(center.y - halfForward.y, center.y + halfForward.y);

        return new Vector3(randomX, randomY, center.z - 1);
    }

    public bool IsNewSpawnPositionTooCloseToExistingSpawnPositions(Vector3 newSpawnPosition)
    {
        return _lastChosenSpawnPositions.Any(sp => Vector3.Distance(sp, newSpawnPosition) <= 0.03f);
    }
}
