using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class LineGrid : MonoBehaviour, ISerializationCallbackReceiver
{
    [Header("Number of Index")] 
    [SerializeField] private int xSize = 1;
    [SerializeField] private int ySize = 1;
    [SerializeField] private int _xCurrentSize = 1, _yCurrentSize = 1;
    
    [Header("GameObject modify")]
    [SerializeField] private GameObject cellGameObject;
    [SerializeField] private Vector3 horizontalOffset = new Vector3(0.5f, 0 ,0 ), verticalOffset = new Vector3(0,0.5f,0);
    [SerializeField] private Quaternion horizontalRotation, verticalRotation;
    
    
    public GameObject [,] GridVerticalCell; 
    public GameObject [,] GridHorizontalCell;

    [SerializeField] private List<GameObject> gridBeforeSerialize;
    
    private void Start()
    {
        ResetGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnValidate () {
        UnityEditor.EditorApplication.delayCall += ResetGrid;
    }
 
    private void InstantiateCell(int xIndex, int yIndex, bool isVerticalNorHorizontal)
    {
        if (xIndex < 0 || xIndex >= xSize || yIndex < 0 || yIndex >= ySize) return;

        if (isVerticalNorHorizontal)
        {
            Vector3 position = Vector3.right * xIndex + Vector3.up * yIndex + verticalOffset;
            GridVerticalCell[xIndex,yIndex] = Instantiate(cellGameObject, position, verticalRotation, transform);

        }
        else 
        {
            Vector3 position = Vector3.right * xIndex + Vector3.up * yIndex + horizontalOffset;
            GridHorizontalCell[xIndex,yIndex] = Instantiate(cellGameObject, position, horizontalRotation, transform);

        }
        
    }
    
    private void DestroyImmediateCell(int xIndex, int yIndex, bool isVerticalNorHorizontal)
    {
        if (xIndex < 0 || xIndex >= _xCurrentSize || yIndex < 0 || yIndex >= _yCurrentSize) return;

        if (isVerticalNorHorizontal)
        {
            if (GridVerticalCell != null && GridVerticalCell[xIndex, yIndex] != null)
            {
                DestroyImmediate( GridVerticalCell[xIndex,yIndex]);
            }
            
        }
        else 
        {
            if (GridHorizontalCell != null && GridHorizontalCell[xIndex, yIndex] != null)
            {
                DestroyImmediate(GridHorizontalCell[xIndex, yIndex]);
            }
        }
        
    }

    private void DestroyAllCell()
    {
        for (int i = 0 ; i< _xCurrentSize; i++)
        {
            for (int j = 0; j < _yCurrentSize; j++)
            {
            
                DestroyImmediateCell(i,j,true);
                DestroyImmediateCell(i,j,false);
            }
        }
    }
    
    private void InitSize()
    {
        GridVerticalCell = new GameObject[xSize, ySize];
        GridHorizontalCell = new GameObject[xSize, ySize];
    }

    private void ResetGrid()
    { 
        DestroyAllCell();
        InitSize();
        
        for (int i = 0 ; i< xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                InstantiateCell(i,j,true);
                InstantiateCell(i,j,false);
            }
        }

        _xCurrentSize = xSize;
        _yCurrentSize = ySize;   
    }

    public void OnBeforeSerialize()
    { 
        gridBeforeSerialize.Clear();
        for (int i = 0 ; i < GridHorizontalCell.GetLength(0); i++)
        {
            for (int j = 0 ; j< GridHorizontalCell.GetLength(1); j++)
            {
                gridBeforeSerialize.Add(GridHorizontalCell[i,j]);
                gridBeforeSerialize.Add(GridVerticalCell[i,j]);
            }            
        }
        
        //Debug.Log("Save " + gridBeforeSerialize.Count.ToString() +" Object");
    }

    public void OnAfterDeserialize()
    {
        DestroyAllCell();
        InitSize();
        for (int i = 0 ; i < xSize; i++)
        {
            for (int j = 0 ; j< ySize; j++)
            {
                if((i*ySize + j)*2 +1 > gridBeforeSerialize.Count) return;
                GridHorizontalCell[i, j] = gridBeforeSerialize[(i*ySize + j)*2];
                GridVerticalCell[i, j] = gridBeforeSerialize[(i*ySize + j)*2+1];

            }            
        }
        //Debug.Log("Load " + gridBeforeSerialize.Count.ToString() +" Object");
        _xCurrentSize = xSize;
        _yCurrentSize = ySize;
    }
}
