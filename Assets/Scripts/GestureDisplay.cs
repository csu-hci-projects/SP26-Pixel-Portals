using UnityEngine;
using TMPro;

public class GestureDisplay : MonoBehaviour
{
    public TMP_Text messageText;
    public float messageDuration = 2f;

    private float hideTimer;

    void Start()
    {
        if (messageText != null)
            messageText.text = "Gesture system ready";
    }

    void Update()
    {
        if (hideTimer > 0f)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f && messageText != null)
                messageText.text = "";
        }
    }

    public void ShowMessage(string message)
    {
        if (messageText == null) return;

        messageText.text = message;
        hideTimer = messageDuration;
        Debug.Log(message);
    }
}