using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DigitalBarItemButton : MonoBehaviour
{

    [SerializeField] private DigitalBarType digitalBarType;

    [SerializeField, HideInInspector] private GameObject spawnedGameObject = null;

    

    private void OnMouseDown()
    {
        Debug.Log("Down");

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
        Debug.Log("Up");
        spawnedGameObject.GetComponent<DigitalBar>().DropSpawned();
        spawnedGameObject = null;
    }
}
