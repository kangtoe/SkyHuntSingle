using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultyUiCtrl : MonoBehaviour
{
    [SerializeField]
    GameObject uiPrefab;

    [Header("for debug")] [SerializeField]
    List<Image> fillImages = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        //ClearHearts();
        //CreatHearts(3);
        //SetHeartsFill(2.5f);
    }

    public void InitUI(int createChild, float fillChild)
    {
        //Debug.Log("InitUI" + createChild + "  " + fillChild);

        if(fillChild > createChild)
        {
            Debug.LogWarning("fillHeart cant exceed createHeart!");
            fillChild = createChild;
        }

        ClearUIs();
        CreatUIs(createChild);
        SetUIsFill(fillChild);
    }

    void ClearUIs()
    {
        fillImages.Clear();
        for (int i = transform.childCount - 1; i >= 0 ; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }        
    }

    void CreatUIs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(uiPrefab, transform);
            Image img = go.transform.GetChild(0).GetComponent<Image>();
            fillImages.Add(img);
        }
    }

    void SetUIsFill(float fillAmount)
    {
        float leftFill = fillAmount;        
        for (int idx = 0; idx < fillImages.Count; idx++)
        {
            if (leftFill >= 1)
            {
                fillImages[idx].fillAmount = 1f;
                leftFill--;
            }
            else if (leftFill >= 0)
            {
                fillImages[idx].fillAmount = leftFill;
            }
            else
            {
                fillImages[idx].fillAmount = 0;
            }            
        }
    }

    

}
