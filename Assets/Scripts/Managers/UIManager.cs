using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Image references
    [SerializeField] private Image _lifebarImage;
    [SerializeField] private Image _weaponImage;

    // Text references
    [SerializeField] private Text _lifeText;
    [SerializeField] private Text _ammoText;

    // Sprites references
    [SerializeField] private List<Sprite> _weaponSprites;

    // variables
    private float _currentLife;

    void Start()
    {
        // Subscribe to events
        EventsManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
        EventsManager.instance.OnAmmoChange += OnAmmoChange;
        EventsManager.instance.OnWeaponChange += OnWeaponChange;
    }

    private void OnCharacterLifeChange(float currentLife, float maxLife)
    {
        _lifebarImage.fillAmount = currentLife / maxLife;
        _lifeText.text = $"{currentLife} / {maxLife}";
        _currentLife = currentLife;
    }

    private void OnAmmoChange(int currentAmmo, int maxAmmo)
    {
        _ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }

    private void OnWeaponChange(int weaponIndex)
    {
        _weaponImage.sprite = _weaponSprites[weaponIndex];
    }

}
