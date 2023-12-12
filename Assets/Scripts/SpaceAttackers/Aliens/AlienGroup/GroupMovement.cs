using System.Collections;
using System.Linq;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Aliens.AlienGroup
{
	public class GroupMovement : MonoBehaviour
	{
		private float _minBoundsX;
		private float _maxBoundsX;
		private float _currentDirection = RightDirection;
		private const float RightDirection = 0.5f;
		private const float LeftDirection = -RightDirection;
		private bool _groupMoving;
		private Camera _camera;
		private float CameraSize => _camera.orthographicSize * _camera.aspect;
		private bool _coroutineStopped;
		private GameObject[] _verticalRows;
		private static readonly Vector3 StartingPosition = new(0, 2, 0);

		private void Awake()
		{
			CalculateCurrentBounds();
			_camera = Camera.main;
			_verticalRows = transform.Cast<Transform>().Select(child => child.gameObject)
				.ToArray();
		}

		private void Start()
		{
			StartCoroutine(MovementCoroutine());
		}

		private IEnumerator MovementCoroutine()
		{
			_groupMoving = true;
			_coroutineStopped = false;
			var position = StartingPosition;
			transform.position = StartingPosition;
			foreach (var verticalRow in _verticalRows)
			{
				verticalRow.SetActive(true);
			}

			while (transform.position != StartingPosition)
			{
				transform.position = StartingPosition;
				yield return new WaitForEndOfFrame();
			}
			CalculateCurrentBounds();


			while (_groupMoving)
			{
				while (PauseManager.Singleton.IsPaused)
				{
					yield return new WaitForEndOfFrame();
					if (_groupMoving == false)
					{
						break;
					}
				}

				if (_groupMoving == false)
				{
					continue;
				}

				yield return StartCoroutine(LerpMovement(new Vector3(_currentDirection, 0, 0), position));
				position.x += _currentDirection;

				switch (_currentDirection)
				{
					case RightDirection:
						_maxBoundsX = ClampToCameraSize(_maxBoundsX += _currentDirection);

						if (_maxBoundsX >= CameraSize)
						{
							FlipDirection();
							CalculateCurrentBounds();
							yield return StartCoroutine(LerpMoveDown(position));
							position.y += -0.5f;
						}

						break;
					case LeftDirection:
						_minBoundsX = ClampToCameraSize(_minBoundsX += _currentDirection);
						if (_minBoundsX <= -CameraSize)
						{
							FlipDirection();
							CalculateCurrentBounds();
							yield return StartCoroutine(LerpMoveDown(position));
							position.y += -0.5f;
						}

						break;
				}

				yield return new WaitForSeconds(0.5f);
			}

			_coroutineStopped = true;
		}

		private IEnumerator LerpMoveDown(Vector3 startingPosition)
		{
			yield return StartCoroutine(LerpMovement(new Vector3(0, -0.5f, 0), startingPosition));
		}

		private float ClampToCameraSize(float number)
		{
			return Mathf.Clamp(number, -CameraSize, CameraSize);
		}

		private void FlipDirection()
		{
			_currentDirection = _currentDirection switch
			{
				RightDirection => LeftDirection,
				LeftDirection => RightDirection,
				_ => _currentDirection
			};
		}

		private IEnumerator LerpMovement(Vector3 vector, Vector3 startingPosition)
		{
			while (transform.position != startingPosition)
			{
				transform.position = startingPosition;
				yield return new WaitForEndOfFrame();
			}

			var lerpTime = 0f;
			var lerpPosition = transform.position + vector;
			while (lerpTime < 1)
			{
				var lerped = Vector3.Lerp(transform.position, lerpPosition, lerpTime);
				transform.position = lerped;
				lerpTime += Time.fixedDeltaTime * 1.5f;
				yield return new WaitForFixedUpdate();
			}
		}

		public void CalculateCurrentBounds()
		{
			var maxBounds = GetMaxBounds();
			_minBoundsX = maxBounds.min.x;
			_maxBoundsX = maxBounds.max.x;
		}

		private Bounds GetMaxBounds()
		{
			var renderers = gameObject.GetComponentsInChildren<Renderer>();

			if (renderers.Length == 0)
			{
				return new Bounds(gameObject.transform.position, Vector3.zero);
			}

			var bounds = renderers[0].bounds;

			foreach (var renderer in renderers)
			{
				bounds.Encapsulate(renderer.bounds);
			}

			return bounds;
		}

		public IEnumerator StopMovement()
		{
			PauseManager.Singleton.PauseGame();
			_groupMoving = false;
			while (!_coroutineStopped)
			{
				yield return new WaitForEndOfFrame();
			}
		}

		public IEnumerator StartMovement()
		{
			_currentDirection = RightDirection;
			while (!_coroutineStopped)
			{
				yield return new WaitForEndOfFrame();
			}

			StartCoroutine(MovementCoroutine());
		}
	}
}