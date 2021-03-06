﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	#region Constants
	private const float EXPLOSION_TIME_OUT = 0.5f;
	private const float EXPLOSION_UPDATE_FRAMES_DELTA = 5;
    private const string EXPLOSION = "Explosion";
	#endregion

	#region Variables
    private bool isExplode = false;
	private int speed = 5;
	private int frameCounter = 0;
	private SpriteRenderer spriteRenderer;
	#endregion

	public Sprite RegSprite;
	public Sprite ExplosionSprite;
	public GameObject Bullet;

	void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () 
	{
		if (Model.IsGamePaused) return;

		if (isExplode)
		{
			if (frameCounter%EXPLOSION_UPDATE_FRAMES_DELTA == 0)
				spriteRenderer.flipX = !spriteRenderer.flipX;
			frameCounter++;
			return;
		}
		
		if 	(Input.GetKey (KeyCode.RightArrow) && transform.position.x + spriteRenderer.bounds.size.x < Model.ScreenLimit.x)
			transform.Translate (Vector2.right * speed * Time.deltaTime);
		else if (Input.GetKey (KeyCode.LeftArrow)  && transform.position.x - spriteRenderer.bounds.size.x > -Model.ScreenLimit.x)
			transform.Translate (Vector2.left * speed * Time.deltaTime);
		
		if (Input.GetKeyDown (KeyCode.Space))
			Instantiate(Bullet, new Vector2 (transform.position.x, transform.position.y), Quaternion.identity);
	}	
	
	public void Explode ()
    {
		StartCoroutine(EXPLOSION);
    }

	private IEnumerator Explosion ()
    {
		isExplode = true;
        spriteRenderer.sprite = ExplosionSprite;

		yield return new WaitForSeconds(EXPLOSION_TIME_OUT);

		isExplode = false;
		spriteRenderer.sprite = RegSprite;		
		transform.position = Model.PLAYERS_START_POS;
    }
}