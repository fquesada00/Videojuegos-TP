using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Controllers.Utils;

public class HealthDisplay : MonoBehaviour
{

    [SerializeField] private Image _lifebarImage;
    [SerializeField] private Text _lifeText;
    [SerializeField] private Text _hitTemplate;
    [SerializeField] private Gradient colorGradient;

    private Cooldown _cooldown;

    private Coroutine _coroutine;

    //private Image _lifebarImage;
    //private Text _lifeText;
    // Start is called before the first frame update

    void set(bool active)
    {
        _lifebarImage.transform.parent.gameObject.SetActive(active);
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

    private void ShowHit(float damage){
        //instanciate hit text
        Text hitText = Instantiate(_hitTemplate, transform);
        hitText.text = damage.ToString();
        hitText.gameObject.SetActive(true);        
        //hitText.gameObject.transform.parent = this.GetComponent<Canvas>().transform;

    }

    private void updateHealthBar(float currentLife, float maxLife){
        _lifebarImage.fillAmount = currentLife / maxLife;
        _lifebarImage.color = colorGradient.Evaluate(currentLife / maxLife);
        _lifeText.text = $"{currentLife} / {maxLife}";
        _coroutine = StartCoroutine(_cooldown.CallbackCooldown(2f, () => set(false)));

    }

    public void TakeDamage(float currentLife, float maxLife, float damage)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        set(true);
        updateHealthBar(currentLife, maxLife);
        ShowHit(damage);
        
    }

}
