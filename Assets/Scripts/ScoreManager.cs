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
public class ScoreManager : PersistentSingletonMonoBehaviour<ScoreManager>
{
    
    [Header("Score and Text")]
    [SerializeField] private int currentScore;

    [SerializeField] private int exchangeSlot1Score, exchangeSlot2Score;
    
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI exchangeSlot1Text;
    [SerializeField] private TextMeshProUGUI exchangeSlot2Text;
    
    [SerializeField] private TextMeshProUGUI numberOfHorizontalBarText;
    [SerializeField] private TextMeshProUGUI numberOfVerticalBarText;

    
    [SerializeField] private TextMeshProUGUI deltaHorizontalBarText;
    [SerializeField] private TextMeshProUGUI deltaVerticalBarText;

    

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

    protected override void OnPersistentSingletonAwake()
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
        
        return instantiate;
    }
    
    public void IncreaseBarNumber(DigitalBarType barType, int value)
    {
        DigitalBarItem barItem = _dictionaryBarItems[barType];

        barItem.numberOfItem+= value;
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

    
    private void UpdateTextGUI()
    {
        currentScoreText.text = currentScore.ToString();
        exchangeSlot1Text.text = exchangeSlot1Score.ToString();
        exchangeSlot2Text.text = exchangeSlot2Score.ToString();

        numberOfHorizontalBarText.text = _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem.ToString();
        numberOfVerticalBarText.text = _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem.ToString();
    }

    public void OnChoosingExchangeSlot(int slotNumber)
    {
        switch (slotNumber)
        {
            case 1:
                ExchangeNumber(numberGraph[ exchangeSlot1Score ]);
                break;
            
            case 2:
                ExchangeNumber(numberGraph[ exchangeSlot2Score ]);
                break;
                
            default:
                break;
        }
        
        UpdateTextGUI();
    }

    public int OnEnterExchangeSlot(int slotNumber)
    {
        switch (slotNumber)
        {
            case 1:
                
                NumberNode oldNumberNode = numberGraph[currentScore], newNumberNode =  numberGraph[ exchangeSlot1Score ];

                int deltaNumberOfHorizontalBar =  - newNumberNode.horizontalBar + oldNumberNode.horizontalBar;
                int deltaNumberOfVerticalBar =  -  newNumberNode.verticalBar + oldNumberNode.verticalBar;

                break;
            
            case 2:
                
                break;
                
            default:
                break;
        }
        
        UpdateTextGUI();
    }


    private bool ExchangeNumber(NumberNode newNumberNode)
    {
        NumberNode oldNumberNode = numberGraph[currentScore];

        int newNumberOfHorizontalBar = _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem - newNumberNode.horizontalBar + oldNumberNode.horizontalBar;
        int newNumberOfVerticalBar =  _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem -  newNumberNode.verticalBar + oldNumberNode.verticalBar;

        if (newNumberOfHorizontalBar < 0 || newNumberOfVerticalBar < 0)
        {
            return false;
        }

        _dictionaryBarItems[DigitalBarType.Horizontal].numberOfItem = newNumberOfHorizontalBar;
        _dictionaryBarItems[DigitalBarType.Vertical].numberOfItem = newNumberOfVerticalBar;
        
        currentScore = newNumberNode.number;
        exchangeSlot1Score = newNumberNode.exchangeNumber1;
        exchangeSlot2Score = newNumberNode.exchangeNumber2;

        return true;
    }
    
    #endregion

    



}
