using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private bool isRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateCamera(){
        if(!isRotate)
        {            
            StartCoroutine( Rotate( new Vector3(0, 0, 90), 1 ) );                      
        }
    }    

    private IEnumerator Rotate( Vector3 angles, float duration )
    {
        isRotate = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
        for(float t = 0; t < duration; t+= Time.deltaTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration );
            yield return null;
        }
        transform.rotation = endRotation;
        isRotate = false;
    }

    public IEnumerator Spawn()
    {
        GameObject.Find("Main Camera").GetComponent<CameraManager>().RotateCamera();
        Debug.Log("Camera");
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Spawn");
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().SpawnPetronimo();
    }
}
