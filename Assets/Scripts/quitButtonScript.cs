using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitButtonScript : MonoBehaviour
{

    // Handles the click event of the Button
    public void ButtonClicked()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }
}
