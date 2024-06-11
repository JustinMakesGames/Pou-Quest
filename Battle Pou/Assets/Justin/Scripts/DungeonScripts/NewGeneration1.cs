using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGeneration1 : MonoBehaviour
{
    public int maxX;
    public int maxZ;
    public float offsetX;
    public float offsetZ;
    public List<Tile1> tileObjects;
    public List<Cell1> gridList = new();
    public Cell1 cell;
    private int iteration;

    public List<GameObject> objectsToSpawn;
    public LayerMask cellLayer;
    public GameObject firstRoom;

    public List<GameObject> rooms;

    public int index;

    public int maxTimes;

    private List<GameObject> roomsUsed = new List<GameObject>();

    public GameObject bossRoom;
    private bool isFinalRoom;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        //Maakt empty gameobjects aan met het script.
        for (int x = 0; x < maxX; x++)
        {
            for (int z = 0; z < maxZ; z++)
            {
                Cell1 newCell = Instantiate(cell, new Vector3(x * offsetX, 0, z * offsetZ), Quaternion.identity);
                
                gridList.Add(newCell);
            }
        }

        CreateFirstPrefab();
    }


    private void CreateFirstPrefab()
    {
        Transform pos = gridList[150].transform;

        GameObject nowCell = FindObjectOfType<Tile1>().gameObject;
        pos.GetComponent<Cell1>().tiles.Add(nowCell.GetComponent<Tile1>());
        objectsToSpawn.Add(nowCell);
        
        StartCoroutine(UpdateGeneration());
    }

    private IEnumerator UpdateGeneration()
    {
        for (int i = 0; i < maxTimes; i++)
        {
            yield return null;
            Generate(objectsToSpawn[index].transform);
        }
        yield return null;
        isFinalRoom = true;
        Generate(objectsToSpawn[index].transform);
        
    }
    private void Generate(Transform originalPos)
    {
        Vector3 dir = GetDirection(originalPos);
        if (dir == Vector3.zero)
        {
            if (roomsUsed.Count == 0)
            {
                RestartGeneration(originalPos.gameObject);
            }
            else
            {
                ReplaceOriginalObject(originalPos.gameObject);
                maxTimes++;
            }
        }
        else
        {
            roomsUsed.Clear();
            roomsUsed.AddRange(rooms);
            Cell1 nextCell = GetCellInDirection(originalPos.position, dir);
            GameObject randomRoom = null;

            if (!isFinalRoom)
            {
                randomRoom = GetRandomRoom();
            }
            else
            {
                randomRoom = bossRoom;
            }
            GameObject newPos = Instantiate(randomRoom, nextCell.transform.position, Quaternion.identity);
            nextCell.tiles.Add(newPos.GetComponent<Tile1>());

            objectsToSpawn.Add(newPos);

            Quaternion rotation = GetRotation(originalPos, newPos.transform);
            newPos.transform.rotation = rotation;
            index++;
        }

    }

    private Vector3 GetDirection(Transform tile)
    {
        Door[] searchForDoors = tile.GetComponentsInChildren<Door>();
        List<Transform> doors = new List<Transform>();
        Debug.Log(searchForDoors[0]);
        foreach (Door door in searchForDoors)
        {
            doors.Add(door.transform);
        }
        Vector3 dir = Vector3.zero;
        bool isChecked = false;
        while (!isChecked)
        {
            int doorToUse = Random.Range(0, doors.Count);

            dir = -doors[doorToUse].forward;
            Cell1 cell = GetCellInDirection(tile.position, dir);

            if (cell != null)
            {
                if (cell.tiles.Count <= 0)
                {
                    doors[doorToUse].GetChild(1).GetComponent<Collider>().isTrigger = true;
                    Renderer[] renderers = doors[doorToUse].GetChild(1).GetComponentsInChildren<Renderer>();

                    foreach (Renderer renderer in renderers)
                    {
                        renderer.enabled = false;
                    }
                    tile.GetComponent<Tile1>().inDoor = doors[doorToUse].gameObject;
                    isChecked = true;
                }
            }

            if (!isChecked)
            {
                doors.RemoveAt(doorToUse);
            }

            if (doors.Count == 0)
            {
                dir = Vector3.zero;
                isChecked = true;
            }
        }

        Debug.Log(dir);
        return dir;
    }

    private GameObject GetRandomRoom()
    {
        int room = Random.Range(0, rooms.Count);
        return rooms[room];
    }

    private Cell1 GetCellInDirection(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, Mathf.Infinity, cellLayer))
        {
            return hit.transform.GetComponent<Cell1>();
        }
        return null;
    }

    private Quaternion GetRotation(Transform originalPos, Transform newPos)
    {
        Door[] searchForDoors = newPos.GetComponentsInChildren<Door>();
        List<Transform> doors = new List<Transform>();

        foreach (Door door in searchForDoors)
        {
            doors.Add(door.transform);
        }        
        int getRandomDoor = Random.Range(0, doors.Count);

        doors[getRandomDoor].gameObject.SetActive(false);
        newPos.GetComponent<Tile1>().outDoor = doors[getRandomDoor].gameObject;
        Vector3 lookPosition = newPos.position - originalPos.position;

        Vector3 doorForward = doors[getRandomDoor].forward;

        Quaternion rotation = Quaternion.FromToRotation(doorForward, lookPosition);
        if (rotation.x == 1)
        {
            rotation = new Quaternion(0, 1, 0, 0);
        }
        return rotation;
    }

    private void ReplaceOriginalObject(GameObject original)
    {
        GameObject newRoom = GetRandomAnotherRoom();
        roomsUsed.Remove(newRoom);
        GameObject cloneRoom = Instantiate(newRoom, original.transform.position, Quaternion.identity);
        objectsToSpawn.RemoveAt(index);
        objectsToSpawn.Add(cloneRoom);
        
        Destroy(original);

    }

    private GameObject GetRandomAnotherRoom()
    {
        int randomRoom = Random.Range(0, roomsUsed.Count);
        return roomsUsed[randomRoom];
    }

    private void RestartGeneration(GameObject pos)
    {
        print("Go back");
        objectsToSpawn.Remove(pos);
        Destroy(pos);    
        index--;
        maxTimes++;
        PutDoorRenderersBack(objectsToSpawn[index]);      
    }

    private void PutDoorRenderersBack(GameObject pos)
    {
        Door[] searchForDoors = pos.GetComponentsInChildren<Door>();
        List<Transform> doors = new List<Transform>();

        foreach (Door door in searchForDoors)
        {
            doors.Add(door.transform);
        }

        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i].gameObject == pos.GetComponent<Tile1>().outDoor)
            {
                print("YESSSSSS NOOO");
            }
            else
            {
                doors[i].GetChild(1).GetComponent<Collider>().isTrigger = false;
                Renderer[] renderers = doors[i].GetChild(1).GetComponentsInChildren<Renderer>();

                foreach (Renderer renderer in renderers)
                {
                    renderer.enabled = true;
                }
            }
            
        }

    }
}
