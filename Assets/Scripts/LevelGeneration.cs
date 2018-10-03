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
    public GameObject roomTemplate, pathTemplate;

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
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, 1);
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

            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, 0);
            takenPositions.Insert(0, checkPos);
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
        foreach (Room room in rooms)
        {
            if (room != null)
            {
                Instantiate(roomTemplate, new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100), Quaternion.identity);
                if (room.doorLeft && !Physics.CheckSphere(new Vector3(room.gridPos.x * 100 - 50, 0, room.gridPos.y * 100), 10))
                {
                    int pathPos = (int)(Random.value * 3 + 1);
                    if (pathPos == 1)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 - 50, 0, room.gridPos.y * 100 + 10), Quaternion.identity);
                    }
                    else if (pathPos == 2)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 - 50, 0, room.gridPos.y * 100), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 - 50, 0, room.gridPos.y * 100 - 10), Quaternion.identity);
                    }
                }
                if (room.doorRight && !Physics.CheckSphere(new Vector3(room.gridPos.x * 100 + 50, 0, room.gridPos.y * 100), 10))
                {
                    int pathPos = (int)(Random.value * 3 + 1);
                    if (pathPos == 1)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 + 50, 0, room.gridPos.y * 100 + 10), Quaternion.identity);
                    }
                    else if (pathPos == 2)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 + 50, 0, room.gridPos.y * 100), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 + 50, 0, room.gridPos.y * 100 - 10), Quaternion.identity);
                    }
                }
                if (room.doorTop && !Physics.CheckSphere(new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 + 50), 10))
                {
                    int pathPos = (int)(Random.value * 3 + 1);
                    if (pathPos == 1)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 + 10, 0, room.gridPos.y * 100 + 50), new Quaternion(0, 90, 0, 90));
                    }
                    else if (pathPos == 2)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 + 50), new Quaternion(0, 90, 0, 90));
                    }
                    else
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 - 10, 0, room.gridPos.y * 100 + 50), new Quaternion(0, 90, 0, 90));
                    }
                }
                if (room.doorBottom && !Physics.CheckSphere(new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 - 50), 10))
                {
                    int pathPos = (int)(Random.value * 3 + 1);
                    if (pathPos == 1)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 + 10, 0, room.gridPos.y * 100 - 50), new Quaternion(0, 90, 0, 90));
                    }
                    else if (pathPos == 2)
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100, 0, room.gridPos.y * 100 - 50), new Quaternion(0, 90, 0, 90));
                    }
                    else
                    {
                        Instantiate(pathTemplate, new Vector3(room.gridPos.x * 100 - 10, 0, room.gridPos.y * 100 - 50), new Quaternion(0, 90, 0, 90));
                    }
                }
            }
        }
    }
}
