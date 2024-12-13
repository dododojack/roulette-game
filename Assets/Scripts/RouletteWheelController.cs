using UnityEngine;

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
            float ballAngle = (resultIndex * segmentSize) + (segmentSize / 2); // 球的位置是區域的中心
            SetBallPosition(ballAngle);
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