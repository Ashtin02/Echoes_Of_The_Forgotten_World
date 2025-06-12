using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    /// <summary>
    /// Handles the continue button click event.
    /// This method is called when the continue button is clicked.
    /// </summary>
    public void OnContinueClicked()
    {
        SceneManager.LoadScene("NextSceneName");

    }
}