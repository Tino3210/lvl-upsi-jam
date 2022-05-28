using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{   
    void Start(){}
    void Update(){}

    private void OnTriggerEnter2D(Collider2D collision)
    {               
        if (collision.transform.tag == "plateform" &&
        gameObject.transform.parent.transform.tag != "plateform" &&
        !gameObject.transform.parent.gameObject.GetComponent<PetrominoManager>().isTrigger)
        {            
            GameObject parent = gameObject.transform.parent.gameObject; 
            parent.GetComponent<PetrominoManager>().isTrigger = true;            
            parent.GetComponent<PetrominoManager>().isMoving = false;
            int nbChild = parent.transform.childCount;
            for(int i = 0; i < nbChild; i++){
                parent.transform.GetChild(0).tag = "plateform";
                parent.transform.GetChild(0).SetParent(collision.transform);
            }
            Destroy(parent);
            GameObject.Find("SpawnManager").GetComponent<SpawnManager>().SpawnPetronimo();
        }
    }
}
