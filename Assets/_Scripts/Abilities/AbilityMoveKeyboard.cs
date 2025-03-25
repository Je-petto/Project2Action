using UnityEngine;
using UnityEngine.Rendering;

public class AbilityMoveKeyboard : Ability<AbilityMoveKeyboardData>
{

    float horz, vert;
    private Transform cameraTransform;
    private Vector3 camFoward, camRight;
    private Vector3 direction;
    private float velocity;

    public AbilityMoveKeyboard(AbilityMoveKeyboardData data, CharacterControl owner) : base(data, owner)
    {
        cameraTransform = Camera.main.transform;
        velocity = data.rotatePerSec;

    }

    public override void FixedUpdate()
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

        direction = (camFoward * vert + camRight * horz).normalized;
    }

    void Movement()
    {
        owner.cc.Move(direction * data.movePerSec * Time.deltaTime);

        if (owner.isGrounded ==true)
        {
            float velocity = Vector3.Distance(Vector3.zero, owner.cc.velocity);
            float targetspeed = Mathf.Clamp01(velocity / data.movePerSec);
            float movespd = Mathf.Lerp(owner.animator.GetFloat("moveSpeed"), targetspeed, Time.deltaTime * 18f);

            owner.animator?.SetFloat("moveSpeed", movespd);
        }


    }

    void Rotate()
    {
        if (direction == Vector3.zero)
            return;
        
        // Atan2의 역할 : Vector2(x,z) 가 있을 대 해당 각도를 알려준다 (radian)
        // pie(π, 3.14) = 180도, 2π = 360도
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float smoothangle = Mathf.SmoothDampAngle(owner.transform.eulerAngles.y, angle, ref velocity, 0.1f);
        owner.transform.rotation = Quaternion.Euler(0f, smoothangle, 0f );

    }
}
