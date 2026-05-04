using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitType StaticData;
    public Faction OwnerFaction;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = OwnerFaction.StaticData.FactionColor;
        Vector3 colorVariation = new Vector3(
            Random.Range(0.75f, 1.25f),
            Random.Range(0.75f, 1.25f),
            Random.Range(0.75f, 1.25f));
        _spriteRenderer.color *= colorVariation;
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f); // #TODO: Find out why this is needed
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
