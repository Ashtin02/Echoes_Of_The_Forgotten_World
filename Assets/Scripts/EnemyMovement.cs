using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float downwardSpeed = 0.5f;
    public Transform leftBoundary;
    public Transform rightBoundary;
    
    [Header("Descent Settings")]
    public float maxDescendY = 0f; 
    public float startY = 7f;
    public float stopDescendingY = 2f;
    
    [Header("Pattern Settings")]
    public float zigzagAmount = 0.5f;
    public float patternSpeed = 2f;
    
    private int direction = 1;
    private bool isDescending = true;
    private float patternOffset = 0f;
    
    /// <summary>
    /// Initialize the enemy's starting position and set it to the specified startY height.
    /// This is called once at the start of the game.
    /// </summary>
    void Start()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
    }
    
    /// <summary>
    /// Update is called once per frame.
    /// This method handles the enemy's movement in a zigzag pattern while descending.
    /// </summary>
    void Update()
{
    patternOffset += Time.deltaTime * patternSpeed;
    float zigzag = Mathf.Sin(patternOffset) * zigzagAmount;
    
    Vector3 movement = new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
    movement.y += zigzag * Time.deltaTime;
    
    transform.Translate(movement, Space.World);
    
    if (isDescending)
    {
        transform.Translate(Vector2.down * downwardSpeed * Time.deltaTime, Space.World);
        
        if (transform.position.y <= stopDescendingY)
        {
            isDescending = false;
        }
    }
    
    if (transform.position.y < maxDescendY)
    {
        ResetPosition();
    }
    
    if (transform.position.x >= rightBoundary.position.x && direction == 1)
    {
        direction = -1;
    }
    else if (transform.position.x <= leftBoundary.position.x && direction == -1)
    {
        direction = 1;
    }
}
    /// <summary>
    /// Resets the enemy's position to the starting Y height and sets it to descend again.
    /// This method is called when the enemy descends below the maxDescendY threshold.
    /// </summary>
    void ResetPosition()
    {
        float resetSpeed = 5f;
        transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(transform.position.x, startY, transform.position.z), 
            resetSpeed * Time.deltaTime);
        
        if (transform.position.y >= startY - 0.1f)
        {
            isDescending = true;
        }
    }
}