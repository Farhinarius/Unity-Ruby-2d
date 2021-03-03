using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    Image whiteRect;
    public Sprite sprite;
    Sprite buferSprite;

    // Start is called before the first frame update
    void Start()
    {
        whiteRect = gameObject.GetComponent<Image>();
        buferSprite = whiteRect.sprite;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            whiteRect.sprite = sprite;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            whiteRect.sprite = buferSprite;
        }
    }
}
