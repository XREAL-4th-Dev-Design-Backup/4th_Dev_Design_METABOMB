using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
전역으로 게임 전체를 관리하는 스크립트
사실 게임 매니저를 컴포넌트로서 어떤 오브젝트에 꼭 붙여야하는 이유는 없지만
scene에 존재하긴 하는 빈 오브젝트를 만드렁서 매니저 스크립트를 컴보넌트로서 붙이고 사용하는 것을 권장한다.
        매니저 스크립트를 붙일 용도의 빈 오브젝트를 만들자
*/



/*
게임 매니저는 게임 전반을 관리하기 때문에 게임 매니저 스크립트는 
딱 하나만 static으로 만들어두고 여러 곳에서 동일한 인스턴스를 참조한다.


게임 매니저 오브젝트의 스크립트 컴포넌트를 여러 곳에서 공유할 수 있도록 static을 만들어둔다
단 하나만 존재하는 이 게임 매니저 컴포넌트를 리턴받을 수 있는 static 함수를 만들어둔다
*/


/*
싱글톤으로 만들 대상은 @Managers 오브젝트에 붙어있는 Managers.cs
여러 곳에서 동일한 인스턴스에 대해 공유할 수 있도록 static을 선언한다.
여러곳에서 동일한 인스턴스를 리턴받아 사용할 수 있도록 이 인스턴스를 리턴하는 static public함수를 만들어둔다.

*/


/*
@Managers오브젝트에 붙어있는 Managers.cs가 할당되어있는 Instance를 가져와 사용하면 된다.
*/

/*
Init()을 쓰는 이유: Managers.cs의 Start()함수가 실행되기도 전에 다른 스크립트에서 게임 매니저 인스턴스를 사용해야할일이 
생긴다면 먼저 그 곳에서 인스턴스를 만들어두도록 하기 위해

DontdestroyOnload 사용해서 @Manager오브젝트가 씬이 변경되도 삭제되지 않고 유지되도록 안전장치를 걸어주자
*/



public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance{
        get{
            Init();
            return s_instance;
        }
    }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource{
        get{
            return Instance._resource;
        }
    }

    SoundManager _sound = new SoundManager();
    public static SoundManager Sound{
        get{
            return Instance._sound;
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        Init();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Init(){
        if(s_instance == null){
            GameObject obj = GameObject.Find("@Managers");
            if(obj == null){
                obj = new GameObject {name = "@Managers"};
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<Managers>();

            s_instance._sound.Init(); //SoundManager의  Init호출
        }
    }

    public static void Clear(){
        Sound.Clear();
    }
}
