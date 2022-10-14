using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Image references
    [SerializeField] private Image _lifebarImage;
    [SerializeField] private Image _currentWeaponImage;
    [SerializeField] private Image _nextWeaponImage;

    // Text references
    [SerializeField] private Text _lifeText;
    [SerializeField] private Text _ammoText;
    [SerializeField] private Text _remainingKillsText;

    // Sprites references
    [SerializeField] private List<Sprite> _weaponSprites;

    // variables
    private float _currentLife;

    void Start()
    {
        // Subscribe to events
        EventsManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
        EventsManager.instance.OnRemainingKillsChange += OnRemainingKillsChange;
        EventsManager.instance.OnAmmoChange += OnAmmoChange;
        EventsManager.instance.OnWeaponChange += OnWeaponChange;
    }

    private void OnCharacterLifeChange(float currentLife, float maxLife)
    {
        _lifebarImage.fillAmount = currentLife / maxLife;
        _lifeText.text = $"{currentLife} / {maxLife}";
        _currentLife = currentLife;
    }

    private void OnRemainingKillsChange(int kills, int objectiveKills)
    {
        _remainingKillsText.text = $"{kills} / {objectiveKills}";
    }

    private void OnAmmoChange(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }

    private void OnWeaponChange(int weaponIndex)
    {
        _nextWeaponImage.sprite = _currentWeaponImage.sprite;
        _currentWeaponImage.sprite = _weaponSprites[weaponIndex];
    }

}
