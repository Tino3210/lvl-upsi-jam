using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{   
    
    private PetrominoManager.Direction direction;    

    void Start(){
        direction = PetrominoManager.Direction.North;
    }
    void Update(){}

    public void RotateCollider(PetrominoManager.Direction newDirection){
        direction = newDirection;

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.offset = new Vector2(0f, 1f);
        collider.size = new Vector2(0.5f, 0.5f);
        switch(direction){
            case PetrominoManager.Direction.North:
                transform.Rotate(0, 0, 0);
            break;
            case PetrominoManager.Direction.East:
                transform.Rotate(0, 0, 270);
            break;
            case PetrominoManager.Direction.South:
                transform.Rotate(0, 0, 180);
            break;
            case PetrominoManager.Direction.West:
                transform.Rotate(0, 0, 90);
            break;
        }
    }

    public void ResetCollider(){
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);
        collider.offset = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {               
        if (collision.transform.tag == "plateform" &&
        gameObject.transform.parent.transform.tag != "plateform" &&
        !gameObject.transform.parent.gameObject.GetComponent<PetrominoManager>().isTrigger)
        {            
            GameObject parent = gameObject.transform.parent.gameObject; 
            parent.GetComponent<PetrominoManager>().isTrigger = true;            
            parent.GetComponent<PetrominoManager>().isMoving = false;            
            
            GameObject.Find("Plateform").GetComponent<PlateformManager>().AddPetromino(parent);

            StartCoroutine(Spawn());
        }
    }

    public IEnumerator Spawn(){
        GameObject.Find("Main Camera").GetComponent<CameraManager>().RotateCamera();
        yield return new WaitForSeconds(1.5f);                
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().SpawnPetronimo();
    }
}
