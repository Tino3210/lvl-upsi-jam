using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float durationPetromino = 0.1f;
    [SerializeField]
    private float magnitudePetromino = 0.1f;
    [SerializeField]
    private float durationPetris = 0.1f;
    [SerializeField]
    private float magnitudePetris = 0.1f;

    public void Shake(int num){
        if(num == 0){
            StartCoroutine(Shake(durationPetromino, magnitudePetromino));
        }
        else if(num == 1){
            StartCoroutine(Shake(durationPetris, magnitudePetris));
        }        
    }

    public IEnumerator Shake(float duration, float magnitude){
        Vector3 initPosition = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, initPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initPosition;
    }    
}
