using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string currentElementType;
    private bool[,] completion; //tracks which gods have been defeated in which timelines to save to file
    public float number = -34;

    void Awake()
    {
        completion = new bool[3, 5]; //3 timelines, 6 gods to defeat in each timeline
    }

    void goToNextDungeon()
    {
        //TODO
    }

    void onDeath()
    {
        //TODO
    }

    public void Save()
    {
        SaveSystem.Save(this);
    }

    public void Load()
    {
        WorldData gameCompletion = SaveSystem.Load();

        completion = gameCompletion.gameCompletion;
        number = gameCompletion.number; 
        //more of these lines can be added for future variables to be saved and stored in WorldData
    }

    public bool[,] GetCompletion()
    {
        return completion;
    }
}
