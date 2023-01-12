using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right; //In order to keep track of our direction
    private List<Transform> _segments;
    public Transform segmentPrefab;

    private void Start() 
    {
        _segments = new List<Transform>(); // Initialize the list
        _segments.Add(this.transform); 

    }
 
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        } 
        else if(Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }   
        else if(Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }

    private void FixedUpdate() 
    {
        Debug.Log(_segments.Count);

        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );               
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Food")
        {
            Grow();    
        }
    }
}
