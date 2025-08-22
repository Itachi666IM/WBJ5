using UnityEngine;

public class WBall : MonoBehaviour
{
    Rigidbody2D rb;
    public float forceAmount;
    bool once = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!once)
            {
                rb.AddForce(Vector2.left * forceAmount);
                once = true;
            }
        }
    }
}
