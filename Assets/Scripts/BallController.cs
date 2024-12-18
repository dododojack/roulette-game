using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject ball; // 引用 Ball 的物件
    public float moveDuration = 1.0f; // 球移動的持續時間

    private Vector3 targetPosition; // 目標位置
    private bool isMoving = false; // 是否正在移動
    private float elapsedTime = 0f; // 已經過的時間

    void Update()
    {
        if (isMoving)
        {
            // 計算插值
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            // 更新球的位置
            ball.transform.localPosition = Vector3.Lerp(ball.transform.localPosition, targetPosition, t);

            // 檢查是否完成移動
            if (t >= 1.0f)
            {
                isMoving = false;
                ball.SetActive(true); // 確保球被啟用
            }
        }
    }

    public void SetBallPosition(float angle)
    {
        float radius = 2f; // 根據轉盤大小調整
        float radians = angle * Mathf.Deg2Rad;
        targetPosition = new Vector3(
            radius * Mathf.Cos(radians),
            radius * Mathf.Sin(radians),
            0
        );

        // 開始移動球
        isMoving = true;
        elapsedTime = 0f;
    }
}
