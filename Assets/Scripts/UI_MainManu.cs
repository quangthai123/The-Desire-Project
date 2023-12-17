using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainManu : MonoBehaviour
{
    [SerializeField] private string sceneName = "scene1";
    public void ContinueGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
