using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 destination;
    public float speedRotate;
    public float speed;
    private float angle;
    public bool selected;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
      angle = 0;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetMouseButtonDown(1))
      {
        this.rotate();
      }
      if (selected)
      {
        this.move();
      }
    }

    void rotate()
    {
      Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      Vector3 lookAt = mouseScreenPosition;

      float AngleRad = Mathf.Atan2(lookAt.y - this.transform.position.y, lookAt.x - this.transform.position.x);

      float AngleDeg = (180 / Mathf.PI) * AngleRad;

      this.transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
    }

    void move()
    {
      // Move
      float step =  speed * Time.deltaTime; // calculate distance to move
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(destination.x, destination.y, destination.z), step);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
      return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
