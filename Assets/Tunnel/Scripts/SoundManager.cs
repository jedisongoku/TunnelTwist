
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System;
using System.Collections;


namespace AppAdvisory.TunnelAndTwist
{
	public class SoundManager : MonoBehaviorHelper
	{

		public bool playMusicWhenPlayerClickStart = false;

		[SerializeField] private AudioClip musicIntro;
		[SerializeField] private AudioClip musicIntroToGame;
		[SerializeField] private AudioClip musicGame;

		[SerializeField] private AudioClip soundJump;
		[SerializeField] private AudioClip soundFail;
		[SerializeField] private AudioClip soundCoin;


		public AudioSource audioSourceMusic;
		[SerializeField] private AudioSource audioSourceFX;
		[SerializeField] private AudioSource audioSourceFXPoint;

		AudioClip currentMusic = null;

		void OnEnable()
		{
			EventManager.OnMotionBlur += SetSoundPitch;
			EventManager.OnSetComboCount += OnSetComboCount;
			InputTouch.OnTouched += OnTouched;
			EventManager.OnGetPointEvent += PlaySoundCoin;
			EventManager.OnAnimFailEvent += OnAnimFailEvent;
			EventManager.OnPlayerJumpEvent += PlaySoundJump;
			EventManager.OnPlayerStartEvent += OnPlayerStartEvent;
		}


		void OnDisable()
		{
			EventManager.OnMotionBlur -= SetSoundPitch;
			EventManager.OnSetComboCount -= OnSetComboCount;
			EventManager.OnGetPointEvent -= PlaySoundCoin;
			EventManager.OnAnimFailEvent -= OnAnimFailEvent;
			EventManager.OnPlayerJumpEvent -= PlaySoundJump;
			EventManager.OnPlayerStartEvent -= OnPlayerStartEvent;
		}

		void OnPlayerStartEvent ()
		{
			if(playMusicWhenPlayerClickStart);
			_PlayMusic();
		}

		void OnTouched (TouchDirection td)
		{
			InputTouch.OnTouched -= OnTouched;

			if(musicIntro == null || musicIntroToGame == null)
				return;

			PlayMusic (musicIntroToGame, () => {
				PlayMusic (musicGame);
			});
		}


		void Start()
		{
			audioSourceMusic.volume = 1;
			audioSourceFX.volume = 1;
			audioSourceFXPoint.volume = 1;

			if(!playMusicWhenPlayerClickStart)
			{
				_PlayMusic();
			}
		}

		void _PlayMusic()
		{
			if(musicIntro == null || musicIntroToGame == null)
			{
				PlayMusic(musicGame);
				return;
			}

			PlayMusic (musicIntro);
		}

		void OnAnimFailEvent(AnimFail g)
		{
			if(g == AnimFail.start)
			{
				EventManager.OnAnimFailEvent -= OnAnimFailEvent;
				PlaySoundGameOver();
			}
		}


		private void PlayMusic(AudioClip nextMusic)
		{
			PlayMusic(nextMusic,null);
		}

		private void PlayMusic(AudioClip nextMusic, Action isCompleted)
		{
			this.currentMusic = nextMusic;
			audioSourceMusic.clip = this.currentMusic;

			if(musicIntro == null || musicIntroToGame == null)
			{
				audioSourceMusic.loop = true;
				audioSourceMusic.Play ();
				return;
			}
			else
			{
				audioSourceMusic.loop = false;
			}

			StopAllCoroutines();
			StartCoroutine(WaitForMusicFinished(isCompleted));
		}

		IEnumerator WaitForMusicFinished(Action isCompleted)
		{
			if(!audioSourceMusic.isPlaying)
				audioSourceMusic.Play ();

			while(audioSourceMusic.isPlaying)
			{
				yield return 0;
			}

			if(currentMusic != null)
				PlayMusic(currentMusic);

			if(isCompleted != null)
				isCompleted();
		}

		private void PlaySoundGameOver()
		{
			audioSourceFX.PlayOneShot (soundFail, 0.5f);
		}

		private void PlaySoundCoin()
		{
			SetSoundPitch ();
			audioSourceFXPoint.PlayOneShot (soundCoin, 0.5f);
		}

		private void PlaySoundJump()
		{
			audioSourceFX.PlayOneShot (soundJump,0.5f);
		}

		private void SetSoundPitch(float f, bool enabled)
		{

			audioSourceMusic.volume = Time.timeScale;


			audioSourceFX.pitch = Time.timeScale;

			if(Time.timeScale < 0.98f)
			{
				audioSourceFXPoint.pitch = Time.timeScale;
			}
			else
			{
				audioSourceFXPoint.pitch = Time.timeScale + GetPitchPoint();
			}

			audioSourceMusic.pitch = Time.timeScale;
		}

		float comboCount;

		void OnSetComboCount(int comboCount)
		{
			this.comboCount = comboCount;	
		}

		private void SetSoundPitch()
		{
			audioSourceFX.pitch = Time.timeScale;

			if(Time.timeScale < 0.98f)
				audioSourceFXPoint.pitch = Time.timeScale;
			else
				audioSourceFXPoint.pitch = Time.timeScale + GetPitchPoint();

			audioSourceMusic.pitch = Time.timeScale;
		}

		private float GetPitchPoint()
		{

			float temp = (float)comboCount;

			return temp / 5f;
		}
	}
}