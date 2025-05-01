using UnityEngine;
using UnityEngine.SceneManagement;

public class BullyMovement : MonoBehaviour
{

   public GameObject Player;
   public float chaseSpeed = 100f;
   public bool canMove = false;
   private Rigidbody2D rb;
   private Vector2 mov;
   public GameObject caughtScreen;


   void Start()
   {
      rb = GetComponent<Rigidbody2D>();
   }

   void Update()
   {
      if (!canMove) return;
      mov.x = 1f;
   }

   void FixedUpdate()
   {
      if (!canMove)
      {
         rb.linearVelocity = Vector2.zero;
         return;
      }
      rb.linearVelocity = new Vector2(mov.x * chaseSpeed, rb.linearVelocityY);
   }

   public void StartMoving()
   {
      canMove = true;
   }


   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         caughtScreen.SetActive(true); //shows caught screen 
         Time.timeScale = 0f; //pause game
         canMove = false;
      }
   }

   public void RetryLevel()
   {
      SceneManager.LoadScene("Level 1 Scene 1");
      Time.timeScale = 1f;
    }
}
