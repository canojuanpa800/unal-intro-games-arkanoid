using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Update is called once per frame

    private int _type = 0;
    private int pts = 0;
    Ball _ball = null;
    Paddle _paddle = null;
    
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
                chagePoints();
            }
            else if (this._type == 2){
                pts = 100;
                chagePoints();
            }
            else if (this._type == 3){
                pts = 250;
                chagePoints();
            }
            else if (this._type == 4){
                pts = 500;
                chagePoints();
            }
            else if (this._type == 5){
                _ball = GameObject.FindObjectOfType<Ball>();
                _ball.powerSlow();
            }
            else if (this._type == 6){
                _ball = GameObject.FindObjectOfType<Ball>();
                _ball.powerFast();
            }
            else if (this._type == 7){
                _paddle = GameObject.FindObjectOfType<Paddle>();
                Debug.LogError(_paddle + "<-- paddle get");

            }
            else if (this._type == 8){
                _paddle = GameObject.FindObjectOfType<Paddle>();
            }
            gameObject.SetActive(false);
        }
    }

    public void setId(int type){
        this._type = type;
    }

    private void chagePoints(){
        ArkanoidController.PointsTakeOfPowerUp(pts);
    }

}
