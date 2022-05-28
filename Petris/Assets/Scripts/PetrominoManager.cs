using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrominoManager : MonoBehaviour
{   
    public enum Direction{
        North,
        East,
        South,
        West
    }

    public Direction direction;
    public float moveSpeed = 1f;
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
            float displacement = moveSpeed * Time.deltaTime;
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
