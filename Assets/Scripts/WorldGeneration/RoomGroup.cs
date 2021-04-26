using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGroup : MonoBehaviour
{
    public GameObject[] compatibleRooms;

    public GameObject GetNextRoom()
    {
        int rand = Random.Range(0, compatibleRooms.Length);
        print(compatibleRooms.Length);
        return compatibleRooms[rand];
    }
}
