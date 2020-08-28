using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData //stores everything that needs to be saved and loaded from a file
{
    public int gameCompletion; //tracks which gods have been defeated in which timelines

    public WorldData(GameManager gameManager)
    {
        gameCompletion = gameManager.GetCompletion();
    }
}

