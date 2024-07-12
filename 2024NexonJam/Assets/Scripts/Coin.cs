using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BallManager ballManager = other.gameObject.GetComponent<BallManager>();
        if (ballManager == null) return;

        PlayerInventory playerInventory = null;

        if (ballManager.LastPlayerType == PlayerType.Player1)
        {
            GameManager.Instance.AddScore(PlayerType.Player1,10);//임시 10점
        }
        else if (ballManager.LastPlayerType == PlayerType.Player2)
        {
            GameManager.Instance.AddScore(PlayerType.Player2, 10); //임시 10점
        }
        gameObject.SetActive(false);
    }
}
