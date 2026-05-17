using UnityEngine;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour
{
    public UnitType staticData;
    public Faction ownerFaction;
    public Lane presentLane;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _targetPosition;
    private float _moveSpeed = 0.5f;

    enum MovementState
    {
        JustSpawned,
        MovingToReserves,
        WaitingInReserves,
        MovingToFrontlines,
        Fighting
    }
    
    private MovementState _movementState = MovementState.JustSpawned;

    public void Init(Lane lane)
    {
        presentLane = lane;
        transform.position = _targetPosition;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }

    void Update()
    {
        if (presentLane is null)
        {
            return;
        }
        switch (_movementState)
        {
            case MovementState.JustSpawned:
                if (presentLane.GetReservesForUnit(this).TryAddUnit(this))
                {
                    presentLane.GetSpawnZoneForFaction(ownerFaction).RemoveUnit(this);
                    _movementState = MovementState.MovingToReserves;
                }

                if (!AtDestination())
                {
                    Advance();
                }
                break;
            case MovementState.MovingToReserves:
                Advance();
                if (AtDestination())
                {
                    _movementState = MovementState.WaitingInReserves;
                }
                break;
            case MovementState.WaitingInReserves:
                if (!AtDestination())
                {
                    Advance();
                }

                if (presentLane.GetFrontlineForUnit(this).TryAddUnit(this))
                {
                    presentLane.GetReservesForUnit(this).RemoveUnit(this);
                    _movementState = MovementState.MovingToFrontlines;
                }
                break;
            case MovementState.MovingToFrontlines:
                Advance();
                if (AtDestination())
                {
                    _movementState = MovementState.Fighting;
                }
                break;
            case MovementState.Fighting:
                if (!AtDestination())
                {
                    Advance();
                }
                break;
        }
    }

    void Advance()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetPosition,
            _moveSpeed * Time.deltaTime);
    }

    bool AtDestination()
    {
        return Vector3.Distance(transform.position, _targetPosition) <= 0.1f;
    }

    void SetColor()
    {
        Color baseColor = ownerFaction.StaticData.FactionColor;
        // Add a little spice
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);

        h += Random.Range(-0.02f, 0.02f);
        s += Random.Range(-0.05f, 0.05f);
        v += Random.Range(-0.05f, 0.05f);

        Color newColor = Color.HSVToRGB(h, s, v);
        _spriteRenderer.color = newColor;
    }

    public void SetTargetPosition(Vector3 position)
    {
        _targetPosition = position;
    }
}
