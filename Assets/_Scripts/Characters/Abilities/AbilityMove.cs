using UnityEngine;

public class AbilityMove : Ability
{
    public override AbilityData Data => Data as AbilityMoveData;
    private float movespeed;
    private float rotatespeed;


    float horz, vert;
    private Transform cameraTransform;
    private Vector3 camFoward, camRight;
    Vector3 movement;

    public AbilityMove(CharacterControl owner, float movespd, float rotatespd) : base(owner)
    {
        
        this.movespeed = movespd;
        this.rotatespeed = rotatespd;

        cameraTransform = Camera.main.transform;
    }

    public override void Update()
    {
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
        owner.rb.AddForce( movement * movespeed * Time.deltaTime * 10f );
    }

    Quaternion targetRotation = Quaternion.identity;
    void Rotate()
    {
        if (movement != Vector3.zero)
            targetRotation = Quaternion.LookRotation(movement);

        owner.transform.rotation = Quaternion.Slerp(owner.rb.rotation, targetRotation, rotatespeed * Time.deltaTime);
    }
}
