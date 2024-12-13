using UnityEngine;

public class RouletteWheelController : MonoBehaviour
{
    public GameObject ball; // �ޥ� Ball ������
    public float minRotationSpeed = 300f;
    public float maxRotationSpeed = 700f;
    public float deceleration = 10f;
    private float currentSpeed;
    private bool isSpinning = false;

    public int totalSegments = 37; // �ϰ�ƶq

    private float rotationTime; // ���઺�ɶ�
    private float timeSpentSpinning = 0f; // �ΨӰO���w�g�L���ɶ�

    void Update()
    {
        if (isSpinning)
        {
            timeSpentSpinning += Time.deltaTime;

            // �������
            transform.Rotate(0, 0, currentSpeed * Time.deltaTime);

            // ��t
            currentSpeed = Mathf.Lerp(currentSpeed, 0, deceleration * Time.deltaTime);

            // �����ɶ��W�L�]�w���ɶ��A�������
            if (timeSpentSpinning >= rotationTime)
            {
                isSpinning = false;
                currentSpeed = 0;

                // ����L�����A�p��y����m
                float finalAngle = transform.eulerAngles.z % 360;
                DetermineResult(finalAngle);
            }
        }
    }

    public void SpinWheel()
    {
        if (!isSpinning)
        {
            // �H����ܱ���ɶ��A5��7����
            rotationTime = UnityEngine.Random.Range(5f, 7f);

            // �p��ݭn����t�ӹF��ؼб���ɶ�
            float totalRotation = 360f * rotationTime / 2f; // �����`����
            currentSpeed = totalRotation / rotationTime; // �ھڮɶ��p��t��

            timeSpentSpinning = 0f; // ���m�ɶ�

            isSpinning = true;
        }
    }

    private void DetermineResult(float finalAngle)
    {
        // �p��C�Ӱϰ쪺���׽d��
        float segmentSize = 360f / totalSegments;

        // �p��̲׵��G���ϰ����
        int resultIndex = Mathf.FloorToInt(finalAngle / segmentSize);

        Debug.Log("���G����: " + resultIndex);

        // �]�m�y����m
        if (ball != null)
        {
            float ballAngle = (resultIndex * segmentSize) + (segmentSize / 2); // �y����m�O�ϰ쪺����
            SetBallPosition(ballAngle);
        }
    }

    private void SetBallPosition(float angle)
    {
        float radius = 2f; // �ھ���L�j�p�վ�
        float radians = angle * Mathf.Deg2Rad;
        Vector3 newPosition = new Vector3(
            radius * Mathf.Cos(radians),
            radius * Mathf.Sin(radians),
            0
        );

        // �]�m�y����m�۹����L������
        ball.transform.localPosition = newPosition;
        ball.SetActive(true); // �T�O�y�Q�ҥ�
    }
}