using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    
    
    private static Dictionary<CellType, GridCellType> _gridCellTypes;
    
    
    [SerializeField] private CellType cellType;
}
