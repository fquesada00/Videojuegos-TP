using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIEndButtonLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("Menu");
    public void CloseGame() => Application.Quit();
}
