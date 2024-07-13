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

        // Z축을 고정하기 위해 Vector3를 사용하고, Z를 0으로 설정합니다.
        Vector3[] wayPoints2D = new Vector3[wayPoints.Length];
        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints2D[i] = new Vector3(wayPoints[i].x, wayPoints[i].y, 0f); // Z축을 0으로 고정
        }

        // 2D 경로를 따라 이동
        transform.DOPath(wayPoints2D, 5f, PathType.CatmullRom)
            .SetOptions(false) // false를 사용하여 Z축 고정
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo)
            .OnUpdate(FixRotation); // OnUpdate 콜백에서 회전 수정
    }

    // z축 회전을 고정하는 메서드
    private void FixRotation()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = 0f; // z축 회전을 0으로 고정
        transform.rotation = Quaternion.Euler(rotation);
    }
}