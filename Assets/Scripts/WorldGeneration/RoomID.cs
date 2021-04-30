using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomID : MonoBehaviour
{
    private GameController _GameController;
    public int chestRoomPercent = 0;
    public int quantityEnemies = 5;
    public int maxQuantity = 20;
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public Transform[] chestSpawnpoints;
    public List<Portal> portals;
    public GameObject chest;
    public bool isChestRoom;
    public bool isInAttack;

    private void Start()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        int druglevel = _GameController.drugLevel;

        if (druglevel > 0)
        {
            quantityEnemies *= 1 + (druglevel / 2);
            if(quantityEnemies >= maxQuantity)
            {
                quantityEnemies = maxQuantity;
            }
        }

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < quantityEnemies; i++)
        {
            if (quantityEnemies > 0)
            {
                GameObject temp = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPoints[Random.Range(0, spawnPoints.Length
                    )].position, Quaternion.identity);
            }
        }
    }
}
