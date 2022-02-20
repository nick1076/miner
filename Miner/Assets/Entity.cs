using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    float speed = 1.75f;
    List<Sprite> directions = new List<Sprite>();
    bool inControl = false;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;
    bool grabable = false;

    List<Entity> hoveredEntity = new List<Entity>();
    GameObject grabbedEntity;

    public string ent = "NONASSIGNED";

    public void InitializeAs(string entity, bool controls)
    {
        ent = entity;
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
            this.gameObject.AddComponent<BoxCollider2D>();
        }
        else if (entity == "Pot")
        {
            this.gameObject.AddComponent<SpriteRenderer>();
            this.gameObject.AddComponent<Rigidbody2D>();

            directions.Add(Resources.Load<Sprite>("pot"));

            this.GetComponent<SpriteRenderer>().sprite = directions[0];
            this.GetComponent<SpriteRenderer>().sortingOrder = 1;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.gameObject.AddComponent<BoxCollider2D>();
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (entity == "Item_Red")
        {
            this.gameObject.AddComponent<SpriteRenderer>();

            directions.Add(Resources.Load<Sprite>("item"));

            this.GetComponent<SpriteRenderer>().sprite = directions[0];
            this.GetComponent<SpriteRenderer>().sortingOrder = 1;
            this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            this.gameObject.AddComponent<BoxCollider2D>();
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            grabable = true;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            print(1);
            if (grabbedEntity == null)
            {
                print(2);
                if (hoveredEntity != null)
                {
                    print(3);
                    if (hoveredEntity[0].grabable == true)
                    {
                        print(4);
                        //Pickup
                        grabbedEntity = hoveredEntity[0].gameObject;
                        grabbedEntity.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + .25f, 1);
                        grabbedEntity.transform.parent = this.gameObject.transform;
                    }
                }
            }
            else
            {
                print(5);
                //Drop
                grabbedEntity.transform.position = new Vector3(grabbedEntity.transform.position.x, grabbedEntity.transform.position.y - .25f, 1);
                grabbedEntity.transform.parent = null;
                hoveredEntity.Remove(grabbedEntity.GetComponent<Entity>());
                grabbedEntity = null;
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() != null && !hoveredEntity.Contains(collision.gameObject.GetComponent<Entity>()))
        {
            hoveredEntity.Add(collision.gameObject.GetComponent<Entity>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hoveredEntity.Contains(collision.gameObject.GetComponent<Entity>()))
        {
            hoveredEntity.Remove(collision.gameObject.GetComponent<Entity>());
        }
    }
}
