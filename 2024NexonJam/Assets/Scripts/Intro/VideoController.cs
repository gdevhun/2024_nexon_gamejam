using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    void Start()
    {
        // videoPlayer와 renderTexture가 null이 아닌지 확인
        if (videoPlayer != null && renderTexture != null)
        {
            // 비디오 플레이어와 렌더 텍스처 초기화
            videoPlayer.targetTexture = renderTexture;

            // 비디오 재생
            videoPlayer.Play();

            // 비디오 재생 완료 이벤트 구독
            videoPlayer.loopPointReached += OnVideoFinished;
        }
        else
        {
            Debug.LogError("VideoPlayer or RenderTexture is not assigned.");
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("MenuScene");
        // 비디오 재생이 완료되었을 때 수행할 로직
    }

    void OnDestroy()
    {
        // 이벤트 구독 해제
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }
}