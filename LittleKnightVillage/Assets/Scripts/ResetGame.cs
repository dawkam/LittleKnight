using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetGame : MonoBehaviour
{
    public GameObject EndScreen;
    void Update()
    {
        if (EndScreen != null && EndScreen.activeSelf && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Interact")))
        {
            SceneManager.UnloadSceneAsync("Game");
            SceneManager.LoadSceneAsync("Start");
        }
    }


}
