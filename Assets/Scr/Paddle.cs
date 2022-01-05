using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

[SerializeField]
private float _speed = 5;
[SerializeField]
private float _movementLimit = 7;

private Vector3 _targetPosition;

private CapsuleCollider2D _collider;
private int size = 2;

private Camera _cam;
private Camera Camera
{
   get
   {
       if (_cam == null)
       {
           _cam = Camera.main;
       }
       return _cam;
   }
}

void Update()
{
   _targetPosition.x = Camera.ScreenToWorldPoint(Input.mousePosition).x;
   _targetPosition.x = Mathf.Clamp(_targetPosition.x, -_movementLimit, _movementLimit);
   _targetPosition.y = this.transform.position.y;
   
   transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _speed);
}

public void changeCollider(int change){
    _collider = GetComponent<CapsuleCollider2D>();
    GameObject paddleBody = transform.GetChild(0).gameObject;
    size = size + change;
    if (size < 1 ){
        size = 1;
    }
    if (size > 3){
        size = 3;
    }

    if (size == 1){
        _collider.size = new Vector2(1.2f, _collider.size.y);
        paddleBody.transform.localScale =  new Vector3( 0.6f , paddleBody.transform.localScale.y , paddleBody.transform.localScale.z);
        Debug.LogError("Entra en 1");
    }
    else if( size == 2)
    {
        _collider.size = new Vector2(1.68f, _collider.size.y);
        paddleBody.transform.localScale =  new Vector3( 0.86f , paddleBody.transform.localScale.y , paddleBody.transform.localScale.z);
        Debug.LogError("Entra en 2");
    }
    else if (size == 3)
    {
        _collider.size = new Vector2(2.3f, _collider.size.y);
        paddleBody.transform.localScale =  new Vector3( 1.2f , paddleBody.transform.localScale.y , paddleBody.transform.localScale.z);
        Debug.LogError("Entra en 3");
    }
} 

}
