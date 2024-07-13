using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneInit : MonoBehaviour
{
    
    void Start()
    {
        // 메뉴bgm재생
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayBGM(SoundType.메뉴씬Bgm);
    }

    public void PlayBtnSelectSfx()
    {
        SoundManager.Instance.PlaySfx(SoundType.버튼선택음sfx);
    }

    public void PlayBtnClickSfx()
    {
        SoundManager.Instance.PlaySfx(SoundType.버튼클릭음sfx);
    }
    
}
