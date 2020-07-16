using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData //stores everything that needs to be saved and loaded from a file
{
    public bool[,] gameCompletion; //tracks which gods have been defeated in which timelines
    public float number; //this is a test variable to easily make sure the save system actually worked

    public WorldData(GameManager gameManager)
    {
        gameCompletion = gameManager.GetCompletion();
        number = gameManager.gameObject.transform.position.x; //again, this is just so that it can be easily tested
          
    }
}

