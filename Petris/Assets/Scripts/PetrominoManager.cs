using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrominoManager : MonoBehaviour
{   
    public enum Direction{
        North = 0,
        East = 90,
        South = 180,
        West = 270
    }

    public Direction direction;
    public float moveSpeed = 2f;
    public float moveSpeedFast = 10f;
    public bool isMoving = true;
    public bool isTrigger = false;

    private float distanceTraveled = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){

            float rotation = Input.GetKeyDown(KeyCode.Q) ? 90f : 0f;
            rotation += Input.GetKeyDown(KeyCode.E) ? -90f : 0f;
            if(rotation != 0f){
                transform.Rotate(0, 0, rotation);
                
                for(int i = 0; i < transform.childCount; i++){
                    transform.GetChild(i).GetComponent<PieceManager>().RotateCollider((Direction)((int)((360f+rotation)%360f)));
                }
            }



            float displacement = Time.deltaTime;
            displacement *= Input.GetKey(KeyCode.Space) ? moveSpeedFast : moveSpeed;

            float walkPiece = 0f;

            distanceTraveled += displacement;

            walkPiece = 0;

            if (distanceTraveled > 1)
            {
                distanceTraveled = 0f;
                walkPiece = 1;
            }


            float dashH = Input.GetKeyDown(KeyCode.D) ? 1 : 0;
            dashH += Input.GetKeyDown(KeyCode.A) ? -1 : 0;


            float dashV = Input.GetKeyDown(KeyCode.W) ? 1 : 0;
            dashV += Input.GetKeyDown(KeyCode.S) ? -1 : 0;          

            switch (direction)
            {
                case Direction.North:


                    transform.position = new Vector2(transform.position.x + dashH, transform.position.y + walkPiece);
                break;
                case Direction.East:
                    transform.position = new Vector2(transform.position.x + walkPiece, transform.position.y + dashV);
                break;
                case Direction.South:
                    transform.position = new Vector2(transform.position.x + dashH, transform.position.y - walkPiece);
                break;
                case Direction.West:
                    transform.position = new Vector2(transform.position.x - walkPiece, transform.position.y + dashV);
                break;            
            }
        }
    }    
}
