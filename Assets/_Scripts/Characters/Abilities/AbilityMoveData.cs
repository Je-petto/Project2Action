using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Move")]

public class AbilityMoveData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Move;
    public float moveSpeed = 10f;
    public float rotateSpeed = 5f;

    public override Ability CreateAbility(Transform owner)
    {
        return new AbilityMove(owner, moveSpeed, rotateSpeed);
    }
}

