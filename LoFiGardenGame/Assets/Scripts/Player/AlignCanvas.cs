using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignCanvas : MonoBehaviour
{
    [SerializeField]
    private Quaternion fixedRotation;

    private Transform mainCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        //transform.localRotation = fixedRotation;
        //transform.LookAt(mainCamera);
        transform.rotation = mainCamera.rotation;
    }
}
