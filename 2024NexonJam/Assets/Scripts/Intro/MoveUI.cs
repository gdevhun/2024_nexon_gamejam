using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveUI : MonoBehaviour
{
    public enum UIType
    {
        Monkey,
        Turtle,
        Coin
    }

    public UIType uiType;
    private RectTransform uiElement;

    private Dictionary<UIType, Vector3[]> uiPositions = new Dictionary<UIType, Vector3[]>
    {
        {   //Vector3(1625,-578,0)   ->
            //Vector3(361,131,0)
            UIType.Monkey, new Vector3[]
            {
                new Vector3(1625f,-578f,0f),
                new Vector3(361f,131f,0f)
            }
        },
        {
            //Vector3(1264,-884,0)
            //Vector3(63,-261,0)
            UIType.Turtle, new Vector3[]
            {
                new Vector3(1264f,-884f,0f),
                new Vector3(63f,-261f,0f)
            }
        },
        {
            //Vector3(1410,-820,0)
            //Vector3(0,0,0)
            UIType.Coin, new Vector3[]
            {
                new Vector3(1410f,-820f,0f),
                new Vector3(0f,0f,0f)
            }
        }
    };

    private void Awake()
    {
        uiElement = GetComponent<RectTransform>();
    }

    private void Start()
    {
        MoveToPositions();
    }

    private void MoveToPositions()
    {
        if (uiPositions.TryGetValue(uiType, out Vector3[] positions))
        {
            Sequence sequence = DOTween.Sequence();

            foreach (Vector3 position in positions)
            {
                sequence.Append(uiElement.DOAnchorPos(position, 0.5f)); // 이동 시간
            }

            sequence.Play();
        }
        else
        {
            Debug.LogError("UI 타입에 대한 위치가 정의되지 않았습니다.");
        }
    }
}

