using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public enum Element { None, Ice, Fire, Earth, Wind, Time, Devil}
    private Element currentDungeonElement = Element.Ice; //Which elemental dungeon the player is in, used to load correct sprites into the game
    private int completion; //tracks which gods have been defeated
    private bool gameComplete = false;
    public int type;

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GoToNextDungeon();
        }
        if (GetCurrentElement() == Element.Ice)
        {
            type = 0;
        }
        else if (GetCurrentElement() == Element.Fire)
        {
            type = 1;
        }
        else if (GetCurrentElement() == Element.Earth)
        {
            type = 2;
        }
        else if (GetCurrentElement() == Element.Wind)
        {
            type = 3;
        }
        else if (GetCurrentElement() == Element.Time)
        {
            type = 4;
        }
        else if (GetCurrentElement() == Element.Devil) {
            type = 5;
        }


    }
    void Awake()
    {
        completion = 0; //6 bosses to defeat: order -> Ice, Fire, Earth, Wind, Time, Devil
    }

    public void Save()
    {
        SaveSystem.Save(this);
    }

    public void Load()
    {
        WorldData gameCompletion = SaveSystem.Load();

        completion = gameCompletion.gameCompletion;
    }

    public void GoToNextDungeon()
    {
        switch (currentDungeonElement)
        {
            case Element.Ice:
                currentDungeonElement = Element.Fire;
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                break;
            case Element.Fire:
                currentDungeonElement = Element.Earth;
                Scene scenee = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scenee.name);
                break;
            case Element.Earth:
                currentDungeonElement = Element.Wind;
                Scene sceneee = SceneManager.GetActiveScene();
                SceneManager.LoadScene(sceneee.name);
                break;
            case Element.Wind:
                currentDungeonElement = Element.Time;
                Scene sceneeee = SceneManager.GetActiveScene();
                SceneManager.LoadScene(sceneeee.name);
                break;
            case Element.Time:
                currentDungeonElement = GetNextBossElement();
                SceneManager.LoadScene(4);
                break;
            case Element.Devil:
                Debug.Log("ERROR: Devil is defeated and game is complete, there is no next element");
                break;
        }
    }

    //**** GETTER METHODS ****//

    public int GetCompletion()
    {
        return completion;
    }

    public Element GetCurrentElement()
    {
        return currentDungeonElement;
    }

    public Element GetNextBossElement()
    {
        switch (completion)
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
            case 5:
                return Element.Devil;

        }
        Debug.Log("ERROR: Completion variable is out of bounds");
        return Element.Devil; //should not be devil, however must return something, cannot return null
    }
};
