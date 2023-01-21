using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExchangeSlot : MonoBehaviour
{

    [SerializeField] private int slotID;

    
    private void OnMouseDown()
    {
        ScoreManager.Instance.OnChoosingExchangeSlot(slotID);
    }
    
    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
        
    }
}
