using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainManu : MonoBehaviour
{
    [SerializeField] private string sceneName = "scene 2";
    [SerializeField] private GameObject continueBtn;
    [SerializeField] UI_FadingScrene fadeScreen;

    private void Start()
    {
        if (SaveManager.instance.HasSaveData() == false)
        {
            continueBtn.SetActive(false);
        }
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(sceneName);
        // StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt("extinct", 0);
        PlayerPrefs.SetInt("flaskModifiers", 0);
        //StartCoroutine(LoadSceneWithFadeEffect(1.5f));
        //
    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

   IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
