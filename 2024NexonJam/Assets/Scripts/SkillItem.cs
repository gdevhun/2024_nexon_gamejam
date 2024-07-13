using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public enum ItemType
    {
        ghost,
        doubleScore,
        startfish
    }

    public ItemType itemType;
    public Sprite itemSprite;
}

public class SkillItem : MonoBehaviour
{
    public Item item;
    public GameObject player1Inventory;
    public GameObject player2Inventory;

    private void Start()
    {
        player1Inventory = GameObject.FindGameObjectWithTag("Player1Inventory");
        player2Inventory = GameObject.FindGameObjectWithTag("Player2Inventory");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BallManager ballManager = other.gameObject.GetComponent<BallManager>();
        if (ballManager == null) return;

        PlayerInventory playerInventory = null;

        if (ballManager.LastPlayerType == PlayerType.Player1)
        {
            if (player1Inventory != null)
            {
                playerInventory = player1Inventory.GetComponent<PlayerInventory>();
                if (playerInventory != null && !playerInventory.IsFullInventory())
                {
                    playerInventory.AddSkill(item.itemSprite);
                }
                else
                {
                    Debug.Log("Player1's inventory is full.");
                }
            }
        }
        else if (ballManager.LastPlayerType == PlayerType.Player2)
        {
            if (player2Inventory != null)
            {
                playerInventory = player2Inventory.GetComponent<PlayerInventory>();
                if (playerInventory != null && !playerInventory.IsFullInventory())
                {
                    playerInventory.AddSkill(item.itemSprite);
                }
                else
                {
                    Debug.Log("Player2's inventory is full.");
                }
            }
        }
        gameObject.SetActive(false);
    }
}