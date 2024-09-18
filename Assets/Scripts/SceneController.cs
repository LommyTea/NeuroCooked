using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneController : MonoBehaviour
{
    public Button loadNextSceneButton;  // Button to trigger the scene change

    void Start()
    {
        // Ensure the button is set in the Inspector, then add the listener
        if (loadNextSceneButton != null)
        {
            loadNextSceneButton.onClick.AddListener(LoadNextScene);
        }
        else
        {
            Debug.LogError("No button assigned to the loadNextSceneButton variable.");
        }
    }

    void LoadNextScene()
    {
        // Load the next scene in the build settings order
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene index is valid (within the build settings)
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogError("No more scenes to load. Make sure you have added scenes to the build settings.");
        }
    }

}
