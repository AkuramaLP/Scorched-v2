using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody rb;

    private Camera mainCamera;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    public bool useController;

    public GunController theGun;

    public Vector3 stop;

    Animator anim;
    int shootHash = Animator.StringToHash("Shoot");

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();

        anim = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if(Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            anim.Play("Walk");
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput * moveSpeed;
        }
        ControllerInput();
	}

    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

    void ControllerInput()
    {
        //Rotate with Mouse
        if(!useController)
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if(groundPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            if(Input.GetMouseButtonDown(0))
            {
                theGun.isFiring = true;
            }

            if(Input.GetMouseButtonUp(0))
            {
                theGun.isFiring = false;
            }
        }

        //Rotate with Controller
        if(useController)
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection * 180, Vector3.up);
            }

            if (Input.GetAxisRaw("RHorizontal") != 0.0f || Input.GetAxisRaw("RVertical") != 0.0f)
            {
                theGun.isFiring = true;
            }

            if(Input.GetAxisRaw("RHorizontal") == 0.0f && Input.GetAxisRaw("RVertical") == 0.0f)
            {
                theGun.isFiring = false;
            }
        }
    }
}