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

    private Coroutine _coroutine;

    //private Image _lifebarImage;
    //private Text _lifeText;
    // Start is called before the first frame update

    void set(bool active)
    {
        this.GetComponent<Canvas>().enabled = active;
        if (!active) _coroutine = null;
    }

    void Start()
    {
        _cooldown = new Cooldown();

    }

    void OnEnable()
    {
        set(false);

    }

    public void setLife(float currentLife, float maxLife)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        set(true);
        _lifebarImage.fillAmount = currentLife / maxLife;
        _lifebarImage.color = colorGradient.Evaluate(currentLife / maxLife);
        _lifeText.text = $"{currentLife} / {maxLife}";

        
         _coroutine = StartCoroutine(_cooldown.CallbackCooldown(2f, () => set(false)));
    }

}
