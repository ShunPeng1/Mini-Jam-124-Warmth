using System;
using System.Collections;
using System.Collections.Generic;
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
            spawnedGameObject.GetComponent<DigitalBar>().SpawnInit();

        }
        else
        {
            
        }
                
    }

    private void OnMouseUp()
    {
        spawnedGameObject.GetComponent<DigitalBar>().DropSpawned();
        spawnedGameObject = null;
    }
}
