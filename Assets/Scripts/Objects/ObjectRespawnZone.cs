using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using VRTK;

public class ObjectRespawnZone : MonoBehaviour
{
    public GameObject prefab;
    public GameObject yokePrefab;
    public ShopInventory _shopInventory;

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Item>() != null)
        {
            StartCoroutine(Wait(other));
        }
    }

    IEnumerator Wait(Collider other)
    {
        _shopInventory.BuyItem(other);
        if(other.tag == "Oxen")
        {
            SpawnOx(other);
            Destroy(other.gameObject);
        }
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        GameObject clone = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        gameObject.SetActive(true);
    }

    /*All this ox-specific logic needs to be moved to another script*/
    private void SpawnOx(Collider other)
    {
        var OxenCountText = GameObject.Find("OxenCount").GetComponent<TextMeshPro>();
        int OxenCount = 0;
        var quantity = other.GetComponent<Item>().Quantity;
        OxenCount += quantity;
        OxenCountText.text = OxenCount.ToString();
        var yokeSpawnPoint = GameObject.Find("YokeSpawnPoint");
        var yokePositions = GameObject.FindGameObjectsWithTag("YokePosition").Select(go => go.GetComponent<ItemStoragePosition>());
        foreach (ItemStoragePosition yokePosition in yokePositions)
        {
            if (yokePosition.Occupied == false)
            {
                yokePosition.Occupied = true;
                NavMeshAgent yokeAgent = Instantiate(yokePrefab, yokeSpawnPoint.transform.position, Quaternion.identity).GetComponent<NavMeshAgent>();
                AudioSource[] sounds = yokeAgent.GetComponentsInChildren<AudioSource>();
                foreach (AudioSource sound in sounds)
                {
                    if (sound.name == "Moo01")
                    {
                        sound.Play();
                    }
                    if (sound.name == "Bell01")
                    {
                        sound.Play();
                    }
                }

                yokeAgent.destination = yokePosition.transform.position;
                break;
            }
        }
    }
}
        
