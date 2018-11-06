using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int numberOfRooms = 20;
    public Vector2 worldSize = new Vector2(4, 4);
    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();
    int gridSizeX, gridSizeY;
    public GameObject roomTemplate, pathTemplate, teleRoom;

    public GameObject[] blueprints = new GameObject[11];
    public GameObject[] outerWalls = new GameObject[8];

    void Start()
    {
        if(numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetRoomDoors();
        DrawMap();
    }

    void CreateRooms()
    {
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 0);
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;

        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        for (int i = 0; i < numberOfRooms - 1; i++)
        {
            float randomPerc = ((float)i) / ((float)numberOfRooms - 1);
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            checkPos = NewPosition();

            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare)
            {
                int iterations = 0;
                do
                {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                }
                while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50)
                    print("error: could not create with fewer neighbors than: " + NumberOfNeighbors(checkPos, takenPositions));
            }

            if (i < numberOfRooms - 2)
            {
                rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, (int)Random.Range(1, 10));
                takenPositions.Insert(0, checkPos);
            }
            else
            {
                rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 11);
            }
        }
    }

    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return checkingPos;
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0, inc = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            inc = 0;
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            }
            while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        }
        while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100)
        {
            print("Error: could not find position with only one neighbor");
        }
        return checkingPos;
    }

    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }

    void SetRoomDoors()
    {
        for (int x = 0; x < (gridSizeX * 2); x++)
        {
            for (int y = 0; y < (gridSizeY * 2); y++)
            {
                if (rooms[x,y] == null)
                {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);
                if (y - 1 < 0)
                {
                    rooms[x, y].doorBottom = false;
                }
                else
                {
                    rooms[x, y].doorBottom = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2)
                {
                    rooms[x, y].doorTop = false;
                }
                else
                {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0)
                {
                    rooms[x, y].doorLeft = false;
                }
                else
                {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2)
                {
                    rooms[x, y].doorRight = false;
                }
                else
                {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }
    }

    void DrawMap()
    {
        GameObject tele = null;
        foreach (Room room in rooms)
        {
            if (room != null)
            {
                if (room.roomType != 11)
                {
                    GameObject roomFloor = Instantiate(roomTemplate, new Vector3(room.gridPos.x * 50, 0, room.gridPos.y * 50), Quaternion.identity);
                    GameObject roomWalls;
                    switch (room.roomType)
                    {
                        case 0:
                            roomWalls = blueprints[0];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 1:
                            roomWalls = blueprints[1];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 2:
                            roomWalls = blueprints[2];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 3:
                            roomWalls = blueprints[3];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 4:
                            roomWalls = blueprints[4];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;
                        case 5:
                            roomWalls = blueprints[5];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 6:
                            roomWalls = blueprints[6];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 7:
                            roomWalls = blueprints[7];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 8:
                            roomWalls = blueprints[8];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 9:
                            roomWalls = blueprints[9];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;

                        case 10:
                            roomWalls = blueprints[10];
                            Instantiate(roomWalls, new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                            break;
                    }
                    if (room.doorLeft)
                    {
                        /*if (!Physics.CheckSphere(new Vector3(room.gridPos.x * 100 - 50, 0, room.gridPos.y * 100), 10))
                        {
                            Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 - 50, 0, room.gridPos.y * 100), Quaternion.identity);
                        }*/
                        Instantiate(outerWalls[0], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(outerWalls[1], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                    if (room.doorRight)
                    {
                        /*if (!Physics.CheckSphere(new Vector3(room.gridPos.x * 100 + 50, 0, room.gridPos.y * 100), 10))
                        {
                            Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 + 50, 0, room.gridPos.y * 100), Quaternion.identity);
                        }*/
                        Instantiate(outerWalls[2], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(outerWalls[3], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                    if (room.doorTop)
                    {
                        /*if (!Physics.CheckSphere(new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 + 50), 10))
                        {
                           Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 + 50), new Quaternion(0, 90, 0, 90));
                        }*/
                        Instantiate(outerWalls[4], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(outerWalls[5], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                    if (room.doorBottom)
                    {
                        /*if (!Physics.CheckSphere(new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 - 50), 10))
                        {
                            Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 - 50), new Quaternion(0, 90, 0, 90));
                        }*/
                        Instantiate(outerWalls[6], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(outerWalls[7], new Vector3(roomFloor.transform.position.x, 2.5f, roomFloor.transform.position.z), Quaternion.identity);
                    }
                }
                else
                {
                    tele = Instantiate(teleRoom, new Vector3(room.gridPos.x * 50, 0, room.gridPos.y * 50), Quaternion.identity);
                }
            }
        }
        GameObject nearest = FindNearest(tele, "room");
        if (tele.transform.position.x < nearest.transform.position.x)
        {
            tele.transform.Rotate(0, 180, 0);
        }
        else if (tele.transform.position.z < nearest.transform.position.z)
        {
            tele.transform.Rotate(0, 90, 0);
        }
        else if (tele.transform.position.z > nearest.transform.position.z)
        {
            tele.transform.Rotate(0, -90, 0);
        }
        tele.transform.Translate(-23, 0, 0);
    }

    private GameObject FindNearest(GameObject obj, string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearestObj = null;
        float nearestPos = Mathf.Infinity;

        foreach(GameObject tmpObj in objects)
        {
            if(Mathf.Abs(tmpObj.transform.position.x - obj.transform.position.x) + Mathf.Abs(tmpObj.transform.position.z - obj.transform.position.z) < nearestPos)
            {
                nearestPos = Mathf.Abs(tmpObj.transform.position.x - obj.transform.position.x) + Mathf.Abs(tmpObj.transform.position.z - obj.transform.position.z);
                nearestObj = tmpObj;
            }
        }
        return nearestObj;
    }
}
