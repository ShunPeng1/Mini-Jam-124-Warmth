using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityUtilities;
using UnityEngine.UI;


public enum DigitalBarType
{
    Horizontal,
    Vertical
}

public enum OperatorType
{
    Assignment,
    Addition,
    Subtraction,
    Multiplication,
    Division    
}

public enum ComparatorType
{
    Null,
    LessThan,
    MoreThan,
    Equal,
}



public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
{
    
    [Header("Score and value")]
    [SerializeField] private int currentScore;
    [SerializeField] private int exchangeSlot1Score, exchangeSlot2Score;
    [SerializeField] private int exchangeTurnLeft = 0;
    
    
    [Header("Text GameObject")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI exchangeSlot1Text;
    [SerializeField] private TextMeshProUGUI exchangeSlot2Text;
    
    [SerializeField] private TextMeshProUGUI numberOfHorizontalBarText;
    [SerializeField] private TextMeshProUGUI numberOfVerticalBarText;

    [SerializeField] private TextMeshProUGUI exchangeTurnLeftText;

    

    [Header("Bars")] 
    [SerializeField] private List<DigitalBarItem> listBarItem;
    private Dictionary<DigitalBarType, DigitalBarItem> _dictionaryBarItems;


    [Serializable]
    class DigitalBarItem
    {
        public DigitalBarType barType;
        public int numberOfItem;
        public GameObject prefab;
        
    }

    protected void Start()
    {
        
        _dictionaryBarItems = new Dictionary<DigitalBarType, DigitalBarItem>();
        foreach (var digitalBarItem in listBarItem)
        {
            _dictionaryBarItems.Add(digitalBarItem.barType, digitalBarItem);
            
        }

        NumberNode node = numberGraph[currentScore];
        currentScore = node.number;
        exchangeSlot1Score = node.exchangeNumber1;
        exchangeSlot2Score = node.exchangeNumber2;
        
        UpdateTextGUI();
        
    }
    
    public GameObject SpawnBar(DigitalBarType barType, Vector3 position)
    {
        
        DigitalBarItem barItem = _dictionaryBarItems[barType];

        if (barItem.numberOfItem <= 0) return null;
        
        barItem.numberOfItem--;
        GameObject instantiate = Instantiate(barItem.prefab, position, Quaternion.identity);
        
        UpdateTextGUI();
        return instantiate;
        
    }
    
    public void IncreaseBarNumber(DigitalBarType barType, int value)
    {
        DigitalBarItem barItem = _dictionaryBarItems[barType];

        barItem.numberOfItem+= value;
        UpdateTextGUI();
    }
    
    public void IncreaseExchangeLeftNumber(int value)
    {
        exchangeTurnLeft += value;
        UpdateTextGUI();
    }

 
    private void UpdateTextGUI()
    {
        currentScoreText.text = currentScore.ToString();
        exchangeSlot1Text.text = exchangeSlot1Score.ToString();
        exchangeSlot2Text.text = exchangeSlot2Score.ToString();

        numberOfHorizontalBarText.text = _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem.ToString();
        numberOfVerticalBarText.text = _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem.ToString();
        exchangeTurnLeftText.text = exchangeTurnLeft.ToString() + " LEFT";
    }


    #region NumberGraph


    [Serializable]
    class NumberNode
    {
        public int number;

        public int horizontalBar;
        public int verticalBar;

        public int exchangeNumber1;
        public int exchangeNumber2;
        
    }

    [Header("Number Graph")]
    [SerializeField] private List<NumberNode> numberGraph;

    
   
    public bool OnChoosingExchangeSlot(int slotNumber)
    {
        bool isAffordable = false;
        switch (slotNumber)
        {
            case 1:
                isAffordable = ExchangeNumber(numberGraph[ exchangeSlot1Score ]);
                break;
            
            case 2:
                isAffordable = ExchangeNumber(numberGraph[ exchangeSlot2Score ]);
                break;
                
            default:
                break;
        }
        
        UpdateTextGUI();
        return isAffordable;
    }

    public void OnEnterExchangeSlot(int slotNumber)
    {
        
        int deltaNumberOfHorizontalBar = 0, deltaNumberOfVerticalBar = 0;
        NumberNode oldNumberNode;
        NumberNode newNumberNode;
        switch (slotNumber)
        {
            case 1:
                oldNumberNode = numberGraph[currentScore];
                newNumberNode = numberGraph[ exchangeSlot1Score ];

                deltaNumberOfHorizontalBar =  - newNumberNode.horizontalBar + oldNumberNode.horizontalBar;
                deltaNumberOfVerticalBar =  -  newNumberNode.verticalBar + oldNumberNode.verticalBar;

                break;
            
            case 2:
                
                oldNumberNode = numberGraph[currentScore];
                newNumberNode = numberGraph[ exchangeSlot2Score ];

                deltaNumberOfHorizontalBar =  - newNumberNode.horizontalBar + oldNumberNode.horizontalBar;
                deltaNumberOfVerticalBar =  -  newNumberNode.verticalBar + oldNumberNode.verticalBar;
                break;
                
            default:
                break;
        }
        
        
        numberOfHorizontalBarText.text = _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem.ToString() + (deltaNumberOfHorizontalBar > 0 ? "+" + deltaNumberOfHorizontalBar.ToString() : ( deltaNumberOfHorizontalBar < 0 ?   deltaNumberOfHorizontalBar.ToString(): String.Empty ));
        numberOfVerticalBarText.text = _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem.ToString() + (deltaNumberOfVerticalBar > 0 ? "+" + deltaNumberOfVerticalBar.ToString() : ( deltaNumberOfVerticalBar < 0 ?  deltaNumberOfVerticalBar.ToString(): String.Empty ));

    }
    
    public void OnExitExchangeSlot()
    {
        numberOfHorizontalBarText.text = _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem.ToString();
        numberOfVerticalBarText.text = _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem.ToString();

    }


    private bool ExchangeNumber(NumberNode newNumberNode)
    {
        NumberNode oldNumberNode = numberGraph[currentScore];

        int newNumberOfHorizontalBar = _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem - newNumberNode.horizontalBar + oldNumberNode.horizontalBar;
        int newNumberOfVerticalBar =  _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem -  newNumberNode.verticalBar + oldNumberNode.verticalBar;

        if (exchangeTurnLeft <= 0 || newNumberOfHorizontalBar < 0 || newNumberOfVerticalBar < 0)
        {
            return false;
        }

        _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem = newNumberOfHorizontalBar;
        _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem = newNumberOfVerticalBar;
        
        currentScore = newNumberNode.number;
        exchangeSlot1Score = newNumberNode.exchangeNumber1;
        exchangeSlot2Score = newNumberNode.exchangeNumber2;
        exchangeTurnLeft--;
        
        return true;
    }
    
    #endregion


    #region Collectible

    public void OnOperationCalculation(OperatorType operatorType, int value)
    {
        switch (operatorType)
        {
            case OperatorType.Assignment:
                currentScore = value;
                break;
            
            case OperatorType.Addition:
                currentScore += value;
                break;
            
            case OperatorType.Subtraction:
                currentScore -= value;
                break;
            case OperatorType.Multiplication:
                currentScore *= value;
                break;
            case OperatorType.Division:
                currentScore /= value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(operatorType), operatorType, null);
        }

        currentScore = (currentScore + 10) % 10;
        UpdateTextGUI();
    }
    

    #endregion

    #region Collectible

    public bool OnComparatorCalculation(ComparatorType comparatorType, int value)
    {
        switch (comparatorType)
        {
            case ComparatorType.Null:
                return true;
                
            
            case ComparatorType.LessThan:
                return (_dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem + _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem ) < value;
                
            
            case ComparatorType.MoreThan:
                return (_dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem + _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem ) > value;
                

            case ComparatorType.Equal:
                return (_dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem + _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem ) == value;
                

            default:
                throw new ArgumentOutOfRangeException(nameof(comparatorType), comparatorType, null);
        }

        
    }
    

    #endregion

    
    
}
