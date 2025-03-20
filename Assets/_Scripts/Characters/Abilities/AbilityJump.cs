using UnityEngine;

public class AbilityJump : Ability
{
    public override AbilityData Data => Data as AbilityJumpData;

    private float jumpforce;
    
    private bool jump;
    private bool isGrounded;
    private float gravityScale = 2f;
    private Rigidbody rb;

    public AbilityJump(Transform owner, float jumpforce) : base(owner)
    {
        this.jumpforce = jumpforce;
        this.jump = false;
        this.isGrounded = false;
        this.gravityScale = 2f;

        rb = owner.GetComponentInChildren<Rigidbody>();
        if (rb == null)
            Debug.LogError("AbilityMove Rigidbody 없음");
        
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

        isGrounded = Physics.Raycast(owner.position + Vector3.up, Vector3.down, 1.1f);

        CustomGravity();
        InputKeyboard();

        Jump();

    }

    void InputKeyboard()
    {
        jump = Input.GetButtonDown("Jump");
    }

    void Jump()
    {
        if (jump && isGrounded)
            rb.AddExplosionForce(jumpforce, owner.position, 5f);
    }

    void CustomGravity()
    {
        rb.useGravity = isGrounded;

        if (!isGrounded)
            rb.AddForce(Physics.gravity * gravityScale, ForceMode.Acceleration);
    }


}
