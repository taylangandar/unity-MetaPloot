using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{

    bool isStafe = false;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        animator.SetBool("iS", isStafe);
       


        if(Input.GetKeyDown(KeyCode.F))
        {
            isStafe = !isStafe;
        }

        if(isStafe == true)
        {
            GetComponent<characterController>().hareketTipi = characterController.movementType.Strafe;
        }

        if (isStafe == false)
        {
            GetComponent<characterController>().hareketTipi = characterController.movementType.Directional;
        }
    }
}
