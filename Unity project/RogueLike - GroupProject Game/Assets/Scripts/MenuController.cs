using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button PlayBtn;
    public Button InfoBtn;
    public Button SettingBtn;
    public Button ExitBtn;

    public void LoadLevel(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
        PlayBtn.interactable = false;
        InfoBtn.interactable = false;
        SettingBtn.interactable = false;
        ExitBtn.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
