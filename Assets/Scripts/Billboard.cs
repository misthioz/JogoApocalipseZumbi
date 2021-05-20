using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
  	//public Transform camTransform;
    private Transform cameraTransform;

	Quaternion originalRotation;
    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        originalRotation = transform.rotation;
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
     	transform.rotation = cameraTransform.rotation * originalRotation;   
    }
}
