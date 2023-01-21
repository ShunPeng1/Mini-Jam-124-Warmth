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
                break;
            case OperatorType.Addition:
                return "+";
                break;
            case OperatorType.Subtraction:
                return "-";
                break;
            case OperatorType.Multiplication:
                return "*";
                break;
            case OperatorType.Division:
                return "/";
                break;
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
