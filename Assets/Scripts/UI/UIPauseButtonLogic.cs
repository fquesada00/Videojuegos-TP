using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIPauseButtonLogic : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    public void LoadMenuScene() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    public void CloseGame() => Application.Quit();
    public void ResumeGame() => EventsManager.instance.EventPauseChange(false);

    public void SettingsWindow(bool active){
        _settingsMenu.SetActive(active);
    }
}
