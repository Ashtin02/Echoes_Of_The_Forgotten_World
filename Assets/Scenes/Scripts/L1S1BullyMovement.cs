using UnityEngine;
using UnityEngine.SceneManagement;

public class BullyMovement : MonoBehaviour
{

   public GameObject Player; //Reference to the player
   public float chaseSpeed = 100f; //Reference to Speed of bullies
   public bool canMove = false; //Marker if they are able to move or not
   private Rigidbody2D rb; //Reference to rigidbody
   private Vector2 mov; //Stores movement
   public GameObject caughtScreen; //Refernece to caught screen


   void Start()
   {
      //Gets rigid body
      rb = GetComponent<Rigidbody2D>();
   }

   void Update()
   {

      //If moving is not allowed do nothing
      if (!canMove) return;

      //else move to the right
      mov.x = 1f;
   }

   void FixedUpdate()
   {
      //Moving is disabled to stop moving
      if (!canMove)
      {
         rb.linearVelocity = Vector2.zero;
         return;
      }

      //move to the right at the speed given
      rb.linearVelocity = new Vector2(mov.x * chaseSpeed, rb.linearVelocityY);
   }


   //Method to start moving the bullies
   public void StartMoving()
   {
      canMove = true;
   }


//Handles when bullies collide with player
   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         caughtScreen.SetActive(true); //shows caught screen 
         Time.timeScale = 0f; //pause game
         canMove = false;
      }
   }

//Method to reset the level
   public void RetryLevel()
   {
      SceneManager.LoadScene("Level 1 Scene 1");
      Time.timeScale = 1f;
   }
}
