using UnityEngine;
using System.Collections;

/// <summary>
/// Camera controller that snaps and pans between screen-sized rooms based on the player's position.
/// Rooms are assumed to match the camera's viewport size.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the Player Transform")]
    public Transform player;

    [Header("Pan Settings")]
    [Tooltip("Speed of camera pan interpolation. Higher is faster.")]
    public float panSpeed = 2f;

    [Header("Optional Padding")]
    [Tooltip("Padding (in world units) before camera pans horizontally or vertically.")]
    public Vector2 paddingThreshold = Vector2.zero;

    [Header("Grid Offset")]
    [Tooltip("World-space offset to apply to the entire room grid (shifts origin).")]
    public Vector2 gridOrigin = Vector2.zero;

    // Internal state
    private int currentRoomX;
    private int currentRoomY;
    private bool isPanning = false;

    private Camera cam;
    private float roomWidth;
    private float roomHeight;

    void Start()
    {
        // Cache the Camera component
        cam = GetComponent<Camera>();

        if (!cam.orthographic)
            Debug.LogWarning("RoomBasedCameraController works best with an orthographic camera.");

        // Calculate room size from the camera's orthographic bounds
        roomHeight = cam.orthographicSize * 2f;
        roomWidth = roomHeight * cam.aspect;

        // Determine starting room indices based on player's position minus gridOrigin
        currentRoomX = Mathf.FloorToInt((player.position.x - gridOrigin.x) / roomWidth);
        currentRoomY = Mathf.FloorToInt((player.position.y - gridOrigin.y) / roomHeight);

        // Snap camera to center of the starting room, applying gridOrigin
        Vector3 startPos = new Vector3(
            gridOrigin.x + (currentRoomX + 0.5f) * roomWidth,
            gridOrigin.y + (currentRoomY + 0.5f) * roomHeight,
            transform.position.z
        );
        transform.position = startPos;
    }

    void Update()
    {
        if (isPanning) return;

        // Calculate offset from center of current room
        float centerX = gridOrigin.x + (currentRoomX + 0.5f) * roomWidth;
        float centerY = gridOrigin.y + (currentRoomY + 0.5f) * roomHeight;
        Vector2 offset = new Vector2(player.position.x - centerX, player.position.y - centerY);

        int targetRoomX = currentRoomX;
        int targetRoomY = currentRoomY;

        // Check horizontal boundary (half-room minus padding)
        if (Mathf.Abs(offset.x) > (roomWidth / 2f) - paddingThreshold.x)
            targetRoomX += offset.x > 0 ? 1 : -1;

        // Check vertical boundary (half-room minus padding)
        if (Mathf.Abs(offset.y) > (roomHeight / 2f) - paddingThreshold.y)
            targetRoomY += offset.y > 0 ? 1 : -1;

        // If room index changed, start smooth pan
        if (targetRoomX != currentRoomX || targetRoomY != currentRoomY)
            StartCoroutine(PanToRoom(targetRoomX, targetRoomY));
    }

    IEnumerator PanToRoom(int newRoomX, int newRoomY)
    {
        isPanning = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(
            gridOrigin.x + (newRoomX + 0.5f) * roomWidth,
            gridOrigin.y + (newRoomY + 0.5f) * roomHeight,
            transform.position.z
        );

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * panSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        // Finalize position and update state
        transform.position = targetPos;
        currentRoomX = newRoomX;
        currentRoomY = newRoomY;
        isPanning = false;
    }
}
