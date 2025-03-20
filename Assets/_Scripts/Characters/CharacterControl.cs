using System;
using UnityEngine;


[Flags]
public enum Ability
{
    None = 0,
    Move = 1 << 0, // 0001
    Jump = 1 << 1, // 0010
    Dodge = 1 << 2, // 0100
    Attack = 1 << 3 // 1000
}


// 캐릭터 관리 (허브)
public class CharacterControl : MonoBehaviour
{
    [Space(10)]
    public Ability abilities;

    void Start()
    {
        abilities.Add(Ability.Move, () => GameManager.I.eventAbilityAdded?.Invoke(abilities));
    }

    void OnEnable()
    {
        GameManager.I.eventAbilityAdded += OneventAbilityAdded;
    }

    void OnDisable()
    {
        GameManager.I.eventAbilityAdded -= OneventAbilityAdded;
    }

    void OneventAbilityAdded(Ability a)
    {
        
        var ability = GetComponent<AbilityMove>();
        if (ability != null)
            return;
            
        gameObject.AddComponent<AbilityMove>();
    }
}
