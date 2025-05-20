using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    float moveSpeed = 2f;
    int score = 0;

    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;
    [SerializeField] Sprite spriteRight;
    [SerializeField] Sprite spriteLeft;
    [SerializeField] TMP_Text scoreText;

    Rigidbody2D rb;
    SpriteRenderer sr;

    Vector2 input;
    Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Start()
    {
        scoreText.text = "Score: 0"; // 처음 점수 표시
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            score += collision.GetComponent<ItemObject>().GetPoint();
            scoreText.text = "Score: " + score; // 점수 UI 업데이트
            Destroy(collision.gameObject);
            Debug.Log("Score: " + score);
        }
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        velocity = input.normalized * moveSpeed;

        if (input.sqrMagnitude > 0.1f)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                    sr.sprite = spriteRight;
                else if (input.x < 0)
                    sr.sprite = spriteLeft;
            }
            else
            {
                if (input.y < 0)
                    sr.sprite = spriteUp;
                else
                    sr.sprite = spriteDown;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}