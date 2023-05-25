using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
소리 발생 근원지 : Audio Source 컴포넌트
어떤 소리를 재생할지 : AudioClip
듣는 사람 귀 : Audio Listner 컴포넌트
    한 Scene안에 하나만 있으면 되는 것으로 기본적으로 MainCamera에 붙어있다.


소리를 발생시킬 오브젝트들에게 AudioSource컴포넌트를 붙여준다
이 컴포넌트에 AudioClip에 원하는 음원을 할당하면 된다
*/



public class SoundManager{
    // Start is called before the first frame update
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    /*
    재생기 AudioSource 가 여러 개일 수 있으므로 이를 _audioSources 배열로 관리한다.
    사운드 종류는 Define의 Sound에 의해 2개(BGM, Effect)이며 각각의  Audio Source 재생기를 가진다. 즉 Audio SOurce 2개
    재생기 당 하나의 클립만 재생할 수 있으므로 이는 곧 2개의 음을 중첩하여 재생할 수 있다는 의미가 된다
    배경음악 재생기 _audioSources[(int)Define.Sound.Bgm]
    효과음 재생기 _audioSources[(int)Define.Sound.Effect]
    원소에 재생기 할당은 밑에 Init()에서
    */

    /*
    Effect사운드는 자주 재생된다. 따라서 계속된 로드는 성능을 떨어뜨릴 수 있도록 미리 로드한다.
    따라서 _audioClips Dictionary에 효과음 클립들을 미리 로드하여 보관하고 여기서 갖다 쓸 것이다.
    Key:클립의 path
    Value: 해당 효과음 클립

    다만 위와 같이 게임 내내 유지되는 빈 오브젝트에 Audio Source 를 붙이면, 거리에 따라 소리 크기가 달라지는 3D 사운드 효과는 낼 수가 없다. 
    배경음악처럼 100% 크게 다 들려야 하는 소리라던가 그런 사운드만 사운드 매니저에서 관리하는 것이 좋다. 

    */


    public void Init(){
        GameObject root = GameObject.Find("@Sound");
        if(root == null){
            root = new GameObject{ name = "@Sound"};
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); //BGM, Effect
            for (int i=0; i<soundNames.Length-1; i++){
                GameObject go = new GameObject { name = soundNames[i]};
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true; //일단 브금 재생기 무한 반복 재생
        }
        Debug.Log("sound initialized");
    }
    /*
    AudioSource를 붙일 오브젝트들을 만들고 컴포넌트를 붙여준다.
    @Sound라는 이름의 오브젝트가 없다면 만들고 이를 부모로 하는 자식 오브젝트 Bgm, Effect라는 이름의 오브젝트들을 만들고
    각각 이에 AUdioSource를 붙일 것이다.

    이 AudioSource들은 게임 내내 파괴되지 않고 살아있도록 DontDestroyOnLoad함수로 인해 보호
    
    */

    public void Clear(){
        //재생기 전부ㅡ 재생 스탑, 음반빼기
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        //효과음 dictionary 비우기
        _audioClips.Clear();
    }

    /*
    AudioSource가 붙어있는 두 오브젝트 Bgm, Effect는 DontDestroyOnLoad를 통해 파괴 안되도록 관리되므로 
    게임이 아주 오랫동안 지속이 되고 새로운 효과음들을 많이 재생시킨다면 _audioClip Dictionary가 계속 추가되어
    메모리 부족해질 수 있는 문제도 생각해야합니다.
    
    그래서 _audioClip Dictionary를 비워주는 함수를 만들어줌
    */

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        Debug.Log($"2. Inside Play : {type}");
        Debug.Log($"2. Inside Play : {audioClip}");
        if(audioClip == null)
        {
            return;
        }
        //
        if(type == Define.Sound.Bgm) // 브금 배경음악 재생
        {
            Debug.Log($"Inside Bgm");
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            Debug.Log($"{_audioSources.Length}");
            if(audioSource.isPlaying){
                audioSource.Stop();
            }

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else{ // effect 효과음 재생
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);

        }

    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Debug.Log($"1. AudioClip {audioClip}");
        Play(audioClip, type, pitch);
    }

    /*
    음원의 AudioClip를 받아재생하는 Play
    BGM 배경음악 재생시
    BGM 재생기를 통해 재생

    Effect 효과음 재생 시
    효과음 재생기를 통해 재생
    원하는 클립을 중첩해서 재생 가능하게끔 palyoneshot 함수를 통해 재생한다.

    */

    /*
    음원의 path를 받아 재생하는 Play
    해당 path에서 GetOrAddAudioClip함수를 통해 해당 클립을 Sound로부터 로드해온다.
    */


    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect){
        if(path.Contains("Sounds/") == false){
            path = $"Sounds/{path}"; // Sounds folder안에 저장될 수 있도록
        }
        AudioClip audioClip = null;

        if(type == Define.Sound.Bgm){
            audioClip = Managers.Resource.Load<AudioClip>(path);
            //Debug.Log($"AudioClip {path}");
           
        }
        else // Effect 효과음 클립 붙이기
        {
            if(_audioClips.TryGetValue(path, out audioClip) == false){
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }
        //Debug.Log($"AudioCLip {path}");
        if(audioClip == null){
            Debug.Log($"AudioCLip missing {path}");
        }
        return audioClip;
    }

    /*
    path를 통해 해당 클립을 AudioClip으로서 로드하고 리턴한다.
    효과음의 경우 굉장히 자주 사용되기 때문에 딱 한번만 로드해서 _audioClip Dictionary에 보관해두고 여기서 가져올 거ㅏㅅ
    _audioClip Dictionary에 해당 path Key가 존재하는지 검사한다.
    TryValue를 통해 path  Key가 존재한다면 한번 로드된적이 있어  Dictionaryy에 보관되어 있는 클립인 것이므로 그냥 
    그것을 리턴하면 되고 없다면 path를 통해 오디오 클립을 로드하여 추가하여 보관한다.
    최초로드 제외하고는 딕셔너리에서 효과음 클립을 로컬폴더로부터의 로드없이 가져오게 되므로 성능상효율적이다.
    */

}


