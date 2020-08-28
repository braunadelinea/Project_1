using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//foreach (Slot slot in backpackPanel.GetComponentsInChildren<Slot>())
public class CompletionLoader : MonoBehaviour
{
    int completion;
    // Start is called before the first frame update
    void Start()
    {
        completion = FindObjectOfType<GameManager>().GetCompletion();
        Image[] iconObjects = GameObject.Find("Completion Icons").GetComponentsInChildren<Image>();
        foreach (Image icon in iconObjects)
        {
            switch (icon.name)
            {
                case "Ice Icon":
                    if (completion > 0)
                    {
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Ice Filled");
                    }
                    else
                    {
                        Debug.Log("Ice Icon Empty");
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Ice Empty");
                    }
                    break;
                case "Fire Icon":
                    if (completion > 1)
                    {
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Fire Filled");
                    }
                    else
                    {
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Fire Empty");
                    }
                    break;
                case "Earth Icon":
                    Debug.Log("Earth Icon Empty");
                    if (completion > 2)
                    {
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Earth Filled");
                    }
                    else
                    {
                        Debug.Log("Fire Icon Empty");
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Earth Empty");
                    }
                    break;
                case "Wind Icon":
                    if (completion > 3)
                    {
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Wind Filled");
                    }
                    else
                    {
                        Debug.Log("Wind Icon Empty");
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Wind Empty");
                    }
                    break;
                case "Time Icon":
                    if (completion > 4)
                    {
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Time Filled");
                    }
                    else
                    {
                        Debug.Log("Time Icon Empty");
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Time Empty");
                    }
                    break;
                case "Devil Icon":
                    if (completion > 5)
                    {
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Devil Filled");
                    }
                    else
                    {
                        Debug.Log("Devil Icon Empty");
                        icon.sprite = Resources.Load<Sprite>("Completion Icons/Completion Devil Empty");
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
