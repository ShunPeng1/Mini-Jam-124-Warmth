using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScoreOperator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI operatorText;

    [SerializeField] private OperatorType operatorType;

    [SerializeField] private int value;
    
    void Start()
    {
        operatorText.text = OperationDecode() + value.ToString();
    }

    

    private string OperationDecode()
    {
        switch (operatorType)
        {
            case OperatorType.Assignment:
                return "=";
                
            case OperatorType.Addition:
                return "+";
                
            case OperatorType.Subtraction:
                return "-";
                
            case OperatorType.Multiplication:
                return "X";
                
            case OperatorType.Division:
                return "/";
                
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public void SendCalculation()
    {
        ScoreManager.Instance.OnOperationCalculation(operatorType, value);    
        Destroy(gameObject);
    }
}
