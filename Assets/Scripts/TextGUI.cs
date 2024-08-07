using UnityEngine;
using TMPro;  // Include this if using TextMeshPro

public class TextGUI : MonoBehaviour
{
    public TextMeshProUGUI upperText;
    public TextMeshProUGUI lowerText;            // Text component to show messages
    public string text1 = "Welcome to Billboard A"; // Default text message
    public string text2 = "Welcome to Billboard A"; // Default text message
    public float proximityDistance = 15f;     // Distance within which to show the text

    private GameObject player;                // Reference to the player GameObject

    void Start()
    {
        // Find the player GameObject with the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Check if the player GameObject is found
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged 'Player'.");
            return;
        }

        // Check if the TextMeshProUGUI component is assigned
        if (upperText == null && lowerText == null)
        {
            Debug.LogError("UI Text is not assigned!");
            return;
        }

        // Initially hide the text
        upperText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Return early if player or uiText is not available
        if (player == null || upperText == null || lowerText == null) return;

        // Calculate the distance between the player and the billboard
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Check if the player is within proximity distance
        bool isPlayerNear = distance <= proximityDistance;

        // Show or hide the text based on player proximity
        upperText.gameObject.SetActive(isPlayerNear);
        lowerText.gameObject.SetActive(isPlayerNear);

        // If the player is near, update the text message
        if (isPlayerNear)
        {
            upperText.text = text1;
            lowerText.text = text2;
        }
    }
}
