using System.Collections;
using UnityEngine;

public class GetSmallAndReturn : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public bool isWhaleType;
    
    private Vector3 originalScale;
    private bool isTriggered = false;

    private void Start()
    {
        if (isWhaleType)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("ball"))
        {
            SoundManager.Instance.PlaySfx(SoundType.고래와튜브충돌sfx);
            isTriggered = true;
            if (isWhaleType)
            {
                StartCoroutine(WhaleAnimEvent());
            }
            StartCoroutine(GetSmallAndBack());
        }
    }

    private IEnumerator WhaleAnimEvent()
    {
        // 자식 오브젝트의 SpriteRenderer를 활성화
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer != _spriteRenderer)
            {
                spriteRenderer.enabled = true;
            }
        }
         
        yield return new WaitForSeconds(1.15f);
        
        // 자식 오브젝트의 SpriteRenderer를 비활성화
        foreach (var spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer != _spriteRenderer)
            {
                spriteRenderer.enabled = false;
            }
        }
    }

    private IEnumerator GetSmallAndBack()
    {
        while (isTriggered)
        {
            isTriggered = false;
            transform.localScale = new Vector3(originalScale.x * 0.9f, originalScale.y * 0.9f, originalScale.z);
            yield return new WaitForSeconds(0.1f);
            transform.localScale = originalScale;
            yield return null;
        }
    }
}