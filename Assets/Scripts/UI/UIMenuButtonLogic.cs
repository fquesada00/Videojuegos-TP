using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameObject))]
public class UIMenuButtonLogic : MonoBehaviour
{
    public GameObject InformationDialog => _informationDialog;
    [SerializeField] private GameObject _informationDialog;
    [SerializeField] private Image _currentDifficultyImage;
    [SerializeField] private List<Sprite> _difficultiesSprite;

    private int _currentDifficulty = 0;
    
    public void LoadLevelScene() => SceneManager.LoadScene("LoadingScene");

    public void ToggleChangeDifficulty() {
        _currentDifficulty = (_currentDifficulty + 1) % _difficultiesSprite.Count;
        _currentDifficultyImage.sprite = _difficultiesSprite[_currentDifficulty];
        _currentDifficultyImage.color = Color32.Lerp(Color.white, new Color32(128,0,0,255), _currentDifficulty / (float)_difficultiesSprite.Count);
    }

    public void ToggleInformationDialog() => _informationDialog.SetActive(!_informationDialog.activeSelf);

    public void CloseGame() => Application.Quit();
}
