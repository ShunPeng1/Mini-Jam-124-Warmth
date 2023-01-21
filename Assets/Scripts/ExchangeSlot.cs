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
        if (ScoreManager.Instance.OnChoosingExchangeSlot(slotID))
        {
            OnMouseEnter();
        }
        else
        {
            Debug.Log("Not affordable");
        }
    }
    
    private void OnMouseEnter()
    {
        ScoreManager.Instance.OnEnterExchangeSlot(slotID);
    }

    private void OnMouseExit()
    {
        ScoreManager.Instance.OnExitExchangeSlot();
    }
}
