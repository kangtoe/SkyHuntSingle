using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageMaker : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public bool createImage;
    public Color StartColor = Color.white;
    public float fadeSpeed = 1f;          

    GameObject afterImagePrefab;

    void Start()
    {
        afterImagePrefab = new GameObject("afterImageOrigin");
        AfterImage afterImage = afterImagePrefab.AddComponent<AfterImage>();
        afterImage.fadeSpeed = fadeSpeed;
        SpriteRenderer sprite = afterImagePrefab.AddComponent<SpriteRenderer>();
        sprite.sprite = spriteRenderer.sprite;
        sprite.color = StartColor;
        afterImagePrefab.SetActive(false);

        StartCoroutine(AfterImageSpawn());
    }

    IEnumerator AfterImageSpawn()
    {
        while (true)
        {
            if (createImage) {
                GameObject currentGhost = Instantiate(afterImagePrefab, transform.position, transform.rotation);
                currentGhost.transform.localScale = transform.localScale;
                currentGhost.SetActive(true);

                Sprite currentSprite = spriteRenderer.sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                Destroy(currentGhost, 1f);
            }            

            yield return new WaitForSeconds(0.04f);
        }

        
    }
}
