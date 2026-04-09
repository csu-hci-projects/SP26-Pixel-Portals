using UnityEngine;

public class HandGestureRecognizer : MonoBehaviour
{
    public enum HandType
    {
        Left,
        Right
    }

    private enum GestureType
    {
        None,
        ThumbsUp,
        Fist,
        Peace
    }

    [Header("Hand")]
    public HandType handType;

    [Header("Bone References")]
    public Transform palm;
    public Transform thumbTip;
    public Transform indexTip;
    public Transform middleTip;
    public Transform ringTip;
    public Transform pinkyTip;

    [Header("Display")]
    public GestureDisplay gestureDisplay;

    [Header("Thresholds")]
    public float curledDistance = 0.08f;
    public float extendedDistance = 0.12f;
    public float messageCooldown = 1.0f;

    [Header("Group Requirements")]
    public bool detectThumbsUp = true;
    public bool detectFist = true;
    public bool detectPeace = true;

    private GestureType currentGesture = GestureType.None;
    private float cooldownTimer = 0f;

    private void Update()
    {
        if (!HasAllReferences()) return;

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        GestureType detected = DetectGesture();

        if (detected != GestureType.None && detected != currentGesture && cooldownTimer <= 0f)
        {
            currentGesture = detected;
            cooldownTimer = messageCooldown;
            ShowGestureMessage(detected);
            Debug.Log(handType + " detected: " + detected);
        }
        else if (detected == GestureType.None)
        {
            currentGesture = GestureType.None;
        }
    }

    private bool HasAllReferences()
    {
        return palm != null &&
               thumbTip != null &&
               indexTip != null &&
               middleTip != null &&
               ringTip != null &&
               pinkyTip != null &&
               gestureDisplay != null;
    }

    private GestureType DetectGesture()
    {
        if (detectThumbsUp && IsThumbsUp())
            return GestureType.ThumbsUp;

        if (detectFist && IsFist())
            return GestureType.Fist;

        if (detectPeace && IsPeaceSign())
            return GestureType.Peace;

        return GestureType.None;
    }

    private bool IsFist()
    {
        float thumbDistance = Vector3.Distance(thumbTip.position, palm.position);

        return IsCurled(indexTip) &&
               IsCurled(middleTip) &&
               IsCurled(ringTip) &&
               IsCurled(pinkyTip) &&
               thumbDistance < 0.075f;
    }

    private bool IsThumbsUp()
    {
        float thumbDistance = Vector3.Distance(thumbTip.position, palm.position);

        return IsCurled(indexTip) &&
               IsCurled(middleTip) &&
               IsCurled(ringTip) &&
               IsCurled(pinkyTip) &&
               thumbDistance > 0.09f;
    }

    private bool IsPeaceSign()
    {
        return Vector3.Distance(indexTip.position, palm.position) > 0.10f &&
            Vector3.Distance(middleTip.position, palm.position) > 0.10f &&
            IsCurled(ringTip) &&
            IsCurled(pinkyTip);
    }

    private bool IsCurled(Transform fingerTip)
    {
        return Vector3.Distance(fingerTip.position, palm.position) < curledDistance;
    }

    private bool IsExtended(Transform fingerTip)
    {
        return Vector3.Distance(fingerTip.position, palm.position) > extendedDistance;
    }

    private void ShowGestureMessage(GestureType gesture)
    {
        string handName = handType == HandType.Left ? "Left hand" : "Right hand";

        switch (gesture)
        {
            case GestureType.ThumbsUp:
                gestureDisplay.ShowMessage(handName + " thumbs up!");
                break;
            case GestureType.Fist:
                gestureDisplay.ShowMessage(handName + " fist!");
                break;
            case GestureType.Peace:
                gestureDisplay.ShowMessage(handName + " Peace Sign!");
                break;
        }
    }
}