using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject ball; // 球的物件

    public void SetBallPosition(float angle)
    {
        float radius = 100f; // 根據轉盤的實際尺寸調整
        float radians = angle * Mathf.Deg2Rad;

        Vector3 newPosition = new Vector3(
            radius * Mathf.Cos(radians),
            radius * Mathf.Sin(radians),
            0
        );

        if (ball != null)
        {
            ball.transform.localPosition = newPosition;
            ball.SetActive(true); // 確保球被啟用
        }
        else
        {
            Debug.LogError("Ball reference is missing!");
        }
    }
}
