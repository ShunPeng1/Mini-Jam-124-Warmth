using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBounce : MonoBehaviour
{
    private LineRenderer _laser;

    private int _count ;
    private GameObject _lastHitObject = null;
    
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private float originalRotationDegree = 0;

    [SerializeField] private int maxBounce = 20;
    [SerializeField] private LayerMask collideLayerMask;

    
    
    private void Start()
    {
        _laser = GetComponent<LineRenderer>();
        _laser.positionCount = maxBounce;
        
    }
    private void Update()
    {
        _count = 0;
        CastLaser(transform.position + positionOffset, new Vector3( Mathf.Cos(originalRotationDegree*Mathf.Deg2Rad),Mathf.Sin(originalRotationDegree*Mathf.Deg2Rad) ,0));
        
    }
    private void CastLaser(Vector3 position , Vector3 direction)
    {
        _laser.SetPosition(0, transform.position + positionOffset );
        _lastHitObject = gameObject;
        
        for (int i=0; i< maxBounce; i++ )
        {
            
            Ray ray = new Ray(position, direction);

            if(_count < maxBounce - 1) _count++;

            RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction, 300, collideLayerMask );
            
            Array.Sort(hits,
                delegate(RaycastHit2D x, RaycastHit2D y) { return x.distance.CompareTo(y.distance); });

            
            foreach (var hit in hits)
            {
                    
                if ( hit.transform.gameObject == _lastHitObject)
                {
                    continue;
                }

                _lastHitObject = hit.transform.gameObject;
                
                if (hit.transform.CompareTag("Mirror")) 
                {
                    position = hit.point;
                    direction = Vector3.Reflect(direction, hit.normal);
                    _laser.SetPosition(_count, hit.point);
                }

                if (hit.transform.CompareTag("OperatorCollectible"))
                {
                    hit.transform.gameObject.GetComponent<ScoreOperator>().SendCalculation();
                }
                else
                {
                    for (int j = i+1; j < maxBounce; j++)
                    {
                        _laser.SetPosition(j, hit.point);

                    }
                }
                
                
                
                goto SuccessfullyHit;
                
            }


            _laser.SetPosition( _count, ray.GetPoint(300));

            SuccessfullyHit: ;

        }
      
    }
}
