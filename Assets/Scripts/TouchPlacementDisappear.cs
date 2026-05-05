using UnityEngine;

public class TouchPlacementDisappear : MonoBehaviour
{
    [Header("Object To Disappear")]
    public GameObject objectToDisappear;

    [Header("Optional Settings")]
    public bool disappearOnlyOnce = true;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (disappearOnlyOnce && hasTriggered)
        {
            return;
        }

        // For now, any player/hand/body collider touching this placement spot will trigger it.
        hasTriggered = true;

        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
            Debug.Log(objectToDisappear.name + " disappeared after touching " + gameObject.name);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " has no objectToDisappear assigned.");
        }
    }
}