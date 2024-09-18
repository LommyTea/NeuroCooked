using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HideAndFlashOnButtonClick : MonoBehaviour
{
    public Button targetButton;  // Button to hide
    public TextMeshPro buttonText;  // Text inside the button (TextMeshPro)
    public TextMeshPro textBox;  // Separate TextMeshPro text box to hide

    public Renderer objectRenderer;  // The object to flash
    public float flashInterval = 0.5f;  // Interval between flashes
    private bool isFlashing = false;

    void Start()
    {
        // Assign the button click event to HideUIElements and StartFlashing
        targetButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Hide UI elements when the button is clicked
        HideUIElements();

        // Start the flashing sequence
        if (!isFlashing)
        {
            StartFlashing();
        }
    }

    void HideUIElements()
    {
        if (targetButton != null)
        {
            targetButton.gameObject.SetActive(false);  // Hide the button
        }

        if (buttonText != null)
        {
            buttonText.gameObject.SetActive(false);  // Hide the text inside the button
        }

        if (textBox != null)
        {
            textBox.gameObject.SetActive(false);  // Hide the separate text box
        }
    }

    void StartFlashing()
    {
        isFlashing = true;
        StartCoroutine(FlashObject());
    }

    IEnumerator FlashObject()
    {
        while (isFlashing)
        {
            // Toggle visibility of the object
            objectRenderer.enabled = !objectRenderer.enabled;

            // Wait for the next flash interval
            yield return new WaitForSeconds(flashInterval);
        }
    }

    public void StopFlashing()
    {
        isFlashing = false;
        StopCoroutine(FlashObject());
    }
}


