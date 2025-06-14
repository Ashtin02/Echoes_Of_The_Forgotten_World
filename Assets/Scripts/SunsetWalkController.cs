using UnityEngine;
using System.Collections; 
using TMPro;

public class SunsetWalkController : MonoBehaviour
{
    [Header("Player Movement & Scaling")]
    public Vector3 startPosition = new Vector3(-1f, -6f, 0f); 
    public Vector3 endPosition = new Vector3(0.55f, -1.05f, 0f);

    public Vector3 startScale = new Vector3(5f, 5f, 1f);    
    public Vector3 endScale = new Vector3(0.35f, 0.35f, 1f); 

    public float walkDuration = 10f;
    public float initialDelay = 0.5f;

    [Header("Speed & Depth Curve")]
    public AnimationCurve positionCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public AnimationCurve scaleCurve = AnimationCurve.Linear(0, 0, 1, 1);

    [Header("Optional: Animator")]
    public Animator playerAnimator; 
    public string walkAnimationTrigger = "Walk";
    public string idleAnimationTrigger = "Idle";

    [Header("End Screen Text Settings")]
    public TMP_Text theEndText; 
    public float textFadeInDuration = 2f; 
    public float textFadeStartOffsetFromEnd = 1.5f; 
    private bool isWalking = false;
    private bool textFadeStarted = false;
    /// <summary>
    /// Initializes the player's position, scale, and starts the walking coroutine.
    /// </summary>
    void Start()
    {
        transform.position = startPosition;
        transform.localScale = startScale;

        if (theEndText != null)
        {
            Color textColor = theEndText.color;
            textColor.a = 0f;
            theEndText.color = textColor;
        }

        StartCoroutine(WalkIntoSunsetCoroutine());
    }
    /// <summary>
    /// Coroutine that moves and scales the player into the sunset while managing animations and text fade-in.
    /// </summary>
    /// <returns></returns>
    IEnumerator WalkIntoSunsetCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        isWalking = true;
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger(walkAnimationTrigger);
        }

        float timer = 0f;
        while (timer <= walkDuration)
        {
            if (!textFadeStarted && (walkDuration - timer) <= textFadeStartOffsetFromEnd)
            {
                if (theEndText != null)
                {
                    StartCoroutine(FadeInTheEndTextCoroutine(theEndText, textFadeInDuration));
                }
                textFadeStarted = true;
            }

            float t = Mathf.Clamp01(timer / walkDuration); 

            float curvedT = positionCurve.Evaluate(t);
            float curvedScaleT = scaleCurve.Evaluate(t);

            transform.position = Vector3.Lerp(startPosition, endPosition, curvedT);
            transform.localScale = Vector3.Lerp(startScale, endScale, curvedScaleT);

            yield return null;
            timer += Time.deltaTime;
        }

        transform.position = endPosition;
        transform.localScale = endScale;

        isWalking = false;
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger(idleAnimationTrigger);
        }

        Debug.Log("Player finished walking into sunset.");

        if (!textFadeStarted && theEndText != null)
        {
            StartCoroutine(FadeInTheEndTextCoroutine(theEndText, textFadeInDuration));
        }

    }
    /// <summary>
    /// Coroutine to fade in the "The End" text over a specified duration.
    /// </summary>
    /// <param name="textObject"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator FadeInTheEndTextCoroutine(TMP_Text textObject, float duration)
    {
        if (textObject == null) yield break; 

        Debug.Log("Starting to fade in 'The End' text.");
        Color startColor = textObject.color; 
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); 

        float fadeTimer = 0f;
        while (fadeTimer < duration)
        {
            fadeTimer += Time.deltaTime;
            float fadeT = fadeTimer / duration; 

            textObject.color = Color.Lerp(startColor, endColor, fadeT);
            yield return null;
        }
        textObject.color = endColor; 
        Debug.Log("'The End' text fully displayed.");
    }
}