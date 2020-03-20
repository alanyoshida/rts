using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selector : MonoBehaviour
{
  public Camera Cam;
  private RaycastHit R;
  private GameObject[] selectedUnits;
  private Vector2 mouseStartPosition;
  private Vector2 mouseEndPosition;
  private Vector2 mouseInitialPosition;
  public Image selectionBackground;
  List<GameObject> selectedGameObjects;

    // Start is called before the first frame update
    void Start()
    {
      selectedUnits = GameObject.FindGameObjectsWithTag("ship");
      selectedGameObjects = new List<GameObject>();
    }

    void drawSelectionBox()
    {
      // when mouse button is down initialize the positions
      if (Input.GetMouseButtonDown(0))
      {
        mouseStartPosition = Input.mousePosition;
        mouseInitialPosition = Input.mousePosition;
      }

      if (Input.GetMouseButton(0))
      {
        // Control the if you draw from right,left or bottom, up
        if (Input.mousePosition.x >= mouseInitialPosition.x)
        {
          mouseEndPosition.x = Input.mousePosition.x;
        } else {
          mouseStartPosition.x = Input.mousePosition.x;
          mouseEndPosition.x = mouseInitialPosition.x;
        }

        if (Input.mousePosition.y >= mouseInitialPosition.y)
        {
          mouseEndPosition.y = Input.mousePosition.y;
        } else {
          mouseStartPosition.y = Input.mousePosition.y;
          mouseEndPosition.y = mouseInitialPosition.y;
        }


      // Set the image to the desired widths and heights
        selectionBackground.rectTransform.anchorMax =
          new Vector2(mouseEndPosition.x / Screen.width, mouseEndPosition.y / Screen.height);
        selectionBackground.rectTransform.anchorMin =
          new Vector2(mouseStartPosition.x / Screen.width, mouseStartPosition.y / Screen.height);
      }

      // when release the mouse it clear the image
      if (Input.GetMouseButtonUp(0))
      {
        selectionBackground.rectTransform.anchorMax = new Vector2(0, 0);
        selectionBackground.rectTransform.anchorMin = new Vector2(0, 0);
      }
    }

    void selectUnits()
    {
      if (Input.GetMouseButtonUp(0))
      {
        RaycastHit2D PosMinMouse2d = Physics2D.Raycast(Cam.ScreenToWorldPoint(mouseStartPosition), Vector2.zero);
        RaycastHit2D PosMaxMouse2d = Physics2D.Raycast(Cam.ScreenToWorldPoint(mouseEndPosition), Vector2.zero);

        List<GameObject> gameObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("ship"));

        foreach (var item in gameObjects)
        {
          if (item.transform.position.x > PosMinMouse2d.point.x &&
              item.transform.position.x < PosMaxMouse2d.point.x &&
              item.transform.position.y > PosMinMouse2d.point.y &&
              item.transform.position.y < PosMaxMouse2d.point.y)
          {
            item.GetComponent<SpriteRenderer>().color = Color.cyan;
            selectedGameObjects.Add(item);
          }
        }
      }
    }

    void moveGameObjects()
    {
      if (Input.GetMouseButtonDown(1))
      {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        // it has a collision
        if (hit.collider != null)
        {
          Vector3 troupDestination = hit.point;
          int matrixSize = (int) Math.Floor(Math.Sqrt( selectedGameObjects.Count));
          Vector3 unitDestination;
          foreach (var unit in selectedGameObjects)
          {
            if (unit)
            {
              unitDestination = troupDestination;
              // TODO: each ship have unique destinations
              unit.GetComponent<Ship>().selected = true;
              unit.GetComponent<Ship>().destination = unitDestination;

            }
          }
        }
      }
    }

    // Update is called once per frame
    void Update()
    {
      this.drawSelectionBox();
      this.selectUnits();
      this.moveGameObjects();
    }
}
