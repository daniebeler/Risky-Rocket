using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{

    

    private Color32[] colors;

    private void Start()
    {
        Color32 iconGreen = new Color32(49, 195, 129, 255);
        Color32 green = new Color32(42, 213, 98, 255);
        Color32 lila = new Color32(103, 91, 232, 255);
        Color32 lightBlue = new Color32(87, 202, 247, 255);
        Color32 pink = new Color32(249, 117, 218, 255);
        Color32 blue = new Color32(92, 130, 228, 255);
        Color32 orange = new Color32(255, 115, 27, 255);
        Color32 yellow = new Color32(234, 202, 32, 255);


        colors = new Color32[] { iconGreen, green, lila, lightBlue, pink, blue, orange, yellow };
    }

    public Color32 GetColor(){
        return colors[PlayerPrefs.GetInt("background", 0)];
    }

    public void setRandomColor(){
        PlayerPrefs.SetInt("background", Random.Range(0, colors.Length));
    }
}
