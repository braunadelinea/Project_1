using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineButton : MonoBehaviour
{

    public void SetTimeline1()
    {
        FindObjectOfType<GameManager>().SetTimeline(1);
    }

    public void SetTimeline2()
    {
        FindObjectOfType<GameManager>().SetTimeline(2);
    }

    public void SetTimeline3()
    {
        FindObjectOfType<GameManager>().SetTimeline(3);
    }
}
