using System;
using System.Collections;
using System.Collections.Generic;
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
    
    [Header("Score and Convert")]
    [SerializeField] private int currentScore;
    [SerializeField] private Image scoreImage;

    [Header("Bars")] 
    [SerializeField] private List<DigitalBarItem> listBarItem;

    [SerializeField, HideInInspector] private Dictionary<DigitalBarType, DigitalBarItem> _dictionary;


    [Serializable]
    class DigitalBarItem
    {
        public DigitalBarType barType;
        public int numberOfItem;
        public GameObject prefab;
        
    }

    protected override void OnPersistentSingletonAwake()
    {
        
        _dictionary = new Dictionary<DigitalBarType, DigitalBarItem>();
        foreach (var digitalBarItem in listBarItem)
        {
            _dictionary.Add(digitalBarItem.barType, digitalBarItem);
            //Debug.Log(digitalBarItem.barType);
        }
        
    }
    
    public GameObject SpawnBar(DigitalBarType barType, Vector3 position)
    {
        
        DigitalBarItem barItem = _dictionary[barType];

        if (barItem.numberOfItem <= 0) return null;
        
        barItem.numberOfItem--;
        GameObject instantiate = Instantiate(barItem.prefab, position, Quaternion.identity);
        
        return instantiate;
    }
    
    public void IncreaseBar(DigitalBarType barType)
    {
        DigitalBarItem barItem = _dictionary[barType];

        barItem.numberOfItem++;
    }
    
}
