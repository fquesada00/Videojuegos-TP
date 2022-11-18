using System.Collections;
using System.Collections.Generic;
using Managers;
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
    
    private void Start()
    {
        _currentDifficulty = 0;
        _currentDifficultyImage.sprite = _difficultiesSprite[_currentDifficulty];
        ChangeGameDifficulty(_currentDifficulty);
    }

    public void LoadLevelScene() => SceneManager.LoadScene("LoadingScene");

    public void ToggleChangeDifficulty() {
        _currentDifficulty = (_currentDifficulty + 1) % _difficultiesSprite.Count;
        _currentDifficultyImage.sprite = _difficultiesSprite[_currentDifficulty];
        _currentDifficultyImage.color = Color32.Lerp(Color.white, new Color32(128,0,0,255), _currentDifficulty / (float)_difficultiesSprite.Count);
        ChangeGameDifficulty(_currentDifficulty);
    }

    public void ToggleInformationDialog() => _informationDialog.SetActive(!_informationDialog.activeSelf);

    public void CloseGame() => Application.Quit();

    private void ChangeGameDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                GlobalDataManager.Instance.SetDifficulty(Difficulty.EASY);
                break;
            case 1:
                GlobalDataManager.Instance.SetDifficulty(Difficulty.MEDIUM);
                break;
            case 2:
                GlobalDataManager.Instance.SetDifficulty(Difficulty.HARD);
                break;
            default:
                GlobalDataManager.Instance.SetDifficulty(Difficulty.EASY);
                break;
        }
    }
}
