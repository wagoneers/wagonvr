using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WagonInventoryZone : MonoBehaviour
{
    private int OxenCount = 0;
    TextMeshPro OxenCountText;

    private int FoodCount = 0;
    TextMeshPro FoodCountText;

    private int AmmoCount = 0;
    TextMeshPro AmmoCountText;

    private int ClothingCount = 0;
    TextMeshPro ClothingCountText;

    private int SparePartCount = 0;
    TextMeshPro SparePartCountText;

    AudioSource ItemAddedSound;

    void Start()
    {
        ItemAddedSound = GameObject.Find("Audio_ItemAddedSound").GetComponent<AudioSource>();
        OxenCountText = GameObject.Find("OxenCount").GetComponent<TextMeshPro>();
        FoodCountText = GameObject.Find("FoodCount").GetComponent<TextMeshPro>();
        AmmoCountText = GameObject.Find("AmmoCount").GetComponent<TextMeshPro>();
        ClothingCountText = GameObject.Find("ClothingCount").GetComponent<TextMeshPro>();
        SparePartCountText = GameObject.Find("SparePartCount").GetComponent<TextMeshPro>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Item>() != null)
        {
            var quantity = other.GetComponent<Item>().Quantity;
            //if (other.tag == "Oxen")
            //{
            //    OxenCount += quantity;
            //    OxenCountText.text = OxenCount.ToString();
            //}
            if (other.tag == "Food")
            {
                var foodStoragePositions = GameObject.FindGameObjectsWithTag("FoodStorage").Select(go => go.GetComponent<ItemStoragePosition>());
                bool success = false;
                StoreItem(other, foodStoragePositions, ref success, Quaternion.identity);
                if (success)
                {
                    FoodCount += quantity;
                    FoodCountText.text = FoodCount.ToString();
                }
            }
            if (other.tag == "Ammo")
            {
                var ammoStoragePositions = GameObject.FindGameObjectsWithTag("AmmoStorage").Select(go => go.GetComponent<ItemStoragePosition>());
                bool success = false;
                StoreItem(other, ammoStoragePositions, ref success, Quaternion.identity);
                if (success)
                {
                    AmmoCount += quantity;
                    AmmoCountText.text = AmmoCount.ToString();
                }
            }
            if (other.tag == "Clothing")
            {
                var clothingStoragePositions = GameObject.FindGameObjectsWithTag("ClothingStorage").Select(go => go.GetComponent<ItemStoragePosition>());
                bool success = false;
                StoreItem(other, clothingStoragePositions, ref success, Quaternion.identity);
                if (success)
                {
                    ClothingCount += quantity;
                    ClothingCountText.text = ClothingCount.ToString();
                }            }
            if (other.tag == "SparePart")
            {
                var spareStoragePositions = GameObject.FindGameObjectsWithTag("SpareStorage").Select(go => go.GetComponent<ItemStoragePosition>());
                bool success = false;
                StoreItem(other, spareStoragePositions, ref success, Quaternion.Euler(0, 90, 90));
                if (success)
                {
                    SparePartCount += quantity;
                    SparePartCountText.text = SparePartCount.ToString();
                }
            }

            StartCoroutine(FadeOut(other));
        }
    }

    private void StoreItem(Collider item, IEnumerable<ItemStoragePosition> itemStoragePositions, ref bool success, Quaternion newRotation)
    {
        foreach(var itemStoragePosition in itemStoragePositions)
        {
            if (itemStoragePosition.Occupied == false)
            {
                itemStoragePosition.Occupied = true;
                item.transform.position = itemStoragePosition.transform.position;
                item.transform.rotation = newRotation;
                item.GetComponent<Rigidbody>().isKinematic = true;
                success = true;
                ItemAddedSound.Play();
                break;
            }
        }
    }

    IEnumerator FadeOut(Collider other)
    {
        yield return new WaitForSeconds(1f);
        Destroy(other.transform.parent);
    }
}
