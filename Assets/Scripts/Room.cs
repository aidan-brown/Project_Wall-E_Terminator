using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 gridPos;
    public int roomType;
    public bool doorTop, doorBottom, doorLeft, doorRight;

    public Room(Vector2 pos, int type)
    {
        gridPos = pos;
        roomType = type;
    }
}
