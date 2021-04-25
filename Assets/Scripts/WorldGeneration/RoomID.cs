using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    ALL,
    U, D, R, L,
    RLD, RLU, RUD, LUD,
    UR, UL, UD,
    DR, DL,
    RL,
}

public class RoomID : MonoBehaviour
{
    public RoomType roomType;
}
