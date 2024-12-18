using UnityEngine;
using System.Collections.Generic;

public class RouletteWheelController : MonoBehaviour
{
    public GameObject ball; // 引用 Ball 的物件
    public float minRotationSpeed = 300f;
    public float maxRotationSpeed = 700f;
    public float deceleration = 10f;
    private float currentSpeed;
    private bool isSpinning = false;

    public int totalSegments = 37; // 區域數量

    private float rotationTime; // 旋轉的時間
    private float timeSpentSpinning = 0f; // 用來記錄已經過的時間

    // 定義每個數字對應的角度
    private Dictionary<int, float> numberAngles = new Dictionary<int, float>
    {
        { 0, 0f }, { 32, 9.73f }, { 15, 19.46f }, { 19, 29.19f }, { 4, 38.92f }, { 21, 48.65f },
        { 2, 58.38f }, { 25, 68.11f }, { 17, 77.84f }, { 34, 87.57f }, { 6, 97.30f }, { 27, 107.03f },
        { 13, 116.76f }, { 36, 126.49f }, { 11, 136.22f }, { 30, 145.95f }, { 8, 155.68f },
        { 23, 165.41f }, { 10, 175.14f }, { 5, 184.87f }, { 24, 194.60f }, { 16, 204.33f },
        { 33, 214.06f }, { 1, 223.79f }, { 20, 233.52f }, { 14, 243.25f }, { 31, 252.98f },
        { 9, 262.71f }, { 22, 272.44f }, { 18, 282.17f }, { 29, 291.90f }, { 7, 301.63f },
        { 28, 311.36f }, { 12, 321.09f }, { 35, 330.82f }, { 3, 340.55f }, { 26, 350.28f }
    };

    void Update()
    {
        if (isSpinning)
        {
            timeSpentSpinning += Time.deltaTime;

            // 持續旋轉
            transform.Rotate(0, 0, currentSpeed * Time.deltaTime);

            // 減速
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);

            // 當旋轉時間超過設定的時間，停止旋轉
            if (timeSpentSpinning >= rotationTime)
            {
                isSpinning = false;
                currentSpeed = 0;

                // 當轉盤停止後，計算球的位置
                float finalAngle = transform.eulerAngles.z % 360;
                DetermineResult(finalAngle);
            }
        }
    }

    public void SpinWheel()
    {
        if (!isSpinning)
        {
            // 隨機選擇旋轉時間，5到7秒之間
            rotationTime = UnityEngine.Random.Range(5f, 7f);

            // 計算需要的轉速來達到目標旋轉時間
            float totalRotation = 360f * rotationTime / 2f; // 旋轉總角度
            currentSpeed = totalRotation / rotationTime; // 根據時間計算速度

            timeSpentSpinning = 0f; // 重置時間

            isSpinning = true;
        }
    }

    private void DetermineResult(float finalAngle)
    {
        // 計算每個區域的角度範圍
        float segmentSize = 360f / totalSegments;

        // 計算最終結果的區域索引
        int resultIndex = Mathf.FloorToInt(finalAngle / segmentSize);

        Debug.Log("結果索引: " + resultIndex);

        // 設置球的位置
        if (ball != null)
        {
            // 獲取結果索引對應的角度
            float ballAngle;
            if (numberAngles.TryGetValue(resultIndex, out ballAngle))
            {
                SetBallPosition(ballAngle);
            }
            else
            {
                Debug.LogError("無法找到對應的角度!");
            }
        }
    }

    private void SetBallPosition(float angle)
    {
        float radius = 2f; // 根據轉盤大小調整
        float radians = angle * Mathf.Deg2Rad;
        Vector3 newPosition = new Vector3(
            radius * Mathf.Cos(radians),
            radius * Mathf.Sin(radians),
            0
        );

        // 設置球的位置相對於轉盤的中心
        ball.transform.localPosition = newPosition;
        ball.SetActive(true); // 確保球被啟用
    }
}
