using UnityEngine;

public class AbilityJump : Ability<AbilityJumpData>
{
    private bool isjumping = false;

    public AbilityJump(AbilityJumpData data, CharacterControl owner) : base(data,owner)
    {   
    }

    public override void Activate()
    {
        if (owner.cc == null || owner.isGrounded == false)
            return;

        isjumping = true;
        elapsed = 0;
        
    }
    public override void Deactivate()
    {
        isjumping = true;
        elapsed = 0;
    }

    float elapsed;
    public override void Update()
    {
        if (owner.cc == null || isjumping == false)
            return;
 
        elapsed += Time.deltaTime;

        float t = elapsed / data.jumpDuration;
        float height = data.jumpCurve.Evaluate(t) * data.jumpForce;

        owner.cc.Move(Vector3.up * height * Time.deltaTime);

        // 점프 시간 종료
        if (elapsed >= data.jumpDuration || (elapsed > 0.1f && owner.isGrounded))
        {
            owner.ability.Deactivate(data.Flag);
        }
    }

}
