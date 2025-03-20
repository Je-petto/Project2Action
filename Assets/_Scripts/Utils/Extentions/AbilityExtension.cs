// 반복, 자주, 편리성


using UnityEngine.Events;

public static class AbilityExtension
{
    //해당 어빌리티를 가지고 있냐
    public static bool Has(this ref Ability abilities, Ability a)
    {
        return (abilities & a) == a;
    }

    //해당 어빌리티 추가
    public static void Add(this ref Ability abilities, Ability a, UnityAction oncomplete)
    {
        abilities |= a;

        oncomplete?.Invoke();
    }

    //해당 어빌리티 삭제
    public static void Remove(this ref Ability abilities, Ability a, UnityAction oncomplete)
    {
        abilities &= ~a;

        oncomplete?.Invoke();
    }

    //해당 어빌리티 가지고 있으면 사용
    public static void Use(this ref Ability abilities, Ability a, UnityAction oncomplete)
    {
        if (abilities.Has(a))
            oncomplete?.Invoke();
    }
}

