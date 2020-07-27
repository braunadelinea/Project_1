using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Element { Ice, Fire, Earth, Wind, Time, Devil}
    private Element currentDungeonElement; //Which elemental dungeon the player is in, used to load correct sprites into the game
    private int currentTimeline; //Which timeline the player is in, used to load correct sprites and correct player stats (Each timeline gives the player a different disadvantage)
    private bool[,] completion; //tracks which gods have been defeated in which timelines to save to file, used to render completion visuals and which boss is to be fought next
    private bool devilComplete = false;
    public float number = -34;

    void Awake()
    {
        completion = new bool[3,5]; //3 timelines, 5 gods to defeat in each timeline: order of gods -> Ice, Fire, Earth, Wind, Time
        print("Setting test completion");
        completion[0, 3] = true;
        completion[1, 2] = true;
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

    public void GoToNextDungeon()
    {
        switch (currentDungeonElement)
        {
            case Element.Ice:
                currentDungeonElement = Element.Fire;
                //TODO: Regenerate dungeon with fire element sprites
                break;
            case Element.Fire:
                currentDungeonElement = Element.Earth;
                //TODO: Regenerate dungeon with earth element sprites
                break;
            case Element.Earth:
                currentDungeonElement = Element.Wind;
                //TODO: Regenerate dungeon with wind element sprites
                break;
            case Element.Wind:
                currentDungeonElement = Element.Time;
                //TODO: Regenerate dungeon with time element sprites, boss room is instead a room with doors to each god
                break;
            case Element.Time:
                currentDungeonElement = GetNextBossElement();
                //TODO: Generate a single room with correct element sprites and the god of that element
                break;
            case Element.Devil:
                Debug.Log("ERROR: Devil is defeated and game is complete, there is no next element");
                break;
        }
    }

    //**** GETTER METHODS ****//

    public bool[,] GetCompletion()
    {
        return completion;
    }

    public int GetTimeline()
    {
        return currentTimeline;
    }

    public Element GetCurrentElement()
    {
        return currentDungeonElement;
    }

    public Element GetNextBossElement()
    {
        for(int i = 0; i < 5; i++)
        {
            //if this god has not yet been defeated, return this as the next boss to defeat
            if(!completion[currentTimeline-1, i])
            {
                switch (i)
                {
                    case 0:
                        return Element.Ice;

                    case 1:
                        return Element.Fire;

                    case 2:
                        return Element.Earth;

                    case 3:
                        return Element.Wind;

                    case 4:
                        return Element.Time;

                }
            }
        }
        //should never get here, the timeline should not be able to be re-entered after it is complete
        Debug.Log("ERROR: All gods have been defeated in this timeline");
        return Element.Devil; //should not be devil, however must return something and cannot return null
    }

    //**** SETTER METHODS ****//

    public void Complete(int timeline, Element god)
    {
        if(timeline < 1 || timeline > 3)
        {
            Debug.LogError("ERROR: Timeline # out of bounds");
        }
        switch (god)
        {
            case Element.Ice:
                completion[timeline - 1, 0] = true;
                break;
            case Element.Fire:
                completion[timeline - 1, 1] = true;
                break;
            case Element.Earth:
                completion[timeline - 1, 2] = true;
                break;
            case Element.Wind:
                completion[timeline - 1, 3] = true;
                break;
            case Element.Time:
                completion[timeline - 1, 4] = true;
                break;
            case Element.Devil:
                devilComplete = true;
                break;
        }
    }

    public void SetTimeline(int num)
    {
        if(num < 1 || num > 3)
        {
            Debug.Log("ERROR: Timeline inputted is out of bounds");
            return;
        }
        currentTimeline = num;
    }
};
