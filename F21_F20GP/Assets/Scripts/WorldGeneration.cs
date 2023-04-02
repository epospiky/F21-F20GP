using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class WorldGeneration : MonoBehaviour
{
    public int size = 100;
    public int minRoomSize = 10;
    public int maxRoomSize = 20;
    public int NumberOfRooms = 5;

    public int numberOfSpawn1 = 5;
    public GameObject spawn1;

    public GameObject Player;

    public List<(int,int)> points = new List<(int, int)>();
    public List<Vector3> validSpawns = new List<Vector3>();

    public NavMeshSurface surface;


    // Start is called before the first frame update
    void Start()
    {

        int[,] world = createArray(size);
        world = createRooms(world);
        world = combineRooms(world);
        world = createHallways(world);
        world = findWalls(world);
        printArray(world);
        placeWalls(world);

        surface.BuildNavMesh();
        findValidSpawns(world);
        spawnEnemies();


        int spawnPoint = Random.Range(0, validSpawns.Count - 1);
        Instantiate(Player, validSpawns[spawnPoint], Quaternion.identity);
        validSpawns.RemoveAt(spawnPoint);
    }

    private void spawnEnemies()
    {
        for(int x = 0; x < numberOfSpawn1; x++)
        {
            int spawnPoint = Random.Range(0, validSpawns.Count-1);
            Instantiate(spawn1, validSpawns[spawnPoint], Quaternion.identity);
            validSpawns.RemoveAt(spawnPoint);
        }
    }

    private void findValidSpawns(int[,] world)
    {
        for (int y = 1; y < (size - 1); y++)
        {
            for (int z = 1; z < (size - 1); z++)
            {
                if (world[y,z] > 0)
                {
                    validSpawns.Add(new Vector3(y * 2, 1.0f, z * 2));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public int[,] createArray(int sizeOfArray)
    {
        int[,] array = new int[sizeOfArray, sizeOfArray];
        return array;
    }

    public int[,] createRooms(int[,] rooms)
    {
        for (int x = 1; x < (NumberOfRooms+1); x++)
        {
            int startPosX = Random.Range(0, ((size-1)-maxRoomSize));
            int startPosY = Random.Range(0, ((size - 1) - maxRoomSize));
            int roomX = Random.Range(minRoomSize, maxRoomSize);
            int roomY = Random.Range(minRoomSize, maxRoomSize);

            points.Add((Random.Range(startPosY, startPosY+roomY), (Random.Range(startPosX, startPosX + roomX))));

            for (int y = startPosY; y < (roomY+startPosY); y++)
            {
                for (int z = startPosX; z < (roomX+startPosX); z++)
                {
                    rooms[y, z] = x;
                }
            }
        }
        return rooms;
    }

    public int[,] combineRooms(int[,] rooms)
    {
        for (int x = 1; x < (NumberOfRooms + 1); x++)
        {
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int y = 1; y < (size-2); y++)
                {
                    for (int z = 1; z < (size-2); z++)
                    {
                        if (rooms[y,z] == x)
                        {
                            //Then check all surronding if != 0 then switch to seen one repeat till none changed
                            if (rooms[y - 1, z - 1] != 0 && rooms[y - 1, z - 1] != x)
                            {
                                rooms[y - 1, z - 1] = x;
                                changed = true;
                            }
                            if (rooms[y - 1, z] != 0 && rooms[y - 1, z] != x)
                            {
                                rooms[y - 1, z] = x;
                                changed = true;
                            }
                            if (rooms[y - 1, z + 1] != 0 && rooms[y - 1, z + 1] != x)
                            {
                                rooms[y - 1, z + 1] = x;
                                changed = true;
                            }
                            if (rooms[y, z + 1] != 0 && rooms[y, z + 1] != x)
                            {
                                rooms[y, z + 1] = x;
                                changed = true;
                            }
                            if (rooms[y + 1, z + 1] != 0 && rooms[y + 1, z + 1] != x)
                            {
                                rooms[y + 1, z + 1] = x;
                                changed = true;
                            }
                            if (rooms[y + 1, z] != 0 && rooms[y + 1, z] != x)
                            {
                                rooms[y + 1, z] = x;
                                changed = true;
                            }
                            if (rooms[y + 1, z - 1] != 0 && rooms[y + 1, z - 1] != x)
                            {
                                rooms[y + 1, z - 1] = x;
                                changed = true;
                            }
                            if (rooms[y, z - 1] != 0 && rooms[y, z - 1] != x)
                            {
                                rooms[y, z - 1] = x;
                                changed = true;
                            }
                        }
                    }
                }
            }
            
        }
        return rooms;
    }

    public int[,] findWalls(int[,] rooms)
    {
        for (int y = 1; y < (size - 1); y++)
        {
            for (int z = 1; z < (size - 1); z++)
            {
                if (rooms[y, z] == 0)
                {
                    if (rooms[y, z + 1] != 0 && rooms[y, z + 1] != -1)
                    {
                        rooms[y, z] = -1;
                    }
                    if (rooms[y, z - 1] != 0 && rooms[y, z - 1] != -1)
                    {
                        rooms[y, z] = -1;
                    }
                    if (rooms[y + 1, z] != 0 && rooms[y + 1, z] != -1)
                    {
                        rooms[y, z] = -1;
                    }
                    if (rooms[y -1, z] != 0 && rooms[y - 1, z] != -1)
                    {
                        rooms[y, z] = -1;
                    }       
                }
            }
        }
        return rooms;
    }

    public int[,] createHallways(int[,] rooms)
    {
        for (int x = 0; x < (points.Count - 1); x++)
        {
            int currX = points[x].Item1;
            int currY = points[x].Item2;
            while(currX != points[x + 1].Item1)
            {
                if(currX < points[x + 1].Item1)
                {
                    currX++;
                    if (rooms[currX,currY] == 0)
                    {
                        rooms[currX,currY] = -2;
                    }
                }
                if (currX > points[x + 1].Item1)
                {
                    currX--;
                    if (rooms[currX, currY] == 0)
                    {
                        rooms[currX, currY] = -2;
                    }
                }
            }
            while (currY != points[x + 1].Item2)
            {
                if (currY < points[x + 1].Item2)
                {
                    currY++;
                    if (rooms[currX, currY] == 0)
                    {
                        rooms[currX, currY] = -2;
                    }
                }
                if (currY > points[x + 1].Item2)
                {
                    currY--;
                    if (rooms[currX, currY] == 0)
                    {
                        rooms[currX, currY] = -2;
                    }
                }
            }
        }
        return rooms;
    }

    public void placeWalls(int[,] rooms)
    {
        for (int y = 0; y < (size); y++)
        {
            for (int z = 0; z < (size); z++)
            {
                if (rooms[y,z] == -1 | y == 0 | z == 0 | y == (size - 1) | z == (size - 1))
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = new Vector3(2, 2, 2);
                    cube.transform.position = new Vector3(y*2, 2, z*2);
                    cube.AddComponent<BoxCollider>();
                }
            }
        }

    }

    public void printArray(int[,] array)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < array.GetLength(1); i++)
        {
            for (int j = 0; j < array.GetLength(0); j++)
            {
                sb.Append(array[i, j]);
                sb.Append(' ');
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }
}
