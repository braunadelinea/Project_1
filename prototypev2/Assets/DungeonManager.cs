using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.Assertions;

public class DungeonManager : MonoBehaviour
{
    DungeonGenerator dungeonGenerator = new DungeonGenerator(10);
    float roomWidth = 11;
    float roomHeight = 9;
    GameManager.Element dungeonElement = GameManager.Element.None;

    float playerX;
    float playerY;

    GameObject wallC;
    GameObject wallN;
    GameObject wallE;
    GameObject wallS;
    GameObject wallW;
    GameObject floorC;
    GameObject floorN;
    GameObject floorE;
    GameObject floorS;
    GameObject floorW;
    GameObject floorNE;
    GameObject floorES;
    GameObject floorSW;
    GameObject floorWN;

    // Start is called before the first frame update
    void Start()
    {
        LoadResources(GameManager.Element.Devil);
        dungeonGenerator.GenerateDungeon();
        DungeonGenerator.RoomType[,] map = dungeonGenerator.getMap();

        float mapWidth = roomWidth * map.GetLength(0);
        float mapHeight = roomHeight * map.GetLength(1);

        for(int row = 0; row < map.GetLength(1); row++)
        {
            for (int col = 0; col < map.GetLength(0); col++)
            {
                DungeonGenerator.RoomType roomType = map[col, row];
                float locationX = (-mapWidth / 2) + (col * roomWidth);
                float locationY = (mapHeight / 2) - (row * roomHeight);

                // TODO: Call a method to create the room in this location.
                // method(locationX, locationY, roomType);
                if (roomType != null)
                {
                    CreateRoom(locationX, locationY, roomType);
                }
            }
        }

        // calculate player start position
        Tuple<int, int> firstRoomLocation = dungeonGenerator.FirstRoomLocation;
        playerX = (-mapWidth / 2) + (firstRoomLocation.Item1 * roomWidth) + roomWidth / 2;
        playerY = (mapHeight / 2) - (firstRoomLocation.Item2 * roomHeight) - roomHeight / 2;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        if (!player.GetComponent<PlayerManager>().IsSpawned)
        

//        if (!GameObject.Find("Player").GetComponent<PlayerManager>().IsSpawned)
        {
            // move the player to the first room
            //GameObject.Find("Player").GetComponent<PlayerManager>().SpawnPlayer(playerX, playerY);
            player.GetComponent<PlayerManager>().SpawnPlayer(playerX, playerY);

        }
    }

    public void LoadResources(GameManager.Element element)
    {
        Debug.Assert(element != GameManager.Element.None, "Passed in element of type none, cannot load resources");
        if (dungeonElement == element)
        {
            return;
        }
        //dungeonElement = element;
        dungeonElement = GameManager.Element.Devil;

        wallC = (GameObject) Resources.Load(element.ToString() + "Tiles/wallC");
        wallN = (GameObject) Resources.Load(element.ToString() + "Tiles/wallN");
        wallE = (GameObject) Resources.Load(element.ToString() + "Tiles/wallE");
        wallS = (GameObject) Resources.Load(element.ToString() + "Tiles/wallS");
        wallW = (GameObject) Resources.Load(element.ToString() + "Tiles/wallW");
        floorC = (GameObject) Resources.Load(element.ToString() + "Tiles/floorC");
        floorN = (GameObject) Resources.Load(element.ToString() + "Tiles/floorN");
        floorE = (GameObject) Resources.Load(element.ToString() + "Tiles/floorE");
        floorS = (GameObject) Resources.Load(element.ToString() + "Tiles/floorS");
        floorW = (GameObject) Resources.Load(element.ToString() + "Tiles/floorW");
        floorNE = (GameObject) Resources.Load(element.ToString() + "Tiles/floorNE");
        floorES = (GameObject) Resources.Load(element.ToString() + "Tiles/floorES");
        floorSW = (GameObject) Resources.Load(element.ToString() + "Tiles/floorSW");
        floorWN = (GameObject) Resources.Load(element.ToString() + "Tiles/floorWN");

        Debug.Assert(wallC != null, "Resources not loaded correctly");
    }

    void CreateRoom(float x, float y, DungeonGenerator.RoomType roomType)
    {
        for (int row = 0; row < roomHeight; row++)
        {
            for (int col = 0; col < roomWidth; col++)
            {
                float locationX = x + col;
                float locationY = y - row;

                //draw openings for doors
                if (roomType.north && row == 0 && col == (int)roomWidth / 2)
                {
                    Instantiate(floorC, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                if (roomType.east && row == (int)roomHeight / 2 && col == (int)roomWidth - 1)
                {
                    Instantiate(floorC, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                if (roomType.south && row == (int)roomHeight - 1 && col == (int)(roomWidth / 2))
                {
                    Instantiate(floorC, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                if (roomType.west && row == (int)roomHeight / 2 && col == 0)
                {
                    Instantiate(floorC, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }

                // draw walls
                if ((row == 0 && (col == 0 || col == roomWidth - 1)) || (row == roomHeight - 1 && (col == 0 || col == roomWidth - 1)))
                {
                    // corner piece
                    Instantiate(wallC, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (row == 0)
                {
                    // north wall
                    Instantiate(wallN, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (row == roomHeight - 1)
                {
                    // south wall
                    Instantiate(wallS, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (col == 0)
                {
                    // west wall
                    Instantiate(wallW, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (col == roomWidth - 1)
                {
                    // east wall
                    Instantiate(wallE, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
   
                // floor corners, edges
                if (row == 1 && col == 1)
                {
                    Instantiate(floorWN, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (row == 1 && col == roomWidth - 2)
                {
                    Instantiate(floorNE, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (row == roomHeight - 2 && col == 1)
                {
                    Instantiate(floorSW, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (row == roomHeight - 2 && col == roomWidth - 2)
                {
                    Instantiate(floorES, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (row == 1)
                {
                    Instantiate(floorN, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (row == roomHeight - 2)
                {
                    Instantiate(floorS, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (col == 1)
                {
                    Instantiate(floorW, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }
                else if (col == roomWidth - 2)
                {
                    Instantiate(floorE, new Vector3(locationX, locationY), Quaternion.identity);
                    continue;
                }

                // floor center
                Instantiate(floorC, new Vector3(locationX, locationY), Quaternion.identity);
            }
        }
    }
    public void MoveCamera(Vector3 location)
    {
        GameObject.Find("Main Camera").transform.position = location;
    }
}