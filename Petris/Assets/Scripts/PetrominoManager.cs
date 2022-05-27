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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.North:
                transform.position = new Vector2(transform.position.x, transform.position.y + (moveSpeed  * Time.deltaTime));
            break;
            case Direction.East:
                transform.position = new Vector2(transform.position.x + (moveSpeed  * Time.deltaTime), transform.position.y);
            break;
            case Direction.South:
                transform.position = new Vector2(transform.position.x, transform.position.y - (moveSpeed  * Time.deltaTime));
            break;
            case Direction.West:
                transform.position = new Vector2(transform.position.x - (moveSpeed  * Time.deltaTime), transform.position.y);
            break;            
        }
    }
}
