using System.Collections;
using UnityEngine;

namespace SpaceAttackers.Aliens
{
	public class GroupMovement : MonoBehaviour
	{
		private float _minBoundsX;
		private float _maxBoundsX;
		private float _currentDirection = RightDirection;
		private const float RightDirection = 0.5f;
		private const float LeftDirection = -RightDirection;
		private bool _groupMoving = true;
		private Camera _camera;
		private float CameraSize => _camera.orthographicSize * _camera.aspect;

		private void Awake()
		{
			CalculateCurrentBounds();
			_camera = Camera.main;
		}

		private void Start()
		{
			StartCoroutine(MovementCoroutine());
		}

		private IEnumerator MovementCoroutine()
		{
			while (_groupMoving)
			{
				yield return StartCoroutine(LerpMovement(new Vector3(_currentDirection, 0, 0)));

				switch (_currentDirection)
				{
					case RightDirection:
						_maxBoundsX = ClampToCameraSize(_maxBoundsX += _currentDirection);

						if (_maxBoundsX >= CameraSize)
						{
							FlipDirection();
							CalculateCurrentBounds();
							yield return StartCoroutine(LerpMoveDown());
						}

						break;
					case LeftDirection:
						_minBoundsX = ClampToCameraSize(_minBoundsX += _currentDirection);
						if (_minBoundsX <= -CameraSize)
						{
							FlipDirection();
							CalculateCurrentBounds();
							yield return StartCoroutine(LerpMoveDown());
						}

						break;
				}

				yield return new WaitForSeconds(0.5f);
			}
		}

		private IEnumerator LerpMoveDown()
		{
			yield return StartCoroutine(LerpMovement(new Vector3(0, -0.5f, 0)));
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

		private IEnumerator LerpMovement(Vector3 vector)
		{
			var lerpTime = 0f;
			var lerpPosition = transform.position + vector;
			while (lerpTime < 1)
			{
				var lerped = Vector3.Lerp(transform.position, lerpPosition, lerpTime);
				transform.position = lerped;
				lerpTime += Time.fixedDeltaTime * 1.5f;
				yield return new WaitForEndOfFrame();
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
	}
}