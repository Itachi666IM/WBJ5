using UnityEngine;

public class WBall : MonoBehaviour
{
    [HideInInspector]public Rigidbody2D rb;
    public float forceAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Wreck()
    {
        rb.AddForce(Vector2.left * forceAmount);
    }
}
