using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    void Start()
    {
        // 비디오 플레이어와 렌더 텍스처 초기화
        videoPlayer.targetTexture = renderTexture;

        // 비디오 재생
        videoPlayer.Play();

        // 비디오 재생 완료 이벤트 구독
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished!");
        SceneConManager.Instance.MoveScene("MenuScene");
        // 비디오 재생이 완료되었을 때 수행할 로직
    }

    void OnDestroy()
    {
        // 이벤트 구독 해제
        videoPlayer.loopPointReached -= OnVideoFinished;
    }
}