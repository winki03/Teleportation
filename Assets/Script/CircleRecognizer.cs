using UnityEngine;

public class CircleRecognizer : MonoBehaviour
{
    public Transform objectToTrack;          // Should be the fingertip (e.g. portalPoint from HandTracking)
    public GameObject portalPrefab;          // Drag Portal A prefab here
    private float previousYPosition;
    private bool numUp = false;
    private bool numDown = false;
    private bool hasSpawned = false;

    void Start()
    {
        previousYPosition = objectToTrack.position.y;
    }

    void Update()
    {
        float currentYPosition = objectToTrack.position.y;
        float deltaYPosition = currentYPosition - previousYPosition;

        if (deltaYPosition > 3)  // Customize for sensitivity
            numUp = true;
        else if (deltaYPosition < -3)
            numDown = true;

        if (numUp && numDown && !hasSpawned)
        {
            Debug.Log("🔄 Circle gesture detected!");
            SpawnPortalAtHand();
            hasSpawned = true;

            // Reset after a short delay
            Invoke(nameof(ResetGesture), 2f);
        }

        previousYPosition = currentYPosition;
    }

    void SpawnPortalAtHand()
    {
        Vector3 spawnPos = objectToTrack.position;
        Quaternion spawnRot = Quaternion.LookRotation(Camera.main.transform.forward); // Face forward
        Instantiate(portalPrefab, spawnPos, spawnRot);
    }

    void ResetGesture()
    {
        numUp = false;
        numDown = false;
        hasSpawned = false;
    }
}
