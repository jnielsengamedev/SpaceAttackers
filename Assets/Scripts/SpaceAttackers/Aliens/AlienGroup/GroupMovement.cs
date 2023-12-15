using System.Collections;
using System.Linq;
using SpaceAttackers.GameManager;
using UnityEngine;

namespace SpaceAttackers.Aliens.AlienGroup
{
	public class GroupMovement : MonoBehaviour
	{
		private float _currentDirection = RightDirection;
		private const float RightDirection = 0.5f;
		private const float LeftDirection = -RightDirection;
		private const float DownDirection = -RightDirection;
		private bool _groupMoving;
		private float CameraSize => camera.orthographicSize * camera.aspect;
		private bool _coroutineStopped;
		private GameObject[] _verticalRows;
		private static readonly Vector3 StartingPosition = new(0, 2, 0);
		private float _currentSpeed = 1;
		[SerializeField] private Collider playerCollider;
		[SerializeField] private Player.Lives playerLives;
		[SerializeField] private new Camera camera;

		private void Awake()
		{
			CalculateCurrentBounds();
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

			var boundsX = CalculateCurrentBounds();

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
					break;
				}

				if (GetMaxBounds().Intersects(playerCollider.bounds) && _verticalRows.Any(row => row.activeInHierarchy))
				{
					playerLives.PlayerBoom();
					continue;
				}

				yield return StartCoroutine(LerpMovement(new Vector3(_currentDirection, 0, 0), position));
				position.x += _currentDirection;

				switch (_currentDirection)
				{
					case RightDirection:
						boundsX.Max = ClampToCameraSize(boundsX.Max += _currentDirection);

						if (boundsX.Max >= CameraSize)
						{
							FlipDirection();
							boundsX = CalculateCurrentBounds();
							yield return StartCoroutine(LerpMoveDown(position));
							position.y += -0.5f;
						}

						break;
					case LeftDirection:
						boundsX.Min = ClampToCameraSize(boundsX.Min += _currentDirection);
						if (boundsX.Min <= -CameraSize)
						{
							FlipDirection();
							boundsX = CalculateCurrentBounds();
							yield return StartCoroutine(LerpMoveDown(position));
							position.y += -0.5f;
						}

						break;
				}

				yield return new WaitForSeconds(0.5f / _currentSpeed);
			}

			_coroutineStopped = true;
		}

		private IEnumerator LerpMoveDown(Vector3 startingPosition)
		{
			yield return StartCoroutine(LerpMovement(new Vector3(0, DownDirection, 0), startingPosition));
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

		public BoundsX CalculateCurrentBounds()
		{
			var maxBounds = GetMaxBounds();
			return new BoundsX(maxBounds.min.x, maxBounds.max.x);
		}

		private Bounds GetMaxBounds()
		{
			var renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

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

		public void AddSpeed()
		{
			_currentSpeed = Mathf.Clamp(_currentSpeed += 0.1f, 0, 2);
		}
	}

	public struct BoundsX
	{
		public float Min;
		public float Max;

		public BoundsX(float min, float max)
		{
			Min = min;
			Max = max;
		}
	}
}