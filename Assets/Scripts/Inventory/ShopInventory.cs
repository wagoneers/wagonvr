using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    List<ObjectRespawnZone> _respawnZones;
    public int StartingCash;
    public decimal cash;
    AudioSource BuyAudio;
    Dictionary<string, decimal> Prices;
    TextMeshPro CashCount;

    private void Start()
    {
        BuyAudio = GameObject.Find("Audio_CashRegister").GetComponent<AudioSource>();
        CashCount = GameObject.Find("UICashCount").GetComponent<TextMeshPro>();
        _respawnZones = GameObject.FindGameObjectsWithTag("RespawnZone").Select(go => go.GetComponent<ObjectRespawnZone>()).ToList();
        cash = Convert.ToDecimal(StartingCash);
        Prices = new Dictionary<string, decimal>
        {
            { "Food", 2.50m },
            { "Ammo", 3.25m },
            { "Clothing", 15.00m },
            { "SparePart", 20.00m },
            { "Oxen", 25.00m }
        };
        InitializePricesForUI();
        UpdateUI();
    }

    public void BuyItem(Collider other)
    {
        BuyAudio.Play();
        decimal price = Prices[other.tag];
        cash = cash - price;

        UpdateUI();
    }

    private void InitializePricesForUI()
    {
        foreach(ObjectRespawnZone respawnZone in _respawnZones)
        {
            TextMeshPro priceLabel = respawnZone.GetComponentInChildren<TextMeshPro>();
            priceLabel.text = $"{respawnZone.prefab.tag} - ${Prices[respawnZone.prefab.tag]}";
        }
    }

    private void UpdateUI()
    {
        CashCount.text = $"${cash}";
        //CashCount.text = cash.ToString();
    }
}
