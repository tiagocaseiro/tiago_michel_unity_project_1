using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData StaticData;
    public Faction Owner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        List<SpriteRenderer> spriteRenderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        foreach (var spriteRenderer in spriteRenderers)
        {
            if (spriteRenderer.color != Color.white)
            {
                spriteRenderer.color = Owner.StaticData.FactionColor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
