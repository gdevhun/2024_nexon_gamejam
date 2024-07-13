using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillManager : SingletonBehaviour<SkillManager>
{
    private enum SkillType
    {
        DoubleScore=0,
        InvisibleTurtle=1,
        ThrowStarfish=2,
    }

    public GameObject player1Turtle;
    public GameObject player2Turtle;
    
    private const int PlayerCount = 2; 
    private const int SkillCount = 3; 
    private WaitForSeconds _5sec = new WaitForSeconds(5f);

    private bool[,] playerSkills = new bool[PlayerCount, SkillCount];

    private void SetPlayerSkillState(int playerIndex, SkillType skillType, bool state)
    {
        playerSkills[playerIndex, (int)skillType] = state;
    }

    private IEnumerator HandleSkill(int playerIndex, SkillType skillType)
    {
        SetPlayerSkillState(playerIndex, skillType, true);

        if (skillType == SkillType.InvisibleTurtle)
        {
            GameObject targetTurtle = playerIndex == 0 ? player2Turtle : player1Turtle;
            targetTurtle.SetActive(false);
        }

        yield return _5sec;

        if (skillType == SkillType.InvisibleTurtle)
        {
            GameObject targetTurtle = playerIndex == 0 ? player2Turtle : player1Turtle;
            targetTurtle.SetActive(true);
        }

        SetPlayerSkillState(playerIndex, skillType, false);
    }

    public void ActivateDoubleScore(int playerIndex) => StartCoroutine(HandleSkill(playerIndex, SkillType.DoubleScore));
    public void ActivateInvisibleTurtle(int playerIndex) => StartCoroutine(HandleSkill(playerIndex, SkillType.InvisibleTurtle));
    public void ActivateThrowStarFish(int playerIndex) => StartCoroutine(HandleSkill(playerIndex, SkillType.ThrowStarfish));


    public bool IsDoubleScoreActive(int playerIndex) => playerSkills[playerIndex, (int)SkillType.DoubleScore];
    public bool IsInvisibleTurtleActive(int playerIndex) => playerSkills[playerIndex, (int)SkillType.InvisibleTurtle];
    public bool IsThrowStarFishActive(int playerIndex) => playerSkills[playerIndex, (int)SkillType.ThrowStarfish];
}


