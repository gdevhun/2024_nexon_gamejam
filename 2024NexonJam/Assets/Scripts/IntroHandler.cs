using UnityEngine;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour
{
    public Sprite[] introImages; 
    public Image displayImage; 
    private int currentImageIndex = 0; 
    void Start()
    {
        if (introImages.Length > 0)
        {
            displayImage.sprite = introImages[currentImageIndex];
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentImageIndex++;

            if (currentImageIndex < introImages.Length)
            {
                displayImage.sprite = introImages[currentImageIndex];
            }
            else
            {
                ClosePanel();
            }
        }
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}