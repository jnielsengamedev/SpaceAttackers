using System;
using UnityEngine;

namespace SpaceAttackers.Player
{
	[RequireComponent(typeof(Input))]
	public class Movement : MonoBehaviour
	{
		[SerializeField] private float moveSpeed;
		private Input _input;

		private void Awake()
		{
			_input = GetComponent<Input>();
		}

		private void Update()
		{
			transform.Translate(Vector3.right * (_input.HorizontalInput * moveSpeed * Time.deltaTime));
		}
	}
}