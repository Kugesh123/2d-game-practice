using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float xValue;
    public Rigidbody2D rb;
    [SerializeField]int jumpPower;
    [SerializeField] TextMeshProUGUI scoreText;

    float scoreCounter = 0;
    int currentScene;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        transform.Translate(xValue, 0, 0);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            Debug.Log("You collected a coin");
            scoreCounter++; // Increase the score by 1
            scoreText.text = "Score: " + scoreCounter;
        }

        else if (other.gameObject.tag == "Spike")
        {
            ReloadScene();
            Debug.Log("You died by falling on the spike");
        }

        else if (other.gameObject.tag == "Diamond")
        {
            Destroy(other.gameObject);
            Debug.Log("You collected a diamond");
            scoreCounter += 5; // Increase the score by 5
            scoreText.text = "Score: " + scoreCounter;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            LoadNextScene();
        }

        else if (other.gameObject.CompareTag("Respawn"))
        {
            ReloadScene();
        }
    }

    void ReloadScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }

    void LoadNextScene()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);

    }
}
