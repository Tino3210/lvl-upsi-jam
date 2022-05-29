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
    // public enum Direction{
    //     North = 0,
    //     West = 90,
    //     South = 180,
    //     East = 270
    // }


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
        if(isMoving && GameManager.Instance.State != GameState.Pause){

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

            bool isMovableLeft = true;
            bool isMovableRight = true;

            for(int i = 0; i < transform.childCount; i++){
                

                isMovableLeft &= transform.GetChild(i).transform.GetChild(0).transform.GetComponent<CheckNeighbour>().isMovableLeft;
                isMovableRight &= transform.GetChild(i).transform.GetChild(1).transform.GetComponent<CheckNeighbour>().isMovableRight;
                
            }

            
            if (distanceTraveled > 1)
            {
                distanceTraveled = 0f;
                walkPiece = 1;
                for(int i = 0; i < transform.childCount; i++){
                    transform.GetChild(i).transform.GetChild(0).transform.GetComponent<CheckNeighbour>().isMovableLeft = true;
                    transform.GetChild(i).transform.GetChild(1).transform.GetComponent<CheckNeighbour>().isMovableRight = true;
                }
            }


            float dashH = 0;
            // dashH += Input.GetKeyDown(KeyCode.D)&&isMovableRight ? 1 : 0;
            // dashH += Input.GetKeyDown(KeyCode.A)&&isMovableLeft ? -1 : 0;


            float dashV = 0;
            // dashV += Input.GetKeyDown(KeyCode.W)&&isMovableUp ? 1 : 0;
            // dashV += Input.GetKeyDown(KeyCode.S)&&isMovableDown ? -1 : 0;


            
            // Get the orientation of the controls relative to the petromino direction and the camera orientation
            float cameraZ = GameObject.Find("Main Camera").transform.rotation.eulerAngles.z;

            // Will be defined by the wasd keys
            bool leftDash = false;
            bool rightDash = false;
            bool upDash = false;
            bool downDash = false;




            // Compensate for the camera orientation and the petromino fall orientation
            int controlOrientation = (int)((360f+(float)direction+cameraZ+180)%360f);



            switch(controlOrientation){
                case 0:
                    rightDash = Input.GetKeyDown(KeyCode.A);
                    leftDash = Input.GetKeyDown(KeyCode.D);
                    break;
                case 90:
                    rightDash = Input.GetKeyDown(KeyCode.W);
                    leftDash = Input.GetKeyDown(KeyCode.S);
                    break;
                case 180:
                    leftDash = Input.GetKeyDown(KeyCode.A);
                    rightDash = Input.GetKeyDown(KeyCode.D);
                    break;
                case 270:
                    leftDash = Input.GetKeyDown(KeyCode.W);
                    rightDash = Input.GetKeyDown(KeyCode.S);
                    break;
            }

            /*Debug.Log("Control orientation: " + controlOrientation);
            Debug.Log("Camera orientation: " + cameraZ);
            Debug.Log("Petromino direction: " + direction);*/

            switch (direction)
            {
                case Direction.North:

                    dashH += leftDash&&isMovableLeft ? -1 : 0;
                    dashH += rightDash&&isMovableRight ? 1 : 0;

                    transform.position = new Vector2(transform.position.x + dashH, transform.position.y + walkPiece);
                break;
                case Direction.East:

                    dashV += leftDash&&isMovableLeft ? 1 : 0;
                    dashV += rightDash&&isMovableRight ? -1 : 0;

                    transform.position = new Vector2(transform.position.x + walkPiece, transform.position.y + dashV);
                break;
                case Direction.South:

                    dashH += rightDash&&isMovableLeft ? -1 : 0;
                    dashH += leftDash&&isMovableRight ? 1 : 0;

                    transform.position = new Vector2(transform.position.x + dashH, transform.position.y - walkPiece);
                break;
                case Direction.West:

                    dashV += rightDash&&isMovableRight ? 1 : 0;
                    dashV += leftDash&&isMovableLeft ? -1 : 0;

                    transform.position = new Vector2(transform.position.x - walkPiece, transform.position.y + dashV);
                break;            
            }




            if(transform.position.x > 16 || transform.position.x < -16 || transform.position.y > 16 || transform.position.y < -16){
                ScoreManager.Instance.LoseLife();
                GameObject.Find("Main Camera").GetComponent<CameraManager>().SpawnAndRotateCamera();
                Destroy(gameObject);                
            }            
        }
    }
}
