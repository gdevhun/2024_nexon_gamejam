using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillManager : SingletonBehaviour<SkillManager>
{
    public enum SkillType
    {
        DoubleScore=0,
        InvisibleTurtle=1,
        ThrowStarfish=2,
    }

    public GameObject player1Turtle;
    public GameObject player2Turtle;

    public GameObject[] playerSwing;

    public GameObject starfish;


    private List<GameObject> swing;

    private const int PlayerCount = 2; 
    private const int SkillCount = 3; 
    private WaitForSeconds _5sec = new WaitForSeconds(5f);

    private bool[,] playerSkills = new bool[PlayerCount, SkillCount];

    private Swing attackedSwing;
    
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
            
            //�ش� �ź����� ������ �޾ƿ���
            Renderer turtleRD = targetTurtle.GetComponent<Renderer>();
            Collider2D turtleCD = targetTurtle.GetComponent<Collider2D>();
            
            //������ȭ
            Color color = turtleRD.material.color;
            color.a = 0.5f;
            turtleRD.material.color = color;
             
            //istriggered�� �ٲٱ�
            turtleCD.isTrigger = true;
        }

        //double score�� gamemanager���� ��ü������ �ذ�

        if (skillType == SkillType.ThrowStarfish)
        {
            GameObject targetSwing = playerIndex == 0 ? playerSwing[1] : playerSwing[0];

            int random = Random.Range(0, 2); // �ڽ� ������ �°� ���� �� ����

            Transform attackedChild = targetSwing.transform.GetChild(0); //�� �� �ϳ� attacked ����
            
            attackedSwing = attackedChild.GetComponent<Swing>();
            attackedSwing.enabled = false;

            starfish.transform.position = attackedChild.position;

            starfish.SetActive(true);
        }

        yield return _5sec;

        if (skillType == SkillType.InvisibleTurtle)
        {
            GameObject targetTurtle = playerIndex == 0 ? player2Turtle : player1Turtle;

            //�ش� �ź����� ������ �޾ƿ���
            Renderer turtleRD = targetTurtle.GetComponent<Renderer>();
            Collider2D turtleCD = targetTurtle.GetComponent<Collider2D>();

            //������ȭ
            Color color = turtleRD.material.color;
            color.a = 1f;
            turtleRD.material.color = color;

            //istriggered�� �ٲٱ�
            turtleCD.isTrigger = false;
        }

        if (skillType == SkillType.ThrowStarfish)
        {
            yield return new WaitForSeconds(2f);
            attackedSwing.enabled = true;
            starfish.SetActive(false);

        }

        SetPlayerSkillState(playerIndex, skillType, false);
    }

    //�̰ɷ� ��ų���
    public void ActivateDoubleScore(int playerIndex) => StartCoroutine(HandleSkill(playerIndex, SkillType.DoubleScore));
    public void ActivateInvisibleTurtle(int playerIndex) => StartCoroutine(HandleSkill(playerIndex, SkillType.InvisibleTurtle));
    public void ActivateThrowStarFish(int playerIndex) => StartCoroutine(HandleSkill(playerIndex, SkillType.ThrowStarfish));


    //�̰ɷ� ��ųüũ
    public bool IsDoubleScoreActive(int playerIndex) => playerSkills[playerIndex, (int)SkillType.DoubleScore];
    public bool IsInvisibleTurtleActive(int playerIndex) => playerSkills[playerIndex, (int)SkillType.InvisibleTurtle];
    public bool IsThrowStarFishActive(int playerIndex) => playerSkills[playerIndex, (int)SkillType.ThrowStarfish];
}


