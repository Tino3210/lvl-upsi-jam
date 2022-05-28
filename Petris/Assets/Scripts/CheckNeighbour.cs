using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNeighbour : MonoBehaviour
{
    public bool isMovableLeft = true;
    public bool isMovableRight = true;

    void Start(){}
    void Update(){}

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if(collision.transform.tag == "plateform" && gameObject.tag == "left"){
    //         isMovableLeft = true;            
    //     }

    //     if(collision.transform.tag == "plateform" && gameObject.tag == "right"){
    //         isMovableRight = true;                   
    //     }    
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "plateform" && gameObject.tag == "left"){
            isMovableLeft = false;       
        }

        if(collision.transform.tag == "plateform" && gameObject.tag == "right"){
            isMovableRight = false;               
        }
    }


}
