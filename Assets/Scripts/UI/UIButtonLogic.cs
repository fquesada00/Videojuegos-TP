using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class UIButtonLogic : MonoBehaviour
{
    public GameObject InformationDialog => _informationDialog;
    [SerializeField] private GameObject _informationDialog;
    
    public void LoadMenuScene() => SceneManager.LoadScene("Menu");
    public void LoadLevelScene() => SceneManager.LoadScene("LoadingScene");
    public void ToggleInformationDialog() => _informationDialog.SetActive(!_informationDialog.activeSelf);
    public void CloseGame() => Application.Quit();
}
