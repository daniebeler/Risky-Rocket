using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsController : MonoBehaviour
{

    public void enableLevel(int level)
    {
        transform.GetChild(level - 1).gameObject.SetActive(true);
    }

    public void disableLevels()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).gameObject.SetActive(false);
        }
    }

    public Vector2 getPlatformPosition(int level)
    {
        Vector2 pos = transform.GetChild(level - 1).gameObject.transform.Find("Start").transform.position;
        return pos;
    }
}
