using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LauncherController : MonoBehaviour
{
    public TextMeshProUGUI txtName;

    private Color colorBG;

    void Start()
    {
        // CHEAT: Unlock all Levels
        // PlayerPrefs.SetInt("unlockedlevels", 20);

        GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<Variables>().setRandomColor();
        colorBG = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<Variables>().GetColor();
        Camera.main.backgroundColor = Color.white;
        StartCoroutine(FadeIn());
        StartCoroutine(StartFadeOut());
    }

    IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        txtName.color = new Color32(21, 161, 86, 0);
        float fZwischenergebnis = 1;
        while (txtName.color.a < 1)
        {
            fZwischenergebnis -= Time.deltaTime * 2;
            txtName.color = new Color(1, 1, 1, 1 - fZwischenergebnis);
            Camera.main.backgroundColor = Color.Lerp(Color.white, colorBG, 1 - fZwischenergebnis);
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        while (txtName.color.a > 0)
        {
            txtName.color = new Color(1, 1, 1, txtName.color.a - Time.deltaTime * 2);
            yield return null;
        }

        SceneManager.LoadScene("Game");
    }
}
