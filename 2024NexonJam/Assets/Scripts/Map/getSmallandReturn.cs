using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getSmallandReturn : MonoBehaviour
{
    Vector3 originalScale;
    private bool isTriggered = false;
    private void Start()
    {
        this.originalScale = transform.localScale; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered && collision.gameObject.CompareTag("ball"))
        {
            isTriggered = true;
            StartCoroutine(getSmallandBack());
        }
    }

    IEnumerator getSmallandBack()
    {
        while (isTriggered)
        {
            isTriggered = false;
            this.transform.localScale = new Vector3(originalScale.x*0.9f, originalScale.y*0.9f, originalScale.z);
            yield return new WaitForSeconds(0.1f);
            this.transform.localScale = originalScale;

            yield return null;
        }
    }
}
