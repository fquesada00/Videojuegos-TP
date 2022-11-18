using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    public static GlobalDataManager Instance;

    public bool IsVictory => _isVictory;
    [SerializeField] private bool _isVictory;
    
    private Difficulty _difficulty;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // cuando se cargue una nueva escena este objeto no debe ser destruido
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetIsVictory(bool isVictory) => _isVictory = isVictory;
    
    public Difficulty GetDifficulty() => _difficulty;
    
    public void SetDifficulty(Difficulty difficulty) => _difficulty = difficulty;
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}
