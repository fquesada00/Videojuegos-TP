using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager : MonoBehaviour
{
    public static GlobalDataManager Instance;

    public bool IsVictory => _isVictory;

    public float MouseSensivity => _mouseSensivity;

    [SerializeField] private bool _isVictory;

    private Difficulty? _difficulty;
    private Level _currentLevel = Level.LEVEL_1;

    private float _mouseSensivity = 1f;
    public float SpeedMultiplier = 1f;
    public float DamageMultiplier = 1f;

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

    public Difficulty? GetDifficulty() => _difficulty;

    public void SetDifficulty(Difficulty difficulty) => _difficulty = difficulty;

    public void SetMouseSensivity(float mouseSensivity) => _mouseSensivity = mouseSensivity;

    public Level CurrentLevel => _currentLevel;

    public void NextLevel()
    {
        _currentLevel = _currentLevel.NextLevel();
    }
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}

public abstract class LevelEnumeration
{
    public int Number { get; private set; }
    public string SceneName { get; private set; }
    
    protected LevelEnumeration(int number, string sceneName)
    {
        Number = number;
        SceneName = sceneName;
    }
}

public class Level : LevelEnumeration
{
    public static readonly Level LEVEL_1 = new Level(1, "MainScene");
    public static readonly Level LEVEL_2 = new Level(2, "SecondScene");

    private Level(int number, string sceneName) : base(number, sceneName){}

    public Level NextLevel()
    {
        if (this == LEVEL_1)
        {
            return LEVEL_2;
        }

        return null;
    }
    
    public bool IsLastLevel()
    {
        return this == LEVEL_2;
    }
}