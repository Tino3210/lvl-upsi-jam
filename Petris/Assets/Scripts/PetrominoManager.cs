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








            switch (direction)
            {
                case Direction.North:

                    dashH += Input.GetKeyDown(KeyCode.D)&&isMovableRight ? 1 : 0;
                    dashH += Input.GetKeyDown(KeyCode.A)&&isMovableLeft ? -1 : 0;

                    transform.position = new Vector2(transform.position.x + dashH, transform.position.y + walkPiece);
                break;
                case Direction.East:

                    dashV += Input.GetKeyDown(KeyCode.W)&&isMovableLeft ? 1 : 0;
                    dashV += Input.GetKeyDown(KeyCode.S)&&isMovableRight ? -1 : 0;

                    transform.position = new Vector2(transform.position.x + walkPiece, transform.position.y + dashV);
                break;
                case Direction.South:

                    dashH += Input.GetKeyDown(KeyCode.D)&&isMovableLeft ? 1 : 0;
                    dashH += Input.GetKeyDown(KeyCode.A)&&isMovableRight ? -1 : 0;

                    transform.position = new Vector2(transform.position.x + dashH, transform.position.y - walkPiece);
                break;
                case Direction.West:

                    dashV += Input.GetKeyDown(KeyCode.W)&&isMovableRight ? 1 : 0;
                    dashV += Input.GetKeyDown(KeyCode.S)&&isMovableLeft ? -1 : 0;

                    transform.position = new Vector2(transform.position.x - walkPiece, transform.position.y + dashV);
                break;            
            }




            if(transform.position.x > 23 || transform.position.x < -23 || transform.position.y > 23 || transform.position.y < -23){
                Debug.Log("-1 PV");
                Destroy(gameObject);
                StartCoroutine(Spawn());
            }            
        }
    }

    public IEnumerator Spawn(){
        GameObject.Find("Main Camera").GetComponent<CameraManager>().RotateCamera();
        yield return new WaitForSeconds(1.5f);                
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().SpawnPetronimo();
    }
}
