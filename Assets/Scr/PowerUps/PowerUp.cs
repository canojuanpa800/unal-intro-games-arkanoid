using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Update is called once per frame

    private int _type = 0;
    private int pts = 0;

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime);    
    }

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.name == "Paddle") 
        {
            if (this._type == 1){
                pts = 50;
            }
            else if (this._type == 2){
                pts = 100;

            }
            else if (this._type == 3){
                pts = 250;

            }
            else if (this._type == 4){
                pts = 500;

            }
            ArkanoidController.PointsTakeOfPowerUp(pts);
            gameObject.SetActive(false);
        }
    }

    public void setId(int type){
        this._type = type;
    }

}
