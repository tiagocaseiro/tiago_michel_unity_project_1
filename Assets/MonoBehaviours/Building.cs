using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData StaticData;
    public Faction Owner;

    private SpriteRenderer _spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Owner.StaticData.FactionColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
