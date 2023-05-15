using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlayerController : MonoBehaviour
{

#region Variables
    public int lives;
    public int points;
    public float timeToMove= 0.2f; // Movement speed ( Lower value = faster )
    public bool isMoving; // Making sure the player only does 1 movement at a time
    private bool hasWall;
    private Vector2 origPos, targetPos;
    private Vector2 spawnPos;
#endregion

#region  Start and Update
    void Start()
    {
        points = 0;
        spawnPos = transform.position;
    }

    void Update()
    {   
        if(lives > 0)
            MovePC();

        // If the player runs out of lives, game over
        if(lives <= 0)
            GameOver();
    }
#endregion

#region  Movement PC Controls
    void MovePC()
    {      
        if(Input.GetKeyDown(KeyCode.UpArrow) && !isMoving) // Up
            StartCoroutine(MovePlayer(Vector2.up));         
        if(Input.GetKeyDown(KeyCode.DownArrow) && !isMoving) // Down
            StartCoroutine(MovePlayer(Vector2.down));
        if(Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving) // Left
            StartCoroutine(MovePlayer(Vector2.left));   
        if(Input.GetKeyDown(KeyCode.RightArrow) && !isMoving) // Right
            StartCoroutine(MovePlayer(Vector2.right));                 
    }
#endregion

#region Movement Mobile Controls
    public void MoveMobileUp()
    {
        if(lives > 0)
            StartCoroutine(MovePlayer(Vector2.up));
    }
    public void MoveMobileDown()
    {
        if(lives > 0)
            StartCoroutine(MovePlayer(Vector2.down));
    }
    public void MoveMobileLeft()
    {
        if(lives > 0)
            StartCoroutine(MovePlayer(Vector2.left));
    }
    public void MoveMobileRight()
    {
        if(lives > 0)
            StartCoroutine(MovePlayer(Vector2.right)); 
    }
#endregion

#region Movement    
    private IEnumerator MovePlayer(Vector2 direction)
    {
        while(hasWall == false)
        {
            isMoving = true;

            float elapsedTime = 0;

            // Grab initial position and target position
            origPos = transform.position;
            targetPos = origPos + direction;

            // Check if there is a wall at that position
            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.up, 0f);
            if (hit.collider != null && hit.transform.tag == "Wall") // It does not stop for traps         
                hasWall = true;

            if(hasWall == false)
            {
                // Move the player within set time
                while(elapsedTime < timeToMove)
                {
                    transform.position = Vector2.Lerp(origPos,targetPos,(elapsedTime/ timeToMove));
                    elapsedTime += Time.deltaTime;

                    yield return null;
                }
            
                // At the end of the movement make sure the player is in the target position
                transform.position = targetPos;
            }

            isMoving = false;          
        }

        hasWall = false;
    }
#endregion

#region Collisions
    void OnCollisionEnter2D(Collision2D other)
    {   
        // If the player collides with a trap, the player dies
        if(other.transform.tag == "Lethal")
            Death();   

        // If the player collides with a points object the player recieves a point
        
        if(other.transform.tag == "Points")
        {
            points++;

            // Get the tilemap and grid for the collision
            Tilemap tilemap = other.transform.GetComponent<Tilemap>();
            Grid grid = tilemap.layoutGrid;
            
            // Reset position to avoid incorrect position
            Vector3 hitPosition = Vector3.zero;

            // Get the tile position from the contact point
            foreach (ContactPoint2D hit in other.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            } 
        }                 
    }

#endregion

#region Death and GameOver
    void Death()
    {
        isMoving = false;
        StopAllCoroutines();
        // Put the player in the beggining of the level, and remove 1 life
        transform.position = spawnPos;
        lives --;
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
#endregion

}
