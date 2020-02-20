using UnityEngine;

public class BoatContoller : MonoBehaviour
{
    
    void FixedUpdate()
    {
        gameObject.transform.Rotate(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.2f, 0.2f));

    }
}
