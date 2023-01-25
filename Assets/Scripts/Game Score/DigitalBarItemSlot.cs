using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DigitalBarItemSlot : MonoBehaviour
{

    [SerializeField] private DigitalBarType digitalBarType;
    
    [SerializeField, HideInInspector] private GameObject spawnedGameObject = null;

    

    private void OnMouseDown()
    {
        
        if (spawnedGameObject != null) return;
        
        spawnedGameObject = ScoreManager.Instance.SpawnBar(digitalBarType, transform.position);
        if (spawnedGameObject != null)
        {
            spawnedGameObject.GetComponent<MirrorBar>().SpawnInit();
            
        }
        else
        {
            
        }
                
    }

    private void OnMouseUp()
    {
        if (spawnedGameObject == null) return;
        spawnedGameObject.GetComponent<MirrorBar>().DropSpawned();
        spawnedGameObject = null;
    }
}
