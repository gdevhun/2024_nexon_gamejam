using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public Ease ease;

    public Transform wayPoint1;
    public Transform wayPoint2;

    private Vector3[] wayPoints;

    private void Start()
    {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);

        wayPoints = new Vector3[2];
        wayPoints[0] = wayPoint1.position;
        wayPoints[1] = wayPoint2.position;

        // Z���� �����ϱ� ���� Vector3�� ����ϰ�, Z�� 0���� �����մϴ�.
        Vector3[] wayPoints2D = new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints2D[i] = new Vector3(wayPoints[i].x, wayPoints[i].y, 0f); // Z���� 0���� ����
        }

        // 2D ��θ� ���� �̵�
        transform.DOPath(wayPoints2D, 5f, PathType.CatmullRom)
            .SetOptions(false) // false�� ����Ͽ� Z�� ����
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo)
            .OnUpdate(FixRotation); // OnUpdate �ݹ鿡�� ȸ�� ����
    }

    // z�� ȸ���� �����ϴ� �޼���
    private void FixRotation()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = 0f; // z�� ȸ���� 0���� ����
        transform.rotation = Quaternion.Euler(rotation);
    }
}