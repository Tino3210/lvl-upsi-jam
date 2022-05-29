using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformManager : MonoBehaviour
{
    [SerializeField]
    private int sizeArray = 30;
    private Dictionary<Vector2, GameObject> pieces;
    public int score = 0;

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

        Vector2 roundedPos = new Vector2(Mathf.Round(piece.transform.position.x*2)/2, Mathf.Round(piece.transform.position.y*2)/2);
        piece.GetComponent<PieceManager>().ResetCollider();
        pieces.Add(roundedPos, piece); // C'est ici qu'on a des erreurs quand on a des pi�ces qui sont en overlap
    }
    
    // Fonction pour ajouter les poinnts ?
    // Pas safe, car on check pas si la pi�ce existent dans le dictionnaire (pour �viter de la redondance dans le code)
    private void UnsafeBreakPiece(Vector2 piecePosition)
    {
        Destroy(pieces[piecePosition]);
        pieces.Remove(piecePosition);
        score += 1;
    }

    // Retire une pi�ce si elle existe
    private void SafeBreakPiece(Vector2 piecePosition)
    {
        if (pieces.ContainsKey(piecePosition))
        {
            UnsafeBreakPiece(piecePosition);
        }
    }

    private void MovePiece(Vector2 oldPos, Vector2 newPos)
    {

        if (pieces.ContainsKey(oldPos))
        {
            //Destroy(pieces[oldPos]);
            GameObject piece = pieces[oldPos];

            pieces.Remove(oldPos);

            if (pieces.ContainsKey(newPos))
            {
                Destroy(pieces[newPos]);
                score += 1;
                pieces.Remove(newPos);
            }
            piece.transform.position = newPos;
            pieces.Add(newPos, piece);

        }
    }


    private void LayerBreaker(int brokenLayer)
    {
        int maxLayer = sizeArray / 2;
        float halfPiece = 0.5f;

        // Du centre vers l'ext�rieur
        // Retirer les pi�ces dans les coins des couches sup�rieures
        // Et d�caller vers les pi�ces vers le bas
        for (int layer = brokenLayer + 1; layer <= maxLayer; layer++)
        {
            float layerIndex = layer - halfPiece;

            for (int j = 0; j < 2 * layer - 1; j++)
            {
                if (j == 0)
                {
                    SafeBreakPiece(new Vector2(layerIndex, layerIndex));
                    SafeBreakPiece(new Vector2(layerIndex, -layerIndex));
                    SafeBreakPiece(new Vector2(-layerIndex, layerIndex));
                    SafeBreakPiece(new Vector2(-layerIndex, -layerIndex));
                }
                else
                {
                    MovePiece(new Vector2(layerIndex, layerIndex - j), new Vector2(layerIndex - 1, layerIndex - j));
                    MovePiece(new Vector2(layerIndex - j, -layerIndex), new Vector2(layerIndex - j, -layerIndex+1));
                    MovePiece(new Vector2(-layerIndex + j, layerIndex), new Vector2(-layerIndex + j, layerIndex-1));
                    MovePiece(new Vector2(-layerIndex, -layerIndex + j), new Vector2(-layerIndex+1, -layerIndex + j));

                    /*
                    SafeBreakPiece(new Vector2(layerIndex, layerIndex - j));
                    SafeBreakPiece(new Vector2(layerIndex - j, -layerIndex));
                    SafeBreakPiece(new Vector2(-layerIndex + j, layerIndex));
                    SafeBreakPiece(new Vector2(-layerIndex, -layerIndex + j));
                    */
                    
                }
            }
        }

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

                    //Destroy(pieces[new Vector2(layerIndex, layerIndex-j)]);
                    //Destroy(pieces[new Vector2(layerIndex-j, -layerIndex)]);
                    //Destroy(pieces[new Vector2(-layerIndex+j, layerIndex)]);
                    //Destroy(pieces[new Vector2(-layerIndex, -layerIndex+j)]);

                    //pieces.Remove(new Vector2(layerIndex, layerIndex-j));
                    //pieces.Remove(new Vector2(layerIndex-j, -layerIndex));
                    //pieces.Remove(new Vector2(-layerIndex+j, layerIndex));
                    //pieces.Remove(new Vector2(-layerIndex, -layerIndex+j));

                    // Removing pieces from full layer
                    UnsafeBreakPiece(new Vector2(layerIndex, layerIndex - j));
                    UnsafeBreakPiece(new Vector2(layerIndex - j, -layerIndex));
                    UnsafeBreakPiece(new Vector2(-layerIndex + j, layerIndex));
                    UnsafeBreakPiece(new Vector2(-layerIndex, -layerIndex + j));
                }            
                LayerBreaker(layer);
                layer = maxLayer; // Recheck until we no more perfect squares left
            }         
        }
    }    
}
