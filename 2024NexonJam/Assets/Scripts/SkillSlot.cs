using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
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
        if (playerType == PlayerType.Player1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                UseSkill();
            }
        }
        else if (playerType == PlayerType.Player2)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                UseSkill();
            }
        }
    }

    private void UseSkill()
    {
        if (skillSlots[2].sprite != null)
        {
            
            // 가장 아래의 스킬을 사용한 것으로 설정
            skillSlots[2].sprite = null;

            // 스킬 슬롯을 하나씩 아래로 당김
            for (int i = skillSlots.Count - 1; i > 0; i--)
            {
                skillSlots[i].sprite = skillSlots[i - 1].sprite;
            }
            skillSlots[0].sprite = null;
        }
    }

    // 스킬을 슬롯에 추가하는 메서드
    public void AddSkill(Sprite skillSprite)
    {
        for (int i = 0; i < skillSlots.Count; i++)
        {
            if (skillSlots[i].sprite == null)
            {
                skillSlots[i].sprite = skillSprite;
                break;
            }
        }
    }
}


