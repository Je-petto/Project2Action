using UnityEngine;

public class AbilityJump : Ability<AbilityJumpData>
{
    private bool isjumping = false;

    public AbilityJump(AbilityJumpData data, CharacterControl owner) : base(data,owner)
    {   
    }

    public override void Activate()
    {
        if (owner.rb == null || owner.isGrounded == false)
            return;

        isjumping = true;
        elapsed = 0;

        //owner.animator?.SetTrigger("jumpUp");

        owner.animator?.CrossFadeInFixedTime("JUMPUP", 0.2f, 0, 0f); //Base Layer = 0
        
    }
    public override void Deactivate()
    {
        isjumping = true;
        elapsed = 0;

        owner.animator?.CrossFadeInFixedTime("JUMPDOWN", 0.1f, 0, 0f); 
    }

    float elapsed;
    public override void Update()
    {
        if (owner.rb == null || isjumping == false)
            return;
 
        elapsed += Time.deltaTime;

        float t = elapsed / data.jumpDuration;
        //500을 곱한 이유 = jumpForce와 linearVelocity 값의 범위를 맞추기 위한 상수
        float height = data.jumpCurve.Evaluate(t) * data.jumpForce * 500f;

        //owner.agent.Move(Vector3.up * height * Time.deltaTime);
        Vector3 velocity = owner.rb.linearVelocity;
        velocity.y = height * Time.deltaTime;
        owner.rb.linearVelocity = velocity;

        // 점프 시간 종료
        if (elapsed > 0.2f && owner.isLanding)
        {
            owner.ability.Deactivate(data.Flag);
        }
    }

}
