using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLook : MonoBehaviour
{
    Animator Anim;
    Camera mainCamera;


    void Start()
    {
        Anim = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void OnAnimatorIK(int layerIndex)
    {
        Anim.SetLookAtWeight(.6f, .2f, 1.2f, .5f, .5f);
        Ray LookAtRay = new Ray(transform.position, mainCamera.transform.forward);
        Anim.SetLookAtPosition(LookAtRay.GetPoint(25));
    }
}
