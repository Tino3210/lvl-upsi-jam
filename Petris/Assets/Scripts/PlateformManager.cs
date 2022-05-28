using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformManager : MonoBehaviour
{
    [SerializeField]
    private int sizeArray = 20;
    private Dictionary<Vector2, GameObject> pieces;

    // Start is called before the first frame update
    void Start()
    {
        pieces = new Dictionary<Vector2, GameObject>();        
        /*pieces[(sizeArray/2)-1,(sizeArray/2)-1] = gameObject.transform.GetChild(0).gameObject;
        pieces[(sizeArray/2)+1,(sizeArray/2)+1] = gameObject.transform.GetChild(1).gameObject;
        pieces[(sizeArray/2)+1,(sizeArray/2)-1] = gameObject.transform.GetChild(2).gameObject;
        pieces[(sizeArray/2),(sizeArray/2)] = gameObject.transform.GetChild(3).gameObject;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPetromino(GameObject petromino){
        int nbChild = petromino.transform.childCount;
        for(int i = 0; i < nbChild; i++){
            Transform piece = petromino.transform.GetChild(0);
            piece.tag = "plateform";            
            piece.SetParent(gameObject.transform);
            AddPiece(piece.gameObject);      
        }                        
        Destroy(petromino);
        CheckPerfectSquare();
    }

    private void AddPiece(GameObject piece){

        // FIXME : Marche probablement pas
        Vector2 roundedPos = new Vector2(Mathf.Round(piece.transform.position.x*2)/2, Mathf.Round(piece.transform.position.y*2)/2);

        pieces.Add(roundedPos, piece);
    }

    private void CheckPerfectSquare(){
        int maxLayer = sizeArray/2;
        float halfPiece = 0.5f;
        for(int layer = maxLayer; layer > 1; layer--){
            bool isPetris = true;

            for(int j = 0; j < 2*layer-1; j++){                
                float layerIndex = layer-halfPiece;
                if(!pieces.ContainsKey(new Vector2(layerIndex, layerIndex-j))){
                    isPetris = false;
                    break;
                    }
                if(!pieces.ContainsKey(new Vector2(layerIndex-j, -layerIndex))){
                    isPetris = false;
                    break;
                }
                if(!pieces.ContainsKey(new Vector2(-layerIndex+j, layerIndex))){
                    isPetris = false;
                    break;
                }
                if(!pieces.ContainsKey(new Vector2(-layerIndex, -layerIndex+j))){
                    isPetris = false;
                    break;
                }                    
            }

            if(isPetris){
                for(int j = 0; j < 2*layer-1; j++){                
                    float layerIndex = layer-halfPiece;

                    Destroy(pieces[new Vector2(layerIndex, layerIndex-j)]);
                    Destroy(pieces[new Vector2(layerIndex-j, -layerIndex)]);
                    Destroy(pieces[new Vector2(-layerIndex+j, layerIndex)]);
                    Destroy(pieces[new Vector2(-layerIndex, -layerIndex+j)]);

                    pieces.Remove(new Vector2(layerIndex, layerIndex-j));
                    pieces.Remove(new Vector2(layerIndex-j, -layerIndex));
                    pieces.Remove(new Vector2(-layerIndex+j, layerIndex));
                    pieces.Remove(new Vector2(-layerIndex, -layerIndex+j));


                  
                }
            }         
        }
    }
}
