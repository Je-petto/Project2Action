using System;
using UnityEngine;

// abstract  : 부모, 자식 - 변수(o)
// interfere : 친척      -  변수(x)

[Flags]
public enum AbilityFlag
{
    None = 0,
    Move = 1 << 0, // 0001
    Jump = 1 << 1, // 0010
    Dodge = 1 << 2, // 0100
    Attack = 1 << 3 // 1000
}

//지속성 여부
public enum AbilityEffect
{
    INSTANT,
    DURATION,
    INFINITE,
}

// 데이터 담당
// 1. Ability의 타입을 정한다
// 2. Ability 타입에 맞게 생성한다 
public abstract class AbilityData : ScriptableObject
{
    public abstract AbilityFlag Flag { get; }
    public abstract Ability CreateAbility( CharacterControl owner );
}

// 행동 담당
// abstract와 virtual의 차이 :
// abstract (정의 - 필수)
// virtual (정의 - 선택)
public abstract class Ability
{
    public abstract AbilityData Data { get; }

    protected CharacterControl owner;

    public Ability(CharacterControl owner) => this.owner = owner;

    // 어빌리티 활성
    public virtual void Activate() {}
    // 어빌리티 비활성
    public virtual void Deactivate() {}
    // 어빌리티 계속 업데이트
    public virtual void Update() {}
    
}
