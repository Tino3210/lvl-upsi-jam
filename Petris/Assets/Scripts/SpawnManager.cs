using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject piece;
    [SerializeField]
    private Vector2 spawnNorth;
    [SerializeField]
    private Vector2 spawnEast;
    [SerializeField]
    private Vector2 spawnSouth;
    [SerializeField]
    private Vector2 spawnWest;
    [SerializeField]
    private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPetronimo();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }    

    public void SpawnPetronimo() {        
        GameObject petromino = new GameObject();
        petromino.name = "Petromino";        
        petromino.transform.SetParent(gameObject.transform);
        PetrominoManager.Direction direction;

        switch(Random.Range(0, 4)){
            case 0:
                petromino.transform.position = spawnNorth;
                direction = PetrominoManager.Direction.South;
            break;            
            case 1:
                petromino.transform.position = spawnEast;
                direction = PetrominoManager.Direction.West;
            break;
            case 2:
                petromino.transform.position = spawnSouth;
                direction = PetrominoManager.Direction.North;
            break;
            case 3:
                petromino.transform.position = spawnWest;
                direction = PetrominoManager.Direction.East;
            break;
            default:
                petromino.transform.position = spawnNorth;
                direction = PetrominoManager.Direction.South;
            break;
        }

        petromino.AddComponent<PetrominoManager>();
        petromino.GetComponent<PetrominoManager>().direction = direction;        
        
        switch(Random.Range(0, 7)){
            case 0:
                //I                
                Petromino(petromino, new []{new Vector2(0f, 0f), new Vector2(-1f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f)}, direction);
            break;            
            case 1:
                //O                
                Petromino(petromino, new []{new Vector2(.0f, 0f), new Vector2(1f, 0f), new Vector2(.0f, -1f), new Vector2(1, -1f)}, direction);
            break;
            case 2:
                //T                
                Petromino(petromino, new []{new Vector2(.0f, .0f), new Vector2(-1f, 0f), new Vector2(0f, -1f), new Vector2(1f, 0f)}, direction);
            break;
            case 3:
                //L                
                Petromino(petromino, new []{new Vector2(.0f, 0f), new Vector2(0f, -1f), new Vector2(1f, 0f), new Vector2(2f, 0f)}, direction);
            break;
            case 4:
                //J                
                Petromino(petromino, new []{new Vector2(.0f, 0f), new Vector2(-1f, 0f), new Vector2(1f, 0f), new Vector2(1f, -1f)}, direction);
            break;
            case 5:
                //Z                
                Petromino(petromino, new []{new Vector2(.0f, 0f), new Vector2(-1f, 0f), new Vector2(0f, -1f), new Vector2(1f, -1f)}, direction);
            break;
            case 6:
                //S                
                Petromino(petromino, new []{new Vector2(.0f, 0f), new Vector2(1f, 0f), new Vector2(0f, -1f), new Vector2(-1f, -1f)}, direction);
            break;
            default:
                Petromino(petromino, new []{new Vector2(0f, 0f), new Vector2(-1f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f)}, direction);
            break;
        }     
    }

    private void Petromino(GameObject petronimo, Vector2[] positions, PetrominoManager.Direction direction){ 
        int spriteNumber = Random.Range(0, 6);        
        
        foreach (Vector2 pos in positions)
        {
            GameObject newPiece = Instantiate(piece);
            newPiece.tag = "piece";
            newPiece.GetComponent<PieceManager>().RotateCollider(direction);
            newPiece.GetComponent<SpriteRenderer>().sprite = sprites[spriteNumber];                            
            newPiece.transform.SetParent(petronimo.transform);        
            newPiece.transform.localPosition = new Vector2(pos.x,pos.y);
        }               
    }
}
