using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{

    private Color32[] colors = new Color32[] { new Color32(49, 195, 129, 255), new Color32(103, 91, 232, 255), new Color32(87, 202, 247, 255), new Color32(249, 117, 218, 255), new Color32(92, 130, 228, 255) };

    public Color32 GetColor(){
        return colors[PlayerPrefs.GetInt("background", 0)];
    }

    public void setRandomColor(){
        PlayerPrefs.SetInt("background", Random.Range(0, colors.Length));
    }
}
