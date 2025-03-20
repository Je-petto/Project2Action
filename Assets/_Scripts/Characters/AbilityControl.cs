using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomInspector;


// abilityDatas : 외부에서 능력 부여/회수 인터페이스
// abilities : abilityDatas 갱신해서 행동
public class AbilityControl : MonoBehaviour
{
    [Space(10), Title("ABILITY SYSTEM", underlined:true, fontSize = 15, alignment = TextAlignment.Center), HideField] public bool _t0;

    [Space(10), ReadOnly] public AbilityFlag flags = AbilityFlag.None;

    [Space(10), SerializeField] List<AbilityData> datas = new List<AbilityData>();

    //<Key 값, Value 값>
    private readonly Dictionary<AbilityFlag, Ability> actives = new Dictionary<AbilityFlag, Ability>();

    public void AddAbility(AbilityData d, bool immediate)
    {
        if (datas.Contains(d) == true || d == null)
            return;
        
        datas.Add(d);
        var ability = d.CreateAbility(transform);

        flags.Add(d.Flag, null);

        if (immediate)
           actives[d.Flag] = ability;
    }

    public void RemoveAbility(AbilityData d)
    {
        if (datas.Contains(d) == false || d == null)
            return;
        
        actives[d.Flag].Deactivate();
        
        datas.Remove(d);
        flags.Remove(d.Flag, null);
        actives.Remove(d.Flag);
    }
    
    private void Update()
    {
        foreach(var a in actives)
            a.Value.Update();
    }


    [HorizontalLine("TEMP CODE"), HideField] public bool _l0;
//TEMP
    public AbilityData testdata;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        AddAbility(testdata, true);
    }
//TEMP

}
