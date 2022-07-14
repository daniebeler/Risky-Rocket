using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    private General general;

    void Start()
    {
        general = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<General>();
        GetComponent<Button>().onClick.AddListener(() => ButtonClicked());
    }

    public void ButtonClicked()
    {
        if (gameObject.name == "btnHome")
        {
            general.FadeOutGame();
        }
        else if (gameObject.name == "btnSettings")
        {
            general.openSettings();
        }
        else if (gameObject.name == "btnCloseSettings")
        {
            general.closeSettings();
        }
        else
        {
            int z = 0;
            int.TryParse(gameObject.name, out z);
            general.loadLevel(z);
        }
    }
}
