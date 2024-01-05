using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainManu : MonoBehaviour
{
    [SerializeField] private string sceneName = "scene2";
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
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        SaveManager.instance.DeleteSavedData();
        SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));

    }

    public void ExitGame()
    {
        Debug.Log("Exit game");
    }

   IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }
}
