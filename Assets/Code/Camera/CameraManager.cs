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

    //[Header("Grid Offset")]
    //[Tooltip("World-space offset to apply to the entire room grid (shifts origin).\nLeave at (0,0) to auto-center room (0,0) at world (0,0).")]
    private Vector2 gridOrigin = Vector2.zero;

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

        // If no custom offset set, auto-offset so room (0,0) centers at world (0,0)
        if (gridOrigin == Vector2.zero)
            gridOrigin = new Vector2(-roomWidth / 2f, -roomHeight / 2f);

        // Determine starting room indices based on player's position minus gridOrigin
        currentRoomX = Mathf.FloorToInt((player.position.x - gridOrigin.x) / roomWidth);
        currentRoomY = Mathf.FloorToInt((player.position.y - gridOrigin.y) / roomHeight);

        // Snap camera to center of the starting room
        transform.position = GetRoomCenter(currentRoomX, currentRoomY);
    }

    void Update()
    {
        if (isPanning) return;

        // Calculate offset from center of current room
        Vector3 center = GetRoomCenter(currentRoomX, currentRoomY);
        Vector2 offset = (Vector2)player.position - (Vector2)center;

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

    /// <summary>
    /// Returns the world-space center of a room, factoring in gridOrigin.
    /// </summary>
    private Vector3 GetRoomCenter(int roomX, int roomY)
    {
        return new Vector3(
            gridOrigin.x + (roomX + 0.5f) * roomWidth,
            gridOrigin.y + (roomY + 0.5f) * roomHeight,
            transform.position.z
        );
    }

    /// <summary>
    /// Smoothly pans the camera to the target room center.
    /// </summary>
    private IEnumerator PanToRoom(int newRoomX, int newRoomY)
    {
        isPanning = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = GetRoomCenter(newRoomX, newRoomY);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * panSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        // Final alignment and update state
        transform.position = targetPos;
        currentRoomX = newRoomX;
        currentRoomY = newRoomY;
        isPanning = false;
    }
}

