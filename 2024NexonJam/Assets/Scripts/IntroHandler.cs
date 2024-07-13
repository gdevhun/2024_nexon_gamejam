using UnityEngine;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour
{
    public Sprite[] introImages; // 설명 이미지를 저장할 배열
    public Image displayImage; // 이미지를 보여줄 UI Image 컴포넌트

    private int currentImageIndex = 0; // 현재 보여지는 이미지의 인덱스

    void Start()
    {
        // 첫 번째 이미지로 초기화
        if (introImages.Length > 0)
        {
            displayImage.sprite = introImages[currentImageIndex];
        }
    }

    void Update()
    {
        // 아무 키나 눌렀을 때 다음 이미지로 넘어감
        if (Input.anyKeyDown)
        {
            currentImageIndex++;

            if (currentImageIndex < introImages.Length)
            {
                displayImage.sprite = introImages[currentImageIndex];
            }
            else
            {
                // 모든 설명 이미지를 다 봤으면 패널을 비활성화
                ClosePanel();
            }
        }
    }

    void ClosePanel()
    {
        // 현재 게임 오브젝트 (패널) 비활성화
        gameObject.SetActive(false);
    }
}