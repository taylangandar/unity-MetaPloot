using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [Header("Metrics")]
    public float damp;
    [Range(1, 20)]
    public float rotationSpeed;
    [Range(1, 20)]
    public float StrafeTurnSpeed;

    public float sprintFOV;
   
    float inputX;
    float inputY;
    float maxSpeed;
    float normalFOV;

    public Transform Model;

    Animator Anim;
    Vector3 StickDirection;
    Camera mainCamera;

    public KeyCode sprintButton = KeyCode.LeftShift;
    public KeyCode walkButton = KeyCode.LeftControl;

    public enum movementType
    {
        Directional,
        Strafe
    };
    public movementType hareketTipi;


    void Start()
    {
        Anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        normalFOV = mainCamera.fieldOfView;
    }

    void LateUpdate()
    {
        Movement();
        InputMove();
        InputRotation();
    }

    void Movement()
    {  
        
        if(hareketTipi == movementType.Strafe)
        {

            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

            Anim.SetFloat("inputX", inputX, damp, Time.deltaTime * 10);
            Anim.SetFloat("inputY", inputY, damp, Time.deltaTime * 10);

            var hareketEdiyor = inputX != 0 || inputY != 0;
            if(hareketEdiyor)
            {
                float yanCamera = mainCamera.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yanCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);
                Anim.SetBool("strafeMoving", true);
            }
            else
            {
                Anim.SetBool("strafeMoving", false);
            }
        }

        if (hareketTipi == movementType.Directional)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");

            StickDirection = new Vector3(inputX, 0, inputY);

            if (Input.GetKey(sprintButton))
            {
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, sprintFOV, Time.deltaTime * 2);
                maxSpeed = 2;
                inputX = 2 * Input.GetAxis("Horizontal");
                inputY = 2 * Input.GetAxis("Vertical");
            }
            else if (Input.GetKey(walkButton))
            {
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFOV, Time.deltaTime * 2);
                maxSpeed = 0.2f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
            else
            {
                mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, normalFOV, Time.deltaTime * 2);
                maxSpeed = 1;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
        }
    }

    void InputMove()
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, maxSpeed).magnitude, damp, Time.deltaTime * 10);
    }

    void InputRotation()
    {
        Vector3 rotOfset = mainCamera.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOfset , Time.deltaTime * rotationSpeed);
    }
}
