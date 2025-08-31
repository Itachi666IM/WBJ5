using UnityEngine;

public class Score : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ball")
        {
            gameManager.CalculateScore();
            Destroy(gameObject);
        }
    }
}
