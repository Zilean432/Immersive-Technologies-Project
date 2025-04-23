using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public float sceneNumber = 1;

    public void LoadScene()
    {
        switch(sceneNumber)
        {
            case 0:
                SceneManager.LoadScene("ProjectScene");
                break;

            case 1:
                SceneManager.LoadScene("MenuScene");
                break;
        }
    }
}
