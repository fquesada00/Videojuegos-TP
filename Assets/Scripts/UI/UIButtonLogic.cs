using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIButtonLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("Menu");
    public void LoadLevelScene() => SceneManager.LoadScene("LoadingScene");
    public void LoadSettingsScene() => Debug.Log("Settings scene");
    public void CloseGame() => Application.Quit();
}
