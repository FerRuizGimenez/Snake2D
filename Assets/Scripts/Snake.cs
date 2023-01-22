using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right; //In order to keep track of our direction
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;

    public int initialSize = 4;

    private Vector2 input;
    public float speed = 20f;
    public float speedMultiplier = 1f;
    private float nextUpdate;

    public GameObject gameOverPanel;

    private void Start() 
    {
        ResetState();

    }
 
    private void Update() 
    {
        // Only allow turning up or down while moving in the x-axis
        if(_direction.x != 0f)
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                input = Vector2.up;
            } 
            else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                input = Vector2.down;
            } 
        }
        // Only allow turning left or right while moving in the y-axis
        else if(_direction.y != 0f)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                input = Vector2.left;
            }
            else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2.right;
            }
        }
        
    }

    private void FixedUpdate() 
    {
        // Wait until the next update before proceeding
        if(Time.time < nextUpdate)
        {
            return;
        }

        // Set the new direction based on the input
        if(input != Vector2.zero)
        {
            _direction = input;
        }

        // This makes sure that sort of each segment is following the one in front of it
        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        float x = Mathf.Round(transform.position.x) + _direction.x;
        float y = Mathf.Round(transform.position.y) + _direction.y;

        transform.position = new Vector2(x, y);
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
              
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment); 
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        gameOverPanel.SetActive(false);
        for(int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }   

        _segments.Clear();
        _segments.Add(this.transform);

        for(int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;   
    }

    public bool Occupies(float x, float y)
    {
        foreach(Transform segment in _segments)
        {
            if(segment.position.x == x && segment.position.y == y)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Food")
        {
            Grow();    
        }
        else if(other.tag == "Obstacle")
        {
            gameOverPanel.SetActive(true);
            this.gameObject.SetActive(false);

        }
    }
}
