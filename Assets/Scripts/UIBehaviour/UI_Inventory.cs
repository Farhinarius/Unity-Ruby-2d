using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    public static UI_Inventory instance { get; private set; }
    private GameObject[] ItemSlots = new GameObject[5];

    public Sprite defaultItemSlotSprite;
    GameObject previousSlot;


    private bool previousSelectionExists = false;

    void Awake()
    {
        instance = this;
    }

    private void Start() 
    {
        // Get all Slots GameObjects
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i] = gameObject.transform.GetChild(i).gameObject;
        }


        previousSlot = new GameObject();
    }

    public void ChangeSlotSprite(Sprite spriteToChange, int index)
    {
        Image itemSlotImage = ItemSlots[index].GetComponent<Image>();
        itemSlotImage.sprite = spriteToChange;
    }

    public void defineSlot(int index)
    {
        Image ItemImage = ItemSlots[index].GetComponent<Image>();

        if (ItemImage.sprite != defaultItemSlotSprite)  // if item slot is not empty (if contain any item)
        {
            if (!previousSelectionExists)   // if item is equipped for the first time
            {
                previousSlot = ItemSlots[index];
                previousSelectionExists = true;

                ScaleInTime(ItemSlots[index]);
                Debug.Log("Slot Upscaled!");

            }   // if item is equiped any time after first
            else if (previousSelectionExists && previousSlot != ItemSlots[index])
            {
                DownScaleInTime(previousSlot);
                ScaleInTime(ItemSlots[index]);
                previousSlot = ItemSlots[index];
                Debug.Log("Change Slot!");
            }
        }
    }

    private void ScaleInTime(GameObject itemSlot)
    {
        Vector2 slotScale = itemSlot.GetComponent<RectTransform>().localScale;
        slotScale = new Vector2(1.1f, 1.1f);
        itemSlot.GetComponent<RectTransform>().localScale = slotScale;
        
/*         for (float scaleX = slotScale.x, scaleY = slotScale.y; 
            scaleX < MaximumScale; 
            scaleX += 0.01f, scaleY += 0.01f)
        {

        } */


        // itemSlot.GetComponent<RectTransform>().localScale = slotScale;
    }

    private void DownScaleInTime(GameObject itemSlot)
    {
        Vector2 slotScale = itemSlot.GetComponent<RectTransform>().localScale;
        slotScale = new Vector2(1.0f, 1.0f);
        itemSlot.GetComponent<RectTransform>().localScale = slotScale;

/*         for (float scaleX = slotScale.x, scaleY = slotScale.y;
            scaleX > MinimumScale;
            scaleX -= 0.01f, scaleY = 0.01f)
        {

        } */


    }

/*     private void Update() {
        
    } */

}