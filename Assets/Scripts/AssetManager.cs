using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Goal,
    Empty,
    Occupy,
    Null,
    ScorePoint
}

[Serializable]
class GridCellType
{
    public CellType type;
    public GameObject prefab;
}

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;

    void Start()
    {
        Instance = this;
        
        
    }

    [SerializeField] private List<GridCellType> gridCellTypesPrefabs;
    private Dictionary<CellType, GameObject> _dictionaryGridCellTypePrefabs;

    



}
