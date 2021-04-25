using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public GameObject initialRoom;
    public GameObject bossRoom;

    [Header("Rooms Prefabs")]
    public GameObject[] roomsAll;
    public GameObject[] roomsU;
    public GameObject[] roomsD;
    public GameObject[] roomsR;
    public GameObject[] roomsL;

    public GameObject[,] matrix = new GameObject[0,0];

    public int quantityRooms;
    public int quantityAlternativeRooms;
    public float roomWidth;

    [Range((int)0, (int)100)]
    public int chancesToNull = 20; //what's the chance to NO spawn?
    [Range((int)0, (int)100)]
    public int chancesToRight = 50; //what's the chance to spawn for right?
    [Range((int)0, (int)100)]
    public int bossSpawnInTopCenter = 50; //what's the chance to spawn the room of boss in top center in finish row?

    //if the initial rooms is in scene before starts, roomsInScene = 1, if it is generate in start roomsInScene = 0
    private int totalCenterRoomsInScene = 0;
    private int x;
    private int y;
    private GameObject currentRoom; //current Alternative room

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        GameObject initialPoint = Instantiate(initialRoom, new Vector3(0, 0, 0), Quaternion.identity);
        //matrix[x, y] = initialPoint;
        totalCenterRoomsInScene++;

        if (initialPoint.GetComponent<RoomID>().roomType == RoomType.U)
        {
            currentRoom = Instantiate(roomsD[Random.Range(0, roomsD.Length)], new Vector3(0, initialPoint.transform.position.y + roomWidth * totalCenterRoomsInScene, 0), Quaternion.identity);
            y++;
            //matrix[x, y] = currentRoom;
            totalCenterRoomsInScene++;
        }

        for (int i = 0; i < quantityRooms; i++)
        {
            currentRoom = InstantiateNewRoom(currentRoom.GetComponent<RoomID>());
           // matrix[x, y] = currentRoom;
        }
    }

    GameObject InstantiateNewRoom(RoomID room)
    {
        GameObject newRoom = new GameObject();
        Vector2 newpos = new Vector2();
        switch (room.roomType)
        {
            case RoomType.U:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;

            case RoomType.D:
                newRoom = roomsU[Random.Range(0, roomsU.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y - roomWidth);
                y--;
                break;

            case RoomType.R:
                newRoom = roomsL[Random.Range(0, roomsL.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x + roomWidth, room.transform.position.y);
                x++;
                break;

            case RoomType.L:
                newRoom = roomsR[Random.Range(0, roomsR.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x - roomWidth, room.transform.position.y);
                x--;
                break;

            case RoomType.RLD:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                x++;
                break;

            case RoomType.RLU:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;


                break;
            case RoomType.RUD:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;

            case RoomType.LUD:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;

            case RoomType.UR:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;
            case RoomType.UL:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;

            case RoomType.UD:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;

            case RoomType.DR:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;
            case RoomType.DL:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;

            case RoomType.RL:
                newRoom = roomsD[Random.Range(0, roomsD.Length)];
                newpos = new Vector2(room.gameObject.transform.position.x, room.transform.position.y + roomWidth);
                y++;

                break;
        }
        
        return Instantiate(newRoom, newpos, Quaternion.identity);
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
