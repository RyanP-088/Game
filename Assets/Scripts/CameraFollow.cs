using TMPro;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform[] targets;
    [SerializeField] [Range(0, 2)] private int activeTarget;
    private CinemachineVirtualCamera vCam;

    public int GetActiveTarget => activeTarget;
    public void SetAciveTarget(int value) => activeTarget = value;

    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void LateUpdate()
    {
        vCam.Follow = targets[activeTarget];
    }
}
