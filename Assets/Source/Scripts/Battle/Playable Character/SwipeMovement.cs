using ArenaHero.Battle.PlayableCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeMovement : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private float _speedHero = 10f;
    [SerializeField] private float _distanceMove = 5f;
    [SerializeField] private Hero _hero;
    
    private Vector3 _direction;
    private bool _isDetectSwipe = false;

    private void Update()
    {
        if (_isDetectSwipe == false)
            return;

        MoveHero();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {       
        DetectSwipe(eventData.delta.x, eventData.delta.y);
    }
  
    public void OnDrag(PointerEventData eventData) { }   

    private void DetectSwipe(float deltaX, float deltaY)
    {
        _isDetectSwipe = true;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
            {
                _direction = new Vector3(0, 0, 1 * _distanceMove);
            }
            else
            {
                _direction = new Vector3(0, 0, -1 * _distanceMove);
            }
            
        }
        else
        {
            if (deltaY > 0)
            {
                _direction = new Vector3(-1 * _distanceMove, 0, 0);
            }
            else
            {
                _direction = new Vector3(1 * _distanceMove, 0, 0);
            }           
        }

        _direction = _hero.transform.position + _direction;
    } 
    
    private void MoveHero()
    {
       
        _hero.transform.position = Vector3.MoveTowards(_hero.transform.position, _direction, _speedHero * Time.deltaTime);

        if (_hero.transform.position == _direction)
        {
            _isDetectSwipe = false;
        }        
    }    
}
