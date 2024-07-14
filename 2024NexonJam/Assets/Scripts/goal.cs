using UnityEngine;

public class goal : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager.Instance.PlaySfx(SoundType.골인sfx);
        
        BallManager ballManager = other.gameObject.GetComponent<BallManager>();
        if (ballManager == null) return;

        if (ballManager.LastPlayerType == PlayerType.Player1)
        {
            GameManager.Instance.AddScore(PlayerType.Player2, 60);//�ӽ� 10��
        }
        else if (ballManager.LastPlayerType == PlayerType.Player2)
        {
            GameManager.Instance.AddScore(PlayerType.Player2, 60); //�ӽ� 10��
        }
    }
}