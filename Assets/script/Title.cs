using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private bool firstPush = false;

    public void PressStart()
    {
        if (!firstPush)
        {
            SceneManager.LoadScene("gameMain");
            firstPush = true;
        }
    }
}