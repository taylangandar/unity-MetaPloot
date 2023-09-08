using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    Animator animator;
    bool isMainCamera = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            cameraChange();
        }
    }

    void cameraChange()
    {
        if(isMainCamera)
        {
            animator.Play("AnaKamera");
        }
        else
        {
            animator.Play("EkKamera");
        }
        isMainCamera = !isMainCamera;
    }
}