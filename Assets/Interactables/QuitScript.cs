using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit game requested.");
        Application.Quit();

#if UNITY_EDITOR
        // Stop play mode if you're testing in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
