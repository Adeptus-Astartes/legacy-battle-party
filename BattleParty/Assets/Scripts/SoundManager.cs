using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;


public class SoundManager : MonoBehaviour
{
	public AudioSource clickSound;
	public AudioSource battleStartSound;
	public AudioSource booster;
	public AudioSource winSound;
	public AudioSource defSound;

	public List<AudioSource> backgroundMusic;

	public void Start()
	{
		battleStartSound.Play ();
		//battleStart = true;
		battleBackgroundMusic = true;
	}

	public void Click()
	{
		clickSound.Play ();
	}
	public void UseBooster()
	{
		booster.Play ();
	}
	public void BattleStart()
	{
		battleStartSound.Play ();
	}
	public void WinSound()
	{
		winSound.Play ();
	}
	public void DefSound()
	{
		defSound.Play ();
	}
	public float backgoundDelay = 3;
	public int currentTrack = 0;
	private bool battleStart = true;
	private bool battleBackgroundMusic = false;

	public float trackLenght;
	public float currentLengh = 0;

	public void Pause()
	{
		backgroundMusic [currentTrack].Pause();
	}
	public void Resume()
	{
		backgroundMusic [currentTrack].UnPause();
	}

	public void FixedUpdate()
	{
		if(battleBackgroundMusic)
		{
			currentLengh = 0;
			currentTrack = Random.Range (0,backgroundMusic.Count);
			trackLenght = backgroundMusic [currentTrack].clip.length;
			backgroundMusic [currentTrack].Play ();
			battleBackgroundMusic = false;
		}
		if (currentLengh < trackLenght)
			currentLengh += Time.fixedDeltaTime;
		else
			battleBackgroundMusic = true;
	}
}
