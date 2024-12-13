using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject ball; // �y������

    public void SetBallPosition(float angle)
    {
        float radius = 100f; // �ھ���L����ڤؤo�վ�
        float radians = angle * Mathf.Deg2Rad;

        Vector3 newPosition = new Vector3(
            radius * Mathf.Cos(radians),
            radius * Mathf.Sin(radians),
            0
        );

        if (ball != null)
        {
            ball.transform.localPosition = newPosition;
            ball.SetActive(true); // �T�O�y�Q�ҥ�
        }
        else
        {
            Debug.LogError("Ball reference is missing!");
        }
    }
}
