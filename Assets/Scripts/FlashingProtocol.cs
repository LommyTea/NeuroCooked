using System.Collections;
using UnityEngine;

public class MSequenceFlasherWithEnum : MonoBehaviour
{
    public enum MSequenceChoice
    {
        MSequence1,
        MSequence2,
        MSequence3
    }

    public MSequenceChoice selectedSequenceChoice;  // Dropdown in Inspector for choosing the m-sequence
    public SpriteRenderer spriteRenderer;  // The SpriteRenderer for the 2D object
    public float flashInterval = 0.1f;  // Interval between flashes
    public Color whiteColor = Color.white;  // Color for white
    public Color blackColor = Color.black;  // Color for black

    private int[] MSequence1 = new int[] { 1, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1 };  // Example base m-sequence
    private int[] MSequence2 = new int[] { 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0 };  // Lagged version 1
    private int[] MSequence3 = new int[] { 0, 1, 1, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1 };  // Lagged version 2
    private int[] selectedSequence;  // Currently selected sequence for flickering

    private bool isFlashing = false;

    void Start()
    {
        // Set the selected sequence based on the dropdown
        UpdateSelectedSequence();

        // Start flickering
        StartCoroutine(FlashMSequence());
    }

    void UpdateSelectedSequence()
    {
        switch (selectedSequenceChoice)
        {
            case MSequenceChoice.MSequence1:
                selectedSequence = MSequence1;
                break;
            case MSequenceChoice.MSequence2:
                selectedSequence = MSequence2;
                break;
            case MSequenceChoice.MSequence3:
                selectedSequence = MSequence3;
                break;
            default:
                selectedSequence = MSequence3;
                break;
        }
    }

    IEnumerator FlashMSequence()
    {
        isFlashing = true;

        float flashDuration = 60f;  // Flash duration in seconds
        float elapsedTime = 0f;
        int sequenceIndex = 0;

        while (elapsedTime < flashDuration)
        {
            if (selectedSequence == null || selectedSequence.Length == 0)
            {
                Debug.LogError("Selected sequence is not set or is empty.");
                break;
            }

            // Set the color based on the selected sequence value
            if (selectedSequence[sequenceIndex] == 1)
            {
                spriteRenderer.color = whiteColor;  // Set to white
            }
            else
            {
                spriteRenderer.color = blackColor;  // Set to black
            }

            yield return new WaitForSeconds(flashInterval);

            sequenceIndex = (sequenceIndex + 1) % selectedSequence.Length;  // Safely wrap around
            elapsedTime += flashInterval;
        }

        spriteRenderer.color = blackColor;  // Ensure the square is black after flashing
        isFlashing = false;
    }
}
