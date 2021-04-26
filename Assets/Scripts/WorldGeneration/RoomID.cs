using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomID : MonoBehaviour
{
    public int chestRoomPercent = 0;
    public int quantityEnemies = 5;
    public GameObject[] compatibleRooms;
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public Transform[] chestSpawnpoints;
    public List<Portal> portals;
    public GameObject chest;
    public bool isChestRoom;
    public bool isInAttack;
    public int currentQuantityEnemies;

    private void Start()
    {
        SpawnEnemies();
    }

    private void Update()
    {
        if (!isChestRoom && quantityEnemies <= 0 && isInAttack)
        {
            isInAttack = false;

            for (int i = 0; i < portals.Count; i++)
            {
                portals[i].gameObject.SetActive(true);
            }
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInAttack = true;

            for(int i = 0; i < portals.Count; i++)
            {
                portals[i].gameObject.SetActive(false);
            }

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<MolotovNPC>().isReady = true;
            }
        }
    }

    public GameObject GetNextRoom()
    {
        int rand = Random.Range(0, compatibleRooms.Length);
        print(compatibleRooms.Length);
        return compatibleRooms[rand];
    }
}
