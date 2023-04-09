using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
    [SerializeField] private Transform wheelModel;
    private WheelCollider wheelCollider;
    private void Awake()
    {
        TryGetComponent(out wheelCollider);
    }
    private void FixedUpdate()
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelModel.position = pos;
        wheelModel.rotation = rot;
    }
}
