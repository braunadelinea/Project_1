using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//foreach (Slot slot in backpackPanel.GetComponentsInChildren<Slot>())
public class CompletionLoader : MonoBehaviour
{
    bool[,] completion;
    // Start is called before the first frame update
    void Start()
    {
        print("Getting completion");
        completion = FindObjectOfType<GameManager>().GetCompletion();
        for(int i = 0; i < 3; i++)
        {
            Image[] iconObjects = GameObject.Find("Timeline " + (i+1) + " Icons").GetComponentsInChildren<Image>();
            foreach (Image icon in iconObjects)
            {
                switch (icon.name)
                {
                    case "Ice Icon":
                        if (completion[i, 0])
                        {
                            icon.color = Color.black;
                        }
                        else
                        {

                        }        
                        break;
                    case "Fire Icon":
                        if (completion[i, 1])
                        {
                            icon.color = Color.black;
                        }
                        else
                        {

                        }
                        break;
                    case "Earth Icon":
                        if (completion[i, 2])
                        {
                            icon.color = Color.black;
                        }
                        else
                        {

                        }
                        break;
                    case "Wind Icon":
                        if (completion[i, 3])
                        {
                            icon.color = Color.black;
                        }
                        else
                        {

                        }
                        break;
                    case "Time Icon":
                        if (completion[i, 4])
                        {
                            icon.color = Color.black;
                        }
                        else
                        {

                        }
                        break;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
