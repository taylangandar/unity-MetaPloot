using UnityEngine;
using Photon.Pun;

public class characterMovement : MonoBehaviourPunCallbacks
{
    public Rigidbody _physics;
    private float speed = 15f;
    private Animator _animator;
    public float x, z;

    void Start()
    {
        _physics = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if(photonView.IsMine)
            {
                _animator.SetBool("IsMine", true);
            }
        }
    }

    void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            Vector3 vector3 = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed);
            transform.position += vector3;
            _animator.SetFloat("Horizontal", x);
            _animator.SetFloat("Horizontal", z);
        }
    }
}