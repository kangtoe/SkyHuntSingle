using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillSparkUI : MonoBehaviour
{
    public Image fillImage;
    public Image uiPrefab;

    public int sparkCount = 1;
    public float fadeSpeed = 1;
    public float randomAngle = 20;
    public float rotateAngle = 0;
    public float popPower = 100;
    public float grav = 100;
    public float interval = 0.2f;

    private void Start()
    {
        DoSpark();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToFillPoint();     
    }

    void MoveToFillPoint()
    {
        // fillAmount�� ���� �� ��ġ ���
        float fillAmount = fillImage.fillAmount;
        RectTransform rect = fillImage.GetComponent<RectTransform>();

        // ä���� �̹����� ���� ũ��
        float imageWidth = rect.rect.width;

        // ä���� �� ��ġ (fillAmount�� ���Ͽ� ���� �� ��ġ ���ϱ�)
        float fillEndPosition = imageWidth * fillAmount;

        // �̵���ų �̹����� ���ο� ��ġ ����
        Vector3 newPosition = rect.position;
        newPosition.x += fillEndPosition;
        newPosition.x -= rect.rect.width / 2;

        // �̵��� �̹����� ��ġ ����
        transform.position = newPosition;
    }


    void DoSpark(int count = 1)
    {
        IEnumerator Cr()
        {
            while (true)
            {
                for (int i = 0; i < count; i++)
                {
                    Transform tf = Instantiate(uiPrefab, transform).transform;

                    float angle = Random.Range(-randomAngle, randomAngle) + rotateAngle;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    float power = popPower;
                    Vector3 vec = rotation * Vector3.up * power;

                    Rigidbody2D rbody = tf.GetComponent<Rigidbody2D>();
                    rbody.AddForce(vec, ForceMode2D.Impulse);
                    rbody.AddTorque(Random.Range(-90, 90));
                    rbody.gravityScale = grav;

                    Graphic gph = tf.GetComponent<Graphic>();
                    gph.color = fillImage.color;

                    FadeUI fade = tf.gameObject.AddComponent<FadeUI>();
                    fade.fadeSpeed = fadeSpeed;
                }

                yield return new WaitForSeconds(interval);
            }            
        }

        StartCoroutine(Cr());
    }
}

