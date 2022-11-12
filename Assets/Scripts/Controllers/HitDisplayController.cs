using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitDisplayController : MonoBehaviour
{

    [SerializeField] private float duration;
    [SerializeField] private float speed;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
       //Destroy after duration
        Destroy(gameObject, duration);
        GetComponent<Text>().CrossFadeAlpha(0, duration, false);
        direction = new Vector3(Random.Range(-1f, 1f), 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Move up with random noise
        transform.position += speed * direction * Time.deltaTime;


    

        
    }
}
