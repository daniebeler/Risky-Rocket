using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public bool canClickMenuButtons = false;
    public bool canClickGameButtons = false;

    private LevelsController levelsController;
    private CanvasController canvasController;
    private PlayerMovement playerMovement;
    private CameraController cameraController;

    void Start()
    {
        Application.targetFrameRate = 60;

        canvasController = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<CanvasController>();
        levelsController = GameObject.FindGameObjectWithTag("LevelHolder").GetComponent<LevelsController>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        cameraController = Camera.main.GetComponent<CameraController>();

        levelsController.disableLevels();

        canvasController.FadeInMenu();
    }

    public void FadeOutGame()
    {
        if (canClickGameButtons)
        {
            canClickGameButtons = false;
            cameraController.zoomOutCamera();
            StartCoroutine(showMenu());
        }

    }

    public void openSettings()
    {
        if (canClickMenuButtons)
        {
            canClickMenuButtons = false;
            canvasController.activateSettingsMenu();
        }
    }

    public void closeSettings()
    {
        canClickMenuButtons = true;
        canvasController.disableSettingsMenu();
    }

    IEnumerator showMenu()
    {
        playerMovement.lockMovement();
        canvasController.FadeOutGameOverlay();
        yield return new WaitForSeconds(2f);
        canvasController.FadeInMenu();
    }

    public void loadLevel(int level)
    {
        if (canClickMenuButtons)
        {
            canClickMenuButtons = false;
            canvasController.FadeOutMenu();
            levelsController.enableLevel(level);
            PlayerPrefs.SetInt("currentlevel", level);
        }
    }
}
