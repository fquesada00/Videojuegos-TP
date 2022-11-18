using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class DollyPathController : MonoBehaviour
{

    private CinemachineVirtualCamera virtualCamera;

    CinemachineTrackedDolly dolly;

    public Vector2 TimeRange = new Vector2(1, 10);

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
            dolly.m_PathPosition = (Time.time - TimeRange.x)/(TimeRange.y - TimeRange.x);

            // move the camera along the path
            transform.position = dolly.m_Path.EvaluatePosition(dolly.m_PathPosition);
            transform.rotation = dolly.m_Path.EvaluateOrientation(dolly.m_PathPosition);
        }
    }
}
