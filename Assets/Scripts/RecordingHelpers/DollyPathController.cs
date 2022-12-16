using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class DollyPathController : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;

    CinemachineTrackedDolly dolly;

    public float timePerSegment = 1;
    public float startAfterSeconds = 1;

    public AnimationCurve rotationCurve;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            dolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dolly != null)
        {
            if(Time.time < startAfterSeconds) return;

            float points = dolly.m_Path.MaxUnit(CinemachinePathBase.PositionUnits.PathUnits);
            float time = (Time.time - startAfterSeconds) / timePerSegment;
            // move the camera along the path
            transform.position = dolly.m_Path.EvaluatePositionAtUnit(time, CinemachinePathBase.PositionUnits.PathUnits);
            transform.rotation = dolly.m_Path.EvaluateOrientationAtUnit(time, CinemachinePathBase.PositionUnits.PathUnits);
            

            transform.Rotate(-rotationCurve.Evaluate(time) * Vector3.up);

        }
    }
}
