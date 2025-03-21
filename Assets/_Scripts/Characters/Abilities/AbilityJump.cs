using UnityEngine;

public class AbilityJump : Ability
{
    public override AbilityData Data => Data as AbilityJumpData;

    private float jumpforce;

    public AbilityJump(CharacterControl owner, float jumpforce) : base(owner)
    {
        this.jumpforce = jumpforce;
        
    }

    public override void Activate()
    {
        Jump();
    }

    void Jump()
    {
        if (owner.isGrounded == false)
            return;
        
        owner.rb.AddExplosionForce(jumpforce, owner.transform.position, 5f, 1f, ForceMode.Acceleration);
    }

}
