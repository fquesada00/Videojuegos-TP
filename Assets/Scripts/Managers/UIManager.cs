using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Image references
    [SerializeField] private Image _lifebarImage;
    [SerializeField] private List<Image> _skillsCooldownImage;
    [SerializeField] private Image _currentWeaponImage;
    [SerializeField] private Image _nextWeaponImage;
    [SerializeField] private GameObject _pauseMenu;

    // Text references
    [SerializeField] private Text _lifeText;
    [SerializeField] private Text _remainingKillsText;

    // Sprites references
    [SerializeField] private List<Sprite> _weaponSprites;

    // variables
    private float _currentLife;
    private List<float> _skillsCooldowns = new List<float> {0, 0};
    private List<float> _skillsCurrentCooldowns = new List<float> {0, 0};

    void Start()
    {
        // Subscribe to events
        EventsManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
        EventsManager.instance.OnSkillCooldownChange += OnSkillCooldownChange;
        EventsManager.instance.OnSkillCooldownReduce += OnSkillCooldownReduce;
        EventsManager.instance.OnRemainingKillsChange += OnRemainingKillsChange;
        EventsManager.instance.OnWeaponChange += OnWeaponChange;
        EventsManager.instance.OnPauseChange += OnPauseChange;
    }

    private void OnCharacterLifeChange(float currentLife, float maxLife)
    {
        _lifebarImage.fillAmount = currentLife / maxLife;
        _lifeText.text = $"{currentLife} / {maxLife}";
        _currentLife = currentLife;
    }

    private void OnSkillCooldownChange(int skillIndex, float cooldown)
    {
        _skillsCooldowns[skillIndex] = cooldown;
        _skillsCurrentCooldowns[skillIndex] = cooldown;
    }

    private void OnSkillCooldownReduce(float timePassed)
    {
        for (int i = 0; i < _skillsCooldowns.Count; i++)
        {
            if (_skillsCurrentCooldowns[i] > 0)
            {
                _skillsCurrentCooldowns[i] -= timePassed;
                if (_skillsCurrentCooldowns[i] < 0)
                    _skillsCurrentCooldowns[i] = 0;

                _skillsCooldownImage[i].fillAmount = _skillsCurrentCooldowns[i]/_skillsCooldowns[i];
            }
        }
    }

    private void OnRemainingKillsChange(int kills, int objectiveKills)
    {
        _remainingKillsText.text = $"{kills} / {objectiveKills}";
    }

    private void OnWeaponChange(int weaponIndex)
    {
        _nextWeaponImage.sprite = _currentWeaponImage.sprite;
        _currentWeaponImage.sprite = _weaponSprites[weaponIndex];
    }

    private void OnPauseChange(bool isPaused)
    {
        _pauseMenu.SetActive(isPaused);
        if (isPaused)
        {
            // enable cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1f;
            _pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
