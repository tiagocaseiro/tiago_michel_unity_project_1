using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitType StaticData;
    public Faction OwnerFaction;
    public GameObject CurrentZone;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }

    void SetColor()
    {
        Color baseColor = OwnerFaction.StaticData.FactionColor;
        // Add a little spice
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);

        h += Random.Range(-0.02f, 0.02f);
        s += Random.Range(-0.05f, 0.05f);
        v += Random.Range(-0.05f, 0.05f);

        Color newColor = Color.HSVToRGB(h, s, v);
        _spriteRenderer.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
