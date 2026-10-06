using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    PlayerInput player;
    InputAction move;
    InputAction jump;
    Rigidbody rb;

    public bool onGround = true;
    bool gameOver = false;

    public float moveSpeed = 5.0f;
    public float jumpValue = 5.0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    float startingTime;
    float runningTime;
    bool activeTime = false;
    float minutes;
    float seconds;

    public GameObject GameOverPanel;

    int score;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerInput>();
        move = player.actions.FindAction("Move");
        jump = player.actions.FindAction("Jump");
        rb.freezeRotation = true;

        activeTime = true;
        startingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && transform.position.y <= -5.0f)
        {
            activeTime = false;
            gameOver = true;
            //FindAnyObjectByType<GameOver>().EndGame();
            GameOver();
        }

        if (activeTime)
        {
            runningTime = Time.time - startingTime;
            minutes = Mathf.FloorToInt(runningTime / 60);
            seconds = Mathf.FloorToInt(runningTime % 60);
            score++;
        }

        MovePlayer();
        PlayerJumps();

        scoreText.text = score.ToString();
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void MovePlayer()
    {

        Vector2 MoveDirection = move.ReadValue<Vector2>();
        Vector3 position = (transform.right * MoveDirection.x + transform.forward).normalized;
        rb.MovePosition(rb.position + position * moveSpeed * Time.deltaTime);
    }

    void PlayerJumps()
    {
        if (onGround && jump.IsPressed())
        {
            onGround = false;
            rb.AddForce(Vector3.up * jumpValue, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

        // Repetitive Code (will need improving)
        if (!gameOver && collision.gameObject.CompareTag("Obstacle"))
        {
            activeTime = false;
            gameOver = true;
            Time.timeScale = 0;
            //FindAnyObjectByType<GameOver>().EndGame();
            GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        FindAnyObjectByType<GroundSpawner>().SpawnGround();
    }

    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject, 5f);
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        //FindAnyObjectByType<GameOver>().EndGame();
    }

    public void RestartTheGame()
    {
        Time.timeScale = 1f;
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //FindAnyObjectByType<GameOver>().RestartGame();
    }
}