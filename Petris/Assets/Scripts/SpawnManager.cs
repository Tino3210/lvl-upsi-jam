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
        Debug.Log("Start");
        SpawnPetronimo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPetronimo() {
        GameObject petromino = new GameObject();
        petromino.name = "Petromino";
        petromino.transform.SetParent(gameObject.transform);
        GameObject newPetromino = Instantiate(petromino);
        PetrominoManager.Direction direction;

        switch(Random.Range(0, 3)){
            case 0:
                newPetromino.transform.position = spawnNorth;
                direction = PetrominoManager.Direction.South;
            break;            
            case 1:
                newPetromino.transform.position = spawnEast;
                direction = PetrominoManager.Direction.West;
            break;
            case 2:
                newPetromino.transform.position = spawnSouth;
                direction = PetrominoManager.Direction.North;
            break;
            case 3:
                newPetromino.transform.position = spawnWest;
                direction = PetrominoManager.Direction.East;
            break;
            default:
                newPetromino.transform.position = spawnNorth;
                direction = PetrominoManager.Direction.South;
            break;
        }
        
        switch(Random.Range(0, 6)){
            case 0:
                //I                
                Petromino(newPetromino, new []{new Vector2(0f, 0f), new Vector2(-1f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f)}, direction);
            break;            
            case 1:
                //O                
                Petromino(newPetromino, new []{new Vector2(.0f, 0f), new Vector2(1f, 0f), new Vector2(.0f, -1f), new Vector2(1, -1f)}, direction);
            break;
            case 2:
                //T                
                Petromino(newPetromino, new []{new Vector2(.0f, .0f), new Vector2(-1f, 0f), new Vector2(0f, -1f), new Vector2(1f, 0f)}, direction);
            break;
            case 3:
                //L                
                Petromino(newPetromino, new []{new Vector2(.0f, 0f), new Vector2(0f, -1f), new Vector2(1f, 0f), new Vector2(2f, 0f)}, direction);
            break;
            case 4:
                //J                
                Petromino(newPetromino, new []{new Vector2(.0f, 0f), new Vector2(-1f, 0f), new Vector2(1f, 0f), new Vector2(1f, -1f)}, direction);
            break;
            case 5:
                //Z                
                Petromino(newPetromino, new []{new Vector2(.0f, 0f), new Vector2(-1f, 0f), new Vector2(-1f, 0f), new Vector2(1f, -1f)}, direction);
            break;
            case 6:
                //S                
                Petromino(newPetromino, new []{new Vector2(.0f, 0f), new Vector2(1f, 0f), new Vector2(0f, -1f), new Vector2(-1f, -1f)}, direction);
            break;
            default:
                Petromino(newPetromino, new []{new Vector2(0f, 0f), new Vector2(-1f, 0f), new Vector2(1f, 0f), new Vector2(2f, 0f)}, direction);
            break;
        }     
    }

    private void Petromino(GameObject petronimo, Vector2[] positions, PetrominoManager.Direction direction){ 
        int spriteNumber = Random.Range(0, 6);        
        
        foreach (Vector2 pos in positions)
        {
            GameObject newPiece = Instantiate(piece);  
            newPiece.GetComponent<SpriteRenderer>().sprite = sprites[spriteNumber];    
            newPiece.AddComponent<PetrominoManager>();
            newPiece.GetComponent<PetrominoManager>().direction = direction;
            newPiece.transform.SetParent(petronimo.transform);
            newPiece.GetComponent<PetrominoManager>().direction = direction;
            newPiece.transform.localPosition = new Vector2(pos.x,pos.y);
        }               
    }
}
