using System.Collections.Generic;
using UnityEngine;
using CustomInspector;


// 캐릭터 컨트롤 : 허브 역할 - 캐릭터 관리
public class CharacterControl : MonoBehaviour
{
    [HideInInspector]public AbilityControl ability;

    [ReadOnly]public bool isGrounded;
    [ReadOnly]public Rigidbody rb;

    public List<AbilityData> initialAbilities;
    
    void Awake()
    {
        if (TryGetComponent(out ability) == false)
            Debug.LogWarning("CharacterControl ] AbilityControl 없음");

        if (TryGetComponent(out rb) == false)
            Debug.LogWarning("CharacterControl ] Rigidbody 없음");
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.1f);
        
        InputKeyboard();
    }

    
    void InputKeyboard()
    {

        if (Input.GetButtonDown("Jump"))
            ability.Trigger(AbilityFlag.Jump);
    }

    void Start()
    {
        foreach( var dat in initialAbilities )
            ability.Add(dat);
    }

}





