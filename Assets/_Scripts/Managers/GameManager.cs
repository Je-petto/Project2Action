using UnityEngine.Events;

//Send
public class GameManager : BehaviourSingleton<GameManager>
{

    protected override bool IsDontdestroy() => true;

//Events
    public UnityAction<Ability> eventAbilityAdded;
    public UnityAction<Ability> eventAbilityRemoved;
    public UnityAction<Ability> eventAbilityUsed;

//Triggers

    public void TriggerAbilityAdd(Ability a)
    {
        eventAbilityAdded?.Invoke(a);
    }
}
