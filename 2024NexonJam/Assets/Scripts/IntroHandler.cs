using UnityEngine;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour
{
    public Sprite[] introImages; // ���� �̹����� ������ �迭
    public Image displayImage; // �̹����� ������ UI Image ������Ʈ

    private int currentImageIndex = 0; // ���� �������� �̹����� �ε���

    void Start()
    {
        // ù ��° �̹����� �ʱ�ȭ
        if (introImages.Length > 0)
        {
            displayImage.sprite = introImages[currentImageIndex];
        }
    }

    void Update()
    {
        // �ƹ� Ű�� ������ �� ���� �̹����� �Ѿ
        if (Input.anyKeyDown)
        {
            currentImageIndex++;

            if (currentImageIndex < introImages.Length)
            {
                displayImage.sprite = introImages[currentImageIndex];
            }
            else
            {
                // ��� ���� �̹����� �� ������ �г��� ��Ȱ��ȭ
                ClosePanel();
            }
        }
    }

    void ClosePanel()
    {
        // ���� ���� ������Ʈ (�г�) ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}