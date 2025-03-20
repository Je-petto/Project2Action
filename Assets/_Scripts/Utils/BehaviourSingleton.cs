using UnityEngine;

//template : 틀, 형 -> 사용법: <T>
// 싱글톤 : 관리자 , 전역, 하나(유일)
public abstract class BehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    public static T _instance;

    //외부에서 부를 때
    public static T I
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    
                    GameObject o = new GameObject(typeof(T).Name);
                    _instance = o.AddComponent<T>();

                }
            }
            return _instance;
        }
        //set {} - 사용하지 않음
    }

    protected abstract bool IsDontdestroy();

    protected virtual void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        
        if (IsDontdestroy())
            DontDestroyOnLoad(gameObject);

    }

}
