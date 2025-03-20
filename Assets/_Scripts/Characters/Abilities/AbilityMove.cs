using UnityEngine;

public class AbilityMove : Ability
{
    public override AbilityData Data => Data as AbilityMoveData;

    private float speed;
    float horz, vert;
    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 camFoward, camRight;
    Vector3 movement;

    public AbilityMove(Transform owner, float spd) : base(owner)
    {
        this.speed = spd;

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
        rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, movement * speed, Time.deltaTime * 10f);
    }

}
