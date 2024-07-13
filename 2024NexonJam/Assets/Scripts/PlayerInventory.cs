using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public PlayerType playerType;
    public List<Image> skillSlots;

    private void Start()
    {
        InitSprite();
    }

    private void InitSprite()
    {
        foreach (var slot in skillSlots)
        {
            slot.sprite = null; // 초기에는 아무 이미지도 없음
        }
    }

    private void Update()
    {
        if (playerType == PlayerType.Player1 && Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill(0);
        }
        else if (playerType == PlayerType.Player2 && Input.GetKeyDown(KeyCode.Backslash))
        {
            UseSkill(1);
        }
    }

    private void UseSkill(int playerIndex)
    {
        if (skillSlots[2].sprite == null) return;

        // 이미 스킬이 활성화된 경우 반환
        if (SkillManager.Instance.IsDoubleScoreActive(playerIndex) ||
            SkillManager.Instance.IsInvisibleTurtleActive(playerIndex) ||
            SkillManager.Instance.IsThrowStarFishActive(playerIndex))
        {
            return;
        }

        string spriteName = skillSlots[2].sprite.name;
        if (spriteName == "Skill_doublescore")
        {
            SkillManager.Instance.ActivateDoubleScore(playerIndex);
        }
        else if (spriteName == "Skill_ghost")
        {
            SkillManager.Instance.ActivateInvisibleTurtle(playerIndex);
        }
        else if (spriteName == "Skill_starfish")
        {
            SkillManager.Instance.ActivateThrowStarFish(playerIndex);
        }

        // 가장 아래의 스킬을 사용한 것으로 설정
        skillSlots[2].sprite = null;

        // 스킬 슬롯을 하나씩 아래로 당김
        for (int i = skillSlots.Count - 1; i > 0; i--)
        {
            skillSlots[i].sprite = skillSlots[i - 1].sprite;
        }

        skillSlots[0].sprite = null;
    }
    

    // 스킬을 슬롯에 추가하는 메서드
    public void AddSkill(Sprite skillSprite)
    {
        // 가장 아래의 비어있는 슬롯에 추가
        for (int i = skillSlots.Count - 1; i >= 0; i--)
        {
            if (skillSlots[i].sprite == null)
            {
                skillSlots[i].sprite = skillSprite;
                break;
            }
        }
    }

    public bool IsFullInventory()
    {
        int cnt = 0;
        for (int i = 0; i < skillSlots.Count; i++)
        {
            if (skillSlots[i].sprite != null)
            {
                cnt++;
            }
        }

        if (cnt == 3) return true;
        else return false;
    }
    /*public bool IsFullInventory()
    {
        return skillSlots.All(slot => slot.sprite != null);
    }*/

}


