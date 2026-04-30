using UnityEngine;

public class WallDisappear : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) 
    {
        if ( other.CompareTag("KeyBlock"))
        {
            if (wallVisual != null)
            {
                wallVisual.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
