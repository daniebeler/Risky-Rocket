using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CameraController cameraController;

    private Scene activeScene;

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void enableLevel(int level)
    {
        // transform.GetChild(level - 1).gameObject.SetActive(true);
        // activeScene = SceneManager.GetSceneByName("Level " + 1);
        // Debug.Log(activeScene.name);
        SceneManager.LoadScene("Level " + 1, LoadSceneMode.Additive);
        // SceneManager.SetActiveScene(activeScene);
        
    }

    // // called second
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("OnSceneLoaded: " + scene.name);
    //     Debug.Log(mode);
        
    //     Vector2 startPos = getPlatformPosition(1);
    //     cameraController.zoomInCamera(startPos);
    //     playerMovement.setPlayerPos(startPos);
    // }

    public void disableLevels()
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).gameObject.SetActive(false);
        }
    }

    public Vector2 getPlatformPosition(int level)
    {
        // Debug.Log(activeScene.isLoaded);//.GetRootGameObjects()[0].name);
        // Debug.Log(GameObject.FindGameObjectWithTag("LevelHolder").name);

        Vector2 pos = transform.GetChild(level - 1).gameObject.transform.Find("Start").transform.position;
        return pos;
    }
}
