using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    float speed = 10;
    List<Sprite> directions = new List<Sprite>();
    bool inControl = false;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;

    public void InitializeAs(string entity, bool controls)
    {
        inControl = controls;
        if (entity == "Player")
        {
            this.gameObject.AddComponent<SpriteRenderer>();
            this.gameObject.AddComponent<Rigidbody2D>();

            directions.Add(Resources.Load<Sprite>("cubeDown"));
            directions.Add(Resources.Load<Sprite>("cubeUp"));
            directions.Add(Resources.Load<Sprite>("cubeLeftRight"));

            this.GetComponent<SpriteRenderer>().sprite = directions[0];
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void Update()
    {
        Rigidbody2D physics = this.GetComponent<Rigidbody2D>();
        Vector2 vel = new Vector2();

        if (!inControl)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            up = true;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            up = false;
        }

        if (up)
        {
            physics = GetComponent<Rigidbody2D>();

            vel.y += speed;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            down = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            down = false;
        }

        if (down)
        {
            physics = GetComponent<Rigidbody2D>();

            vel.y += -speed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
        }

        if (left)
        {
            physics = GetComponent<Rigidbody2D>();

            vel.x += -speed;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            right = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
        }

        if (right)
        {
            physics = GetComponent<Rigidbody2D>();

            vel.x += speed;
        }

        if (vel.x > 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = directions[2];
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (vel.x < 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = directions[2];
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (vel.x == 0)
        {
            if (vel.y > 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = directions[1];
            }
            if (vel.y < 0)
            {
                this.GetComponent<SpriteRenderer>().sprite = directions[0];
            }
        }

        physics.velocity = vel;
    }
}
