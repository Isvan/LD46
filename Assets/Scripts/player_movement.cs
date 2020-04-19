using UnityEngine;
using System.Collections;

// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.

public class player_movement : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float pushPower = 100f;
    public Vector3 pickupPosition;

    public Vector3 rotationOffSet;

    private static Vector3 verticalAxis = new Vector3(0.5f, 0.0f, 0.5f);
    private static Vector3 horizontalAxis = new Vector3(0.5f, 0.0f, -0.5f);

    private Vector3 moveDirection = Vector3.zero;
    private GameObject pickedUpObject;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        pickedUpObject = null;
    }

    void Update()
    {
        MovePlayer();
        if (pickedUpObject != null)
        {
            pickedUpObject.transform.position = transform.position + pickupPosition;
        }
    }

    private void MovePlayer()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            moveDirection = verticalAxis * verticalInput + horizontalAxis * horizontalInput;
            moveDirection.Normalize();
            moveDirection *= speed;
            if(moveDirection.magnitude > 0)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
                transform.Rotate(rotationOffSet);
            }
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(hit.transform.name == "Door")
        {
            hit.transform.SendMessage("SendMsg", SendMessageOptions.DontRequireReceiver);
        }

        if (hit.gameObject.CompareTag("Trap") || hit.transform.name == "Water")
        {
            Destroy(gameObject, 0);
        }

        Rigidbody rigidBody = hit.collider.attachedRigidbody;
        GameObject collidee = hit.collider.gameObject;
        Pickup pickup = collidee.GetComponent<Pickup>();

        if (pickup != null && pickedUpObject == null)
        {
            // place pickup on top of the player
            collidee.transform.position = transform.position + pickupPosition;
            pickedUpObject = collidee;
            return;
        }

        // only push non-kinematic rigidbodies
        if (rigidBody == null || rigidBody.isKinematic) return;
        // Don't push objects below us
        if (hit.moveDirection.y < -0.3) return;

        // Don't push up or down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        // HEAVE
        rigidBody.velocity = pushDir * pushPower;
    }
}