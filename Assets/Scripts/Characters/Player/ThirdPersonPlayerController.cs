using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonPlayerController : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    [SerializeField] Camera activeCam;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float groundAccel;
    [SerializeField] float groundDeaccel;
    [SerializeField] float airDeaccel;
    [SerializeField] float rotationSpeed;
    Vector2 moveInputDir;
    Vector3 cameraRelativeMovementInputDir;
    Vector3 wishDir;
    Vector3 moveDir;
    public bool canMove = true;
    const float groundCheckPadding = 0.3f;
    [SerializeField] float maxSlopeAngle;
    RaycastHit slopeHit;

    [Header("Input References")]
    [SerializeField] InputActionReference moveInput;

    void Awake(){
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CalculateMovement();
        characterController.Move(moveDir * Time.deltaTime);

        UpdateRotation();
        SetAnimationParameters();
    }

    void CalculateMovement(){
        moveInputDir = moveInput.action.ReadValue<Vector2>();

        Vector3 forward = activeCam.transform.forward;
        Vector3 right = activeCam.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 moveInputDirVec3 = new Vector3(moveInputDir.x, 0, moveInputDir.y);

        Vector3 forwardRelativeVerticalInput = moveInputDirVec3.z * forward;
        Vector3 rightRelativeHorizontalInput = moveInputDirVec3.x * right;

        cameraRelativeMovementInputDir = forwardRelativeVerticalInput + rightRelativeHorizontalInput;

        ApplyFriction(1.0f);

        if(IsGrounded()) 
            wishDir = new Vector3(cameraRelativeMovementInputDir.x, 0, cameraRelativeMovementInputDir.z);

        if(wishDir.magnitude > 1) wishDir.Normalize();

        float wishSpeed = wishDir.magnitude;

        wishSpeed *= moveSpeed;

        if(!canMove) wishSpeed = 0.1f;

        if(IsGrounded()){
            Accelerate(wishDir, wishSpeed, groundAccel);
            moveDir.y = Physics.gravity.y * Time.deltaTime;
        } 
        else{
            Accelerate(Vector3.zero, 0, airDeaccel);
            if(!OnSlope()) moveDir.y -= Physics.gravity.y * Time.deltaTime;
        }

        if(OnSlope()){
            moveDir = GetSlopeMoveDir(moveDir);
        }

    }

    void Accelerate(Vector3 wishDir, float wishSpeed, float acceleration){
        float addSpeed;
        float accelSpeed;
        float currentSpeed;

        currentSpeed = Vector3.Dot(moveDir, wishDir);
        addSpeed = wishSpeed - currentSpeed;
        if(addSpeed <= 0) return;
        accelSpeed = acceleration * Time.deltaTime * wishSpeed;
        if(accelSpeed > addSpeed) accelSpeed = addSpeed;


        moveDir.x += wishDir.x * accelSpeed;
        moveDir.z += wishDir.z * accelSpeed;
    }
    
    void ApplyFriction(float amount){
        Vector3 vec = moveDir;
        float speed;
        float newSpeed;
        float control;
        float drop;

        vec.y = 0;
        speed = vec.magnitude;
        drop = 0;

        if(IsGrounded()){
            control = speed < groundDeaccel ? groundDeaccel : speed;
            float friction = 6f;
            drop = control * friction * Time.deltaTime * amount;
        }

        newSpeed = speed - drop;

        if(newSpeed < 0) newSpeed = 0;
        if(speed > 0) newSpeed /= speed;

        moveDir.x *= newSpeed;
        moveDir.z *= newSpeed;
    }

    public bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<CapsuleCollider>().bounds.extents.y + groundCheckPadding);
    }

    bool OnSlope() {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, GetComponent<CapsuleCollider>().bounds.extents.y + groundCheckPadding)){
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    Vector3 GetSlopeMoveDir(Vector3 wishDir) {
        Quaternion slopeRot = Quaternion.FromToRotation(Vector3.up, slopeHit.normal);
        return slopeRot * wishDir;

    }

    void UpdateRotation(){
        Vector3 targetLookDir;
        if(moveInputDir != Vector2.zero){
            targetLookDir = new Vector3(moveDir.x, 0, moveDir.z);
            targetLookDir.Normalize();
            Quaternion toRotation = Quaternion.LookRotation(targetLookDir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void SetAnimationParameters(){
        animator.SetBool("Walking", moveDir.magnitude > 2f);
    }

}
