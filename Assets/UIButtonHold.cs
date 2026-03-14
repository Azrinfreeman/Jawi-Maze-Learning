using Player;
using UnityEngine;
using UnityEngine.EventSystems; // Required for touch/pointer events

// Implementing these interfaces allows the script to detect press and release
public class UIButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Player Reference")]
    [Tooltip("Drag the GameObject with your player script here.")]
    public PlayerController player; // CHANGE THIS to your actual player script name

    [Header("Direction Settings")]
    [Tooltip("The direction this button should send (e.g., X:0, Y:1 for Up)")]
    public Vector2 moveDirection;

    // This runs the moment your finger touches the button
    public void OnPointerDown(PointerEventData eventData)
    {
        if (player != null)
        {
            player.SetInputDirection(moveDirection);
        }
    }

    // This runs the moment your finger lets go of the button
    public void OnPointerUp(PointerEventData eventData)
    {
        if (player != null)
        {
            // Optional: Send a zero vector to stop the player when releasing the button
            // Note: I commented this out because your current script ignores Vector2.zero!
            // player.SetInputDirection(Vector2.zero);
        }
    }
}
