using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject bossRoom;

    public int maxQuantityRooms;
    public float roomWidth;

    private GameObject currentRoom; //current Alternative room
    private GameObject newRoom;
    private Vector3 nextPos;
    private int quantityRooms;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        GameObject initialObj = Instantiate(initialRoom, Vector3.zero, Quaternion.identity);
        newRoom = initialObj.GetComponent<RoomID>().GetNextRoom();
        quantityRooms++;

        for (int i = 0; i <= maxQuantityRooms; i++)
        {
            nextPos = new Vector3(initialObj.transform.position.x, initialObj.transform.position.y + roomWidth * quantityRooms);
            currentRoom = Instantiate(newRoom, nextPos, Quaternion.identity);
            newRoom = currentRoom.GetComponent<RoomID>().GetNextRoom();
            quantityRooms++;
        }

        currentRoom = Instantiate(bossRoom, nextPos, Quaternion.identity);
    }

    /*
    
            //---Side Rooms of center rooms---
            for (int j = 0; j < quantityAlternativeRooms; j++)
            {
                int rand = Random.Range(0, 100);

                if(rand >= chancesToNull)
                {
                    rand = Random.Range(0, 100);

                    if (rand <= chancesToRight) //right
                    {
                        SpawnPrefab(new Vector3(temp.transform.position.x + roomWidth * rightRooms, temp.transform.position.y, temp.transform.position.z));

                        rightRooms++;
                        isRight = true;
                    }
                    else //left
                    {
                        SpawnPrefab(new Vector3(temp.transform.position.x + (roomWidth * -leftRooms), temp.transform.position.y, temp.transform.position.z));

                        leftRooms++;
                        isRight = false;
                    }
                }
            }

            rightRooms = 1;
            leftRooms = 1;
            totalCenterRoomsInScene++;
        }

        //bossRoom spawn in center
        if (Random.Range(0, 100) <= bossSpawnInTopCenter)
        {
            Instantiate(bossRoom, new Vector3(0, temp.transform.position.y + roomWidth, temp.transform.position.z), Quaternion.identity);
        }
        else
        {
            Instantiate(bossRoom, new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y + roomWidth, currentRoom.transform.position.z), Quaternion.identity);
        }
    }

    void SpawnPrefab(Vector3 position)
    {
       currentRoom = Instantiate(roomsPrefabs[Random.Range(0, roomsPrefabs.Length)], position, Quaternion.identity);
    }
    */
} 
