using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Movement : MonoBehaviourPun {

    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Forward;
    public KeyCode Backward;

    [SerializeField]
    private float MoveSpeed;
    private Rigidbody body;

    public GameObject cam;

    public Animator anim;


    void Start() {
        if (photonView.IsMine) {
            body = GetComponent<Rigidbody>();
            cam.SetActive(true);
        }
    }

    void FixedUpdate() {
        if (photonView.IsMine) {
            anim.SetBool("Grounded", true);
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            float controllerX = Input.GetAxis("Horizontal");
            float controllerY = Input.GetAxis("Vertical");

            if (Input.GetKey(Forward)) {
                transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            }

            if (Input.GetKey(Backward)) {
                transform.Translate(Vector3.back * MoveSpeed * Time.deltaTime);
            }

            Vector3 direction = new Vector3(controllerX, 0, controllerY);
            if (direction.magnitude > 0.3f) {
                Vector3 movement = direction * MoveSpeed * Time.deltaTime;
                transform.Translate(movement);
                anim.SetFloat("MoveSpeed", (direction.magnitude));
            } else {
                anim.SetFloat("MoveSpeed", 0);
            }

            gameObject.transform.Rotate(new Vector3(0, x, 0));
            cam.transform.Rotate(new Vector3(-y, 0, 0));
        }
    }
}
