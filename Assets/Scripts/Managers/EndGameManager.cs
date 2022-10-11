using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private Image _endStatusImage;
    [SerializeField] private Text _endStatusText;
    [SerializeField] private Sprite _victorySprite;
    [SerializeField] private Sprite _defeatSprite;

    private void Start()
    {
        _endStatusImage.sprite = GlobalDataManager.Instance.IsVictory ? _victorySprite : _defeatSprite;
        _endStatusText.text = GlobalDataManager.Instance.IsVictory ? "Victory!" : "Defeat!";
        _endStatusText.color = GlobalDataManager.Instance.IsVictory ? Color.yellow : Color.red;
    }
}
