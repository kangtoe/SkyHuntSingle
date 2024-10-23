using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    GameObject heartPrefab;

    [Header("for debug")] [SerializeField]
    List<Image> heartImages = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        //ClearHearts();
        //CreatHearts(3);
        //SetHeartsFill(2.5f);
    }

    public void InitUI(int createHeart, float fillHeart)
    {
        Debug.Log("InitUI" + createHeart + "  " + fillHeart);

        if(fillHeart > createHeart)
        {
            Debug.LogWarning("fillHeart cant exceed createHeart!");
            fillHeart = createHeart;
        }

        ClearHearts();
        CreatHearts(createHeart);
        SetHeartsFill(fillHeart);
    }

    void ClearHearts()
    {
        heartImages.Clear();
        for (int i = transform.childCount - 1; i >= 0 ; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }        
    }

    void CreatHearts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(heartPrefab, transform);
            Image img = go.transform.GetChild(0).GetComponent<Image>();
            heartImages.Add(img);
        }
    }

    void SetHeartsFill(float fillAmount)
    {
        float leftFill = fillAmount;
        int idx = 0;

        while (true)
        {
            if (leftFill >= 1) heartImages[idx].fillAmount = 1f;
            else if (leftFill >= 0) heartImages[idx].fillAmount = leftFill;
            
            leftFill--;
            idx++;

            if (leftFill <= 0) break;
        }
    }

    

}
