using UnityEngine;

public class AbilityMove : Ability
{
    public override AbilityData Data => Data as AbilityMoveData;
    private float movespeed;
    private float rotatespeed;


    float horz, vert;
    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 camFoward, camRight;
    Vector3 movement;

    public AbilityMove(Transform owner, float movespd, float rotatespd) : base(owner)
    {
        
        this.movespeed = movespd;
        this.rotatespeed = rotatespd;

        rb = owner.GetComponentInChildren<Rigidbody>();
        if (rb == null)
            Debug.LogError("AbilityMove Rigidbody 없음");
        cameraTransform = Camera.main.transform;
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    public override void Update()
    {
        base.Update();

        InputKeyboard();
        Rotate();
        Movement();

    }

    void InputKeyboard()
    {
        horz = Input.GetAxisRaw("Horizontal");
        vert = Input.GetAxisRaw("Vertical");

        camFoward = cameraTransform.forward;
        camRight = cameraTransform.right;

        camFoward.y = 0;
        camRight.y =0;

        camFoward.Normalize();
        camRight.Normalize();

        movement = (camFoward * vert + camRight * horz).normalized;
    }

    void Movement()
    {
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, movement * movespeed, Time.deltaTime * 10f);
    }

    Quaternion targetRotation = Quaternion.identity;
    void Rotate()
    {
        if (movement != Vector3.zero)
            targetRotation = Quaternion.LookRotation(movement);

        rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotatespeed * Time.deltaTime);
    }
}
