using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Load제네릭 사용자 지정 함수 정의
where T:Object, 부모클래스가 object인 타입만 받을 수 있도록 제약을 걸고
Resource.Load<T>(path)
Resource폴더를 시작 위치로 한 path에 해당하는 T타입의 에셋파일을 불러오고 이를 리턴한다.
Instantiate사용자 지정함수 정의
Load를 사용해 prefab에 path에 해당하는 GameObject타입의 에셋을 할당한다.
Resource의 Prefab에서 찾아온다
성공적으로 찾았다면 Object.Instantiate 리턴

*/
public class ResourceManager : MonoBehaviour
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if(prefab == null){
            Debug.Log($"Filled to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go){
        if(go == null){
            return;
        }
        Object.Destroy(go);
    }

}
