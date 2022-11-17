using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackwards : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.back * _curve.Evaluate(Time.time) * Time.deltaTime);
    }
}
