using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the movement of bullies that chase the player and trigger game over on contact.
/// </summary>
public class BullyMovement : MonoBehaviour
{
   public GameObject Player;
   public float chaseSpeed = 100f;
   public bool canMove = false; 
   private Rigidbody2D rb; 
   private Vector2 mov; 
   void Start()
   {
      rb = GetComponent<Rigidbody2D>();
   }
   /// <summary>
   /// Updates the movement direction of the bully to chase the player.
   /// </summary>
   void Update()
   {

      if (!canMove) return;

      mov.x = 1f;
   }
   /// <summary>
   /// Applies movement to the bully in FixedUpdate for consistent physics behavior.
   /// </summary>
   void FixedUpdate()
   {
      if (!canMove)
      {
         rb.linearVelocity = Vector2.zero;
         return;
      }
      rb.linearVelocity = new Vector2(mov.x * chaseSpeed, rb.linearVelocityY);
   }
   /// <summary>
   /// Enables the bully to start moving towards the player.
   /// </summary>
   public void StartMoving()
   {
      canMove = true;
   }
   /// <summary>
   /// Handles collision with the player, triggering a game over scene.
   /// </summary>
   /// <param name="collision"></param>
   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         SceneManager.LoadScene("GameOver");
         canMove = false;
      }
   }
}
