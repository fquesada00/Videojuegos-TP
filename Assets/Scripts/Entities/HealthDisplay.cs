using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controllers.Utils;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField] private Image _lifebarImage;
    [SerializeField] private Text _lifeText;

    [SerializeField] private Gradient colorGradient;

    private Cooldown _cooldown;

    //private Image _lifebarImage;
    //private Text _lifeText;
    // Start is called before the first frame update
    void Start()
    {
        _cooldown = new Cooldown();
        //this.GetComponent<Canvas>().enabled = false;  
        this.gameObject.SetActive(false);

    }

    public void setLife(float currentLife, float maxLife)
    {
        
        this.gameObject.SetActive(true);
        _lifebarImage.fillAmount = currentLife / maxLife;
        _lifebarImage.color = colorGradient.Evaluate(currentLife / maxLife);
        _lifeText.text = $"{currentLife} / {maxLife}";

        StartCoroutine(_cooldown.CallbackCooldown(2f, () => this.gameObject.SetActive(false)));
    }

}
