
/***********************************************************************************************************
 * Produced by App Advisory	- http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/


using UnityEngine;
using System.Collections;

namespace AppAdvisory.TunnelAndTwist
{
	public class MonoBehaviorHelper : MonoBehaviour
	{

		private GameManager _gameManager;
		public GameManager gameManager
		{
			get
			{
				if (_gameManager == null)
					_gameManager = FindObjectOfType<GameManager> ();

				return _gameManager;
			}
		}

		private Player _player;
		public Player player
		{
			get
			{
				if (_player == null)
					_player = FindObjectOfType<Player> ();

				return _player;
			}
		}

		private Transform _playerTransform;
		public Transform playerTransform
		{
			get
			{
				if (_playerTransform == null)
					_playerTransform = player.transform;

				return _playerTransform;
			}
		}

		private ObjectPooling _objectPooling;
		public ObjectPooling objectPooling
		{
			get
			{
				if (_objectPooling == null)
					_objectPooling = FindObjectOfType<ObjectPooling> ();

				return _objectPooling;
			}
		}


		private BackgroundAnim _backgroundAnim;
		public BackgroundAnim backgroundAnim
		{
			get
			{
				if (_backgroundAnim == null)
					_backgroundAnim = FindObjectOfType<BackgroundAnim> ();

				return _backgroundAnim;
			}
		}
	}
}