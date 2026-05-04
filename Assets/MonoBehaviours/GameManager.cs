using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// #TODO_UTIL: Import serializable singleton from Mappy
public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance = null;
    private static readonly object padlock = new object();

    GameManager()
    {
    }

    public static GameManager Instance
    {
        get
        {
            lock (padlock)
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
    }
    #endregion

    // #TODO_NEXT: This shouldn't be a raw box collider
    // Make GameZone a concept
    // Make Lane a GameZone, + the bases
    // GameZone can handle rendering itself + maybe being nested in another game zone
    // This will be useful later for cards
    // But anyway, even more useful: Then units can know which zone they're in, and can enter or exit it accordingly
    public List<BoxCollider2D> GameZones;

    //public List<Faction> Factions = new();

    //public float SpawnInterval = 30f; // in seconds
    //private float _timer;

    //void Update()
    //{
    //    _timer += Time.deltaTime;

    //    if (_timer >= SpawnInterval)
    //    {
    //        TickSpawns();
    //        _timer = 0;
    //    }
    //}

    //void TickSpawns()
    //{
    //    foreach (var faction in Factions)
    //    {
    //        faction.SpawnTroops();
    //    }
    //}
}
