using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MSequenceFlasher2D : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // SpriteRenderer of the 2D square
    public Text[] textsToHide;  // Array of Text components to hide
    public int[] mSequence;  // M-sequence array
    public float flashInterval = 0.1f;  // Interval between flashes (in seconds)
    public Color whiteColor = Color.white;  // Color for white
    public Color blackColor = Color.black;  // Color for black

    private bool isFlashing = false;

    void Start()
    {
        // Ensure mSequence is initialized
        if (mSequence == null || mSequence.Length == 0)
        {
            Debug.LogWarning("M-sequence array is not set or is empty.");
            mSequence = new int[] { 1, 0, 1, 0, 1, 1, 0, 1, 0, 1 };
        }

        // Start the flashing sequence
        StartCoroutine(FlashMSequence());
    }

    IEnumerator FlashMSequence()
    {
        isFlashing = true;

        float flashDuration = 60f;  // Flash duration in seconds
        float elapsedTime = 0f;
        int sequenceIndex = 0;

        // Hide text components
        foreach (Text text in textsToHide)
        {
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }
        }

        while (elapsedTime < flashDuration)
        {
            if (mSequence.Length == 0)
            {
                Debug.LogError("M-sequence array is empty.");
                break;
            }

            // Set the color based on the mSequence value
            if (mSequence[sequenceIndex] == 1)
            {
                spriteRenderer.color = whiteColor;  // Set to white color
            }
            else
            {
                spriteRenderer.color = blackColor;  // Set to black color
            }

            yield return new WaitForSeconds(flashInterval);

            sequenceIndex = (sequenceIndex + 1) % mSequence.Length;  // Safely wrap around
            elapsedTime += flashInterval;
        }

        spriteRenderer.color = blackColor;  // Ensure the square is black after flashing

        // Re-enable text components
        foreach (Text text in textsToHide)
        {
            if (text != null)
            {
                text.gameObject.SetActive(true);
            }
        }

        isFlashing = false;
    }
}
