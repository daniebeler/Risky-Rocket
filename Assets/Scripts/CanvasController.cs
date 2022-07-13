using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasController : MonoBehaviour
{
    private int numberOfLevels = 0;
    private int FadeSequence = 0;
    private float startTime;

    public GameObject Button_Go_Home;
    public GameObject Header;

    public GameObject ButtonToSpawn;

    public GameObject Menu;
    public GameObject ButtonHolder;
    public GameObject Game;
    public GameObject Settings;

    [SerializeField]
    private Image bg;

    private General general;

    void Start()
    {

        general = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<General>();

        numberOfLevels = GameObject.FindGameObjectWithTag("LevelHolder").transform.childCount;

        float abstand = Screen.width / 5 / 5;
        float breite = Screen.width / 5;
        float höhe = Screen.height / 5;
        float abstandunten = Screen.width * 0.1f + höhe * 0.5f;

        float breiteHeader = Screen.width / 3;
        float höheHeader = breiteHeader / 7.87f;

        Header.transform.position = new Vector3(0, Screen.height / -4);
        Header.GetComponent<RectTransform>().sizeDelta = new Vector2(breiteHeader, höheHeader);

        for (int i = 1; i <= numberOfLevels; i++)
        {
            Vector2 spawnPos = new Vector2(i * Screen.height / 2, 0);

            if ((i + 3) % 4 == 0)    // Links
            {
                spawnPos.x = (breite + abstand) * -1.5f;
            }
            else if ((i + 1) % 4 == 0)        // Mitte Rechts
            {
                spawnPos.x = (breite / 2 + abstand / 2);
            }
            else if (i % 4 == 0)       //Rechts
            {
                spawnPos.x = (breite + abstand) * 1.5f;
            }
            else            // Mitte Links
            {
                spawnPos.x = -(breite / 2 + abstand / 2);
            }

            spawnPos.y = (-Screen.height + höhe * 2.5f + abstand * 3) - (Mathf.Ceil(i / 4f) * (höhe + abstand));

            GameObject LoadedButton = Instantiate(ButtonToSpawn, spawnPos, Quaternion.identity);
            LoadedButton.transform.SetParent(ButtonHolder.transform, false);
            LoadedButton.GetComponent<RectTransform>().sizeDelta = new Vector2(breite, höhe);
            LoadedButton.name = i.ToString();
            LoadedButton.GetComponentInChildren<TMP_Text>().text = "LEVEL " + i.ToString();
        }

        RectTransform scrollViewContent = GameObject.FindGameObjectWithTag("ScrollViewContent").GetComponent<RectTransform>();

        scrollViewContent.sizeDelta = new Vector2(scrollViewContent.sizeDelta.x, Mathf.Ceil(numberOfLevels / 4f) * (höhe + abstand) + Screen.height - höhe * 2f - abstand * 2);

        Button_Go_Home.transform.position = new Vector3(Screen.height / 7, Screen.height / 7 * 6);

        Button_Go_Home.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height / 7, Screen.height / 7);

        Color32 col = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<Variables>().GetColor();

        bg.color = col;
        Camera.main.backgroundColor = col;

        Game.SetActive(false);
    }

    void Update()
    {
        if (FadeSequence == 1)
        {
            float t = (Time.time - startTime) / 1f;
            Menu.GetComponent<CanvasGroup>().alpha = Mathf.SmoothStep(0, 1, t);
            if (t >= 1)
            {
                FadeSequence = 0;
                general.canClickMenuButtons = true;
            }
        }
        else if (FadeSequence == 2)
        {
            float t = (Time.time - startTime) / 1f;
            Menu.GetComponent<CanvasGroup>().alpha = Mathf.SmoothStep(1, 0, t);
            if (t >= 1)
            {
                FadeSequence = 0;
                setButtonsActive(false);
            }
        }
        else if (FadeSequence == 3)
        {
            float t = (Time.time - startTime) / 1f;
            Game.GetComponent<CanvasGroup>().alpha = Mathf.SmoothStep(0, 1, t);
            if (t >= 1)
            {
                FadeSequence = 0;
                general.canClickGameButtons = true;
            }
        }
        else if (FadeSequence == 4)
        {
            float t = (Time.time - startTime) / 1f;
            Game.GetComponent<CanvasGroup>().alpha = Mathf.SmoothStep(1, 0, t);
            if (t >= 1)
            {
                FadeSequence = 0;
                Game.SetActive(false);
            }
        }
    }

    public void FadeInMenu()
    {
        startTime = Time.time;
        setButtonsActive(true);
        refreshButtons();
        FadeSequence = 1;
    }

    public void FadeOutMenu()
    {
        startTime = Time.time;
        FadeSequence = 2;
    }

    public void FadeInGameOverlay()
    {
        startTime = Time.time;
        Game.SetActive(true);
        Game.GetComponent<CanvasGroup>().alpha = 0;
        FadeSequence = 3;
    }

    public void FadeOutGameOverlay()
    {
        startTime = Time.time;
        FadeSequence = 4;
    }

    private void setButtonsActive(bool b)
    {
        for (int j = 0; j < ButtonHolder.transform.childCount; j++)
        {
            ButtonHolder.transform.GetChild(j).gameObject.SetActive(b);
        }
    }

    private void refreshButtons()
    {
        for (int j = 0; j < ButtonHolder.transform.childCount; j++)
        {
            ButtonHolder.transform.GetChild(j).gameObject.GetComponent<Button>().interactable = j < PlayerPrefs.GetInt("unlockedlevels", 1);
        }
    }

    public void activateSettingsMenu()
    {
        Settings.SetActive(true);
    }

    public void disableSettingsMenu()
    {
        Settings.SetActive(false);
    }
}
