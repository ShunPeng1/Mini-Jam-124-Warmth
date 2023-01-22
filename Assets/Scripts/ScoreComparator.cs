using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using static OperatorType;


public class ScoreComparator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI operatorText;
    [SerializeField] private ComparatorType comparatorType;
    
    
    [SerializeField] private int value;
    
    [SerializeField] private UnityEvent onTrueEvent;
    
    
    void Start()
    {
        
        if(comparatorType != ComparatorType.Null)
            operatorText.text = OperationDecode() + value.ToString();
    }

    

    private string OperationDecode()
    {
        switch (comparatorType)
        {
            case ComparatorType.LessThan:
                return "<";
                break;
            case ComparatorType.MoreThan:
                return ">";
                break;
            case ComparatorType.Equal:
                return "==";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public void SendCalculation()
    {
        if( ScoreManager.Instance.OnComparatorCalculation(comparatorType, value) && onTrueEvent != null)
            onTrueEvent.Invoke();    
       
    }
}
