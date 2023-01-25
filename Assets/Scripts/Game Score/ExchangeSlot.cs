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
            //Debug.Log("Not affordable");
            OnMouseEnter();
        }
    }
    
    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");
        ScoreManager.Instance.OnEnterExchangeSlot(slotID);
    }

    private void OnMouseExit()
    {
        //Debug.Log("Mouse Exit");
        ScoreManager.Instance.OnExitExchangeSlot();
    }
}
