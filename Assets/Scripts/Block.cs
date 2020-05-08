using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config parameters
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    
    [SerializeField] Sprite[] hitSprites;

    //[SerializeField] Level level; one option

    //cache reference
    Level level;

    //state variables
    [SerializeField] int timesHit; //TODO only serialised for debug purposes

    private void Start()
    {
        CountBreakableBlock();

    }

    private void CountBreakableBlock()
    {
        level = FindObjectOfType<Level>(); //looking for a type of Level

        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }

        Debug.Log(collision.gameObject.name); //will print out the name of whatever collided with the block - handy, telling you what caused the object to be destroyed

    }

    private void HandleHit()
    {

        timesHit++;

        int maxHits = hitSprites.Length + 1;

        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        // getting and changing the sprite

        // we render the sprite in Sprite Renderer -> Sprite

        int spriteIndex = timesHit - 1;

        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array:" + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();

        TriggerSparklesVFX();

        Destroy(gameObject, 0f);
        level.BlockDestroyed();
        //Debug.Log("here");

    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameSession>().AddToScore();

        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position); // will persist until sound played after object destroyed -> disposed of when block destroyed
                                                                                 //want to play next to the camera -> need to reference the camera's position
    }

    private void TriggerSparklesVFX()
    {

        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);

        UnityEngine.Object.Instantiate(sparkles);

        Destroy(sparkles, 2f);
    }
}
