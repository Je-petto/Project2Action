using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Move")]

public class AbilityMoveData : AbilityData
{
    public override AbilityFlag Flag => AbilityFlag.Move;
    public float speed;

    public override Ability CreateAbility(Transform owner)
    {
        return new AbilityMove(owner, speed);
    }
}

