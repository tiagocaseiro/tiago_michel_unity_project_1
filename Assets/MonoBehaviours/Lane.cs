using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Lane : MonoBehaviour
{
    private Renderer _renderer;
    private LineRenderer _borderDrawer;
    private List<SpawnZone> _spawnZones;
    private float _halfLength = 10f;
    private float _halfWidth = 3f;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _borderDrawer = gameObject.AddComponent<LineRenderer>();
        _spawnZones = GetComponentsInChildren<SpawnZone>().ToList();
        _spawnZones.ForEach(sz => sz.ParentLane = this);
        _halfLength = _renderer.bounds.extents.y;
        _halfWidth = _renderer.bounds.extents.x;
    }

    void Start()
    {
        // #TODO_DEBUGGING: Why does this not work?
        DrawLane();
    }

    void DrawLane()
    {
        var corners = GetCorners();

        _borderDrawer.positionCount = corners.Length;
        _borderDrawer.useWorldSpace = true;
        _borderDrawer.loop = false;

        _borderDrawer.SetPositions(corners);
    }

    void OnDrawGizmos()
    {
        if (_borderDrawer != null)
        {
            DrawLane();
        }
    }

    Vector3[] GetCorners()
    {
        Vector3 forward = transform.up;
        Vector3 right = transform.right;

        Vector3 center = transform.position;

        Vector3 halfForward = forward * _halfLength;
        Vector3 halfRight = right * _halfWidth;

        Vector3 topLeft = center + halfForward - halfRight;
        Vector3 topRight = center + halfForward + halfRight;
        Vector3 bottomRight = center - halfForward + halfRight;
        Vector3 bottomLeft = center - halfForward - halfRight;

        return new Vector3[]
        {
            topLeft,
            topRight,
            bottomRight,
            bottomLeft,
            topLeft // close the loop
        };
    }
}