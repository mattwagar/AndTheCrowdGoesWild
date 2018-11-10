
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HandsCloseTogether : MonoBehaviour
{

    public Transform LeftHand;
    public Transform RightHand;

    public UnityEvent OnBeginFacingCamera;
    public UnityEvent OnEndFacingCamera;


    private float _distance;

    private bool isCloseTogether = false;


    void Start()
    {
        _distance = Vector3.Distance(LeftHand.position, RightHand.position);
    }

    void Update()
    {
		float distance = Vector3.Distance(LeftHand.position, RightHand.position); 
        
		if (distance < _distance/6 && isCloseTogether == false){
            OnBeginFacingCamera.Invoke();
            isCloseTogether = true;
		}
		else if (distance >= _distance/6 && isCloseTogether == true)
        {
            OnEndFacingCamera.Invoke();
            isCloseTogether = false;
		}
    }

}

