using UnityEngine;

public class ProximityBlock : MonoBehaviour
{
    public enum BlockBehavior
    {
        ChangeColor,
        PlaySound,
        Disappear
    }

    [Header("Behavior")]
    public BlockBehavior behavior;

    [Header("Detection")]
    public string handTag = "Hand";
    public string controllerTag = "Controller";

    [Header("Color Change / Disappear")]
    public Renderer targetRenderer;
    public Color normalColor = Color.blue;
    public Color nearColor = Color.white;

    [Header("Audio")]
    public AudioSource audioSource;

    private void Start()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        if (targetRenderer != null && behavior == BlockBehavior.ChangeColor)
        {
            targetRenderer.material.color = normalColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidInteractor(other)) return;

        switch (behavior)
        {
            case BlockBehavior.ChangeColor:
                if (targetRenderer != null)
                    targetRenderer.material.color = nearColor;
                break;

            case BlockBehavior.PlaySound:
                if (audioSource != null && !audioSource.isPlaying)
                    audioSource.Play();
                break;

            case BlockBehavior.Disappear:
                if (targetRenderer != null)
                    targetRenderer.enabled = false;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsValidInteractor(other)) return;

        switch (behavior)
        {
            case BlockBehavior.ChangeColor:
                if (targetRenderer != null)
                    targetRenderer.material.color = normalColor;
                break;

            case BlockBehavior.Disappear:
                if (targetRenderer != null)
                    targetRenderer.enabled = true;
                break;
        }
    }

    private bool IsValidInteractor(Collider other)
    {
        return other.CompareTag(handTag) || other.CompareTag(controllerTag);
    }
}