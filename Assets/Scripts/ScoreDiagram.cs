using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDiagram : MonoBehaviour
{
    [SerializeField] private GameObject scoreDiagram;
    private void OnMouseEnter()
    {
        scoreDiagram.SetActive(true);
    }

    private void OnMouseExit()
    {
        scoreDiagram.SetActive(false);
    }
}
