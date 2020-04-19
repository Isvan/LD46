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
    public float interactRange;
    public Vector3 pickupPosition;

    public Vector3 rotationOffSet;

    private static Vector3 verticalAxis = new Vector3(0.5f, 0.0f, 0.5f);
    private static Vector3 horizontalAxis = new Vector3(0.5f, 0.0f, -0.5f);
    private Vector3 moveDirection = Vector3.zero;

    private GameObject heldObject; // object the rat is holding
    private Vector3 posRelativeToHeld;
    private bool objectIsLight; // whether the held object is heavy (dragged) or light (held)
    private GameObject interactable; // If <interact> is pressed, this is what is interacted with

    private bool anchorRotation;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        heldObject = null;
        interactable = null;
        anchorRotation = false;
    }

    void Update()
    {
        MovePlayer();

        // interact with object
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null && interactable != null)
            {
                Pickup pickup = interactable.GetComponent<Pickup>();
                if (pickup != null)
                {
                    // place pickup on top of the player
                    interactable.transform.position = transform.position + pickupPosition;
                    heldObject = interactable;
                    objectIsLight = true;
                }
                else
                {
                    PhysicsObject obj = interactable.GetComponent<PhysicsObject>();
                    if (obj != null && obj.isPullable)
                    {
                        heldObject = interactable;
                        objectIsLight = false;
                        anchorRotation = true;
                        posRelativeToHeld = transform.position - heldObject.transform.position;
                    }
                }
            }
        }

        // drop held object
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (objectIsLight)
            {
                heldObject.transform.position += new Vector3(0, 1, 0);
            }
            heldObject = null;
            anchorRotation = false;
        }

        // update nearest interactable
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -(transform.right));
        if(Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider != null)
            {
                interactable = hit.collider.gameObject;
                // if you want to highlight interactables, do it here
            }
        }
        else
        {
            interactable = null;
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
            if(moveDirection.magnitude > 0 && !anchorRotation)
            {
                transform.rotation = Quaternion.LookRotation(moveDirection);
                transform.Rotate(rotationOffSet);

                if (heldObject != null && objectIsLight)
                {
                    heldObject.transform.rotation = Quaternion.LookRotation(moveDirection);
                    heldObject.transform.Rotate(rotationOffSet);
                }
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

        // held object follows player
        if (heldObject != null)
        {
            if (objectIsLight)
            {
                heldObject.transform.position = transform.position + pickupPosition;
                characterController.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                moveDirection = moveDirection.normalized * pushPower;
                //heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * (pushPower * Time.deltaTime), ForceMode.Impulse);
                heldObject.GetComponent<Rigidbody>().velocity = moveDirection;
                transform.position = posRelativeToHeld + heldObject.transform.position;
            }
        }
        else
        {
            characterController.Move(moveDirection * Time.deltaTime);
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(hit.transform.name == "Door")
        {
            hit.transform.SendMessage("SendMsg", SendMessageOptions.DontRequireReceiver);
        }

        if (hit.gameObject.CompareTag("Trap"))
        {
            Destroy(hit.gameObject, 0);
            Destroy(gameObject, 0);
        }

        if (hit.transform.name == "Water")
        {
            Destroy(gameObject, 0);

        }

        Rigidbody rigidBody = hit.collider.attachedRigidbody;

        // only push non-kinematic rigidbodies
        if (rigidBody == null || rigidBody.isKinematic) return;
        // Don't push objects below us
        if (hit.moveDirection.y < -0.3) return;

        // Don't push up or down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        // HEAVE
        rigidBody.velocity = pushDir * pushPower;
    }

    private void AttachRope(Rope rope, GameObject obj)
    {
        rope.attachedObject = obj;
    }
}