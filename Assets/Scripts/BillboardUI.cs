using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Include this if using TextMeshPro

public class BillboardUISwitcher : MonoBehaviour
{
    public Image uiImage;                 // Reference to the UI Image component
    public Sprite image1;                 // First sprite
    public Sprite image2;                 // Second sprite
    public float switchInterval = 2f;     // Interval in seconds to switch images
    public float proximityDistance = 15f; // Distance within which to switch images
    public TextMeshProUGUI uiText;        // Text component to show messages
    // public Text uiText;                // Use this instead if using standard UI Text
    public string text1 = "Welcome to Billboard A"; // First text message
    public Canvas additionalCanvas;       // Reference to the additional Canvas

    private GameObject player;
    private bool isPlayerNear = false;
    private float timer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged 'Player'.");
            return;
        }

        if (uiImage != null)
        {
            uiImage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("UI Image is not assigned!");
        }

        if (uiText != null)
        {
            uiText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("UI Text is not assigned!");
        }

        if (image1 == null || image2 == null)
        {
            Debug.LogError("Sprites are not assigned!");
        }

        if (additionalCanvas != null)
        {
            additionalCanvas.gameObject.SetActive(false); // Ensure the additional Canvas is initially hidden
        }
        else
        {
            Debug.LogError("Additional Canvas is not assigned!");
        }
    }

    void Update()
    {
        if (player == null || uiImage == null || uiText == null || additionalCanvas == null) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);
        Debug.Log($"Player Position: {player.transform.position}, Billboard Position: {transform.position}, Distance to player: {distance}");

        isPlayerNear = distance <= proximityDistance;
        Debug.Log($"Is player near: {isPlayerNear} (Distance: {distance}, Proximity: {proximityDistance})");

        if (isPlayerNear)
        {
            uiImage.gameObject.SetActive(true);
            uiText.gameObject.SetActive(true); // Show text

            timer += Time.deltaTime;

            if (timer >= switchInterval)
            {
                // Switch the sprite and text
                if (uiImage.sprite == image1)
                {
                    uiImage.sprite = image2;
                   uiText.text = text1; // Update text
                }
                else
                {
                    uiImage.sprite = image1;
                    uiText.text = text1; // Update text
                }

                Debug.Log($"Switched to sprite: {uiImage.sprite.name}");
                Debug.Log($"Switched text to: {uiText.text}");
                timer = 0f;
            }

            // Check for X key press
            if (Input.GetKeyDown(KeyCode.X))
            {
                ToggleAdditionalCanvas();
            }
        }
        else
        {
            uiImage.gameObject.SetActive(false);
            uiText.gameObject.SetActive(false); // Hide text
            additionalCanvas.gameObject.SetActive(false); // Ensure additional Canvas is hidden when not near
            timer = 0f;
        }
    }

    void ToggleAdditionalCanvas()
    {
        // Toggle the active state of the additional Canvas
        if (additionalCanvas != null)
        {
            bool isActive = additionalCanvas.gameObject.activeSelf;
            additionalCanvas.gameObject.SetActive(!isActive); // Toggle visibility
            Debug.Log("Additional Canvas toggled to " + !isActive);
        }
    }
}
