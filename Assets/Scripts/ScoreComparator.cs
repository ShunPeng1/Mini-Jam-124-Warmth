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
    [SerializeField] private bool isOneTimeActiveNorAlwaysActive = true;
    [SerializeField] private bool isEnable = true;
    [SerializeField] private bool isPassable = false;

    [SerializeField] private Sprite litSprite;
    [SerializeField] private Sprite unlitSprite;
    
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = unlitSprite;
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


    public void SendComparison(out bool isPassableOut)
    {
        isPassableOut = isPassable;
        Debug.Log("Hit");
        if (!isEnable) return;
        if (!ScoreManager.Instance.OnComparatorCalculation(comparatorType, value) || onTrueEvent == null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = unlitSprite;
            return;
        }
        Debug.Log("Light");
        if (isOneTimeActiveNorAlwaysActive) isEnable = false;

        gameObject.GetComponent<SpriteRenderer>().sprite = litSprite;
        onTrueEvent.Invoke();
        
        
    }
}
