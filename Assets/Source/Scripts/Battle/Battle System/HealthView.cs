using System;
using System.Collections;
using ArenaHero.Battle.CharacteristicHolders;
using UnityEngine;

namespace ArenaHero.Battle
{
	[RequireComponent(typeof(Character))]
	public class HealthView : MonoBehaviour
	{
		[SerializeField] private MeshRenderer _healthBarMeshRenderer;

		private readonly int _fill = Shader.PropertyToID("_Fill");

		private MaterialPropertyBlock _materialPropertyBlock;
		private Character _character;
		private Camera _mainCamera;
		private Coroutine _alignCameraCoroutine;

		private void Awake()
		{
			_materialPropertyBlock = new MaterialPropertyBlock();
			
			_character = GetComponent<Character>();

			if (_character is null)
			{
				throw new NullReferenceException(nameof(Character));
			}
		}

		private void OnEnable()
		{
			_character.HealthChanged += OnHealthChanged;
			OnHealthChanged(_character.MaxHealth);

			_alignCameraCoroutine = StartCoroutine(AlignCamera());
		}

		private void OnDisable()
		{
			_character.HealthChanged -= OnHealthChanged;
			StopCoroutine(_alignCameraCoroutine);
			_mainCamera = null;
		}

		private void OnHealthChanged(float currentHealth)
		{
			_healthBarMeshRenderer.GetPropertyBlock(_materialPropertyBlock);
			_materialPropertyBlock.SetFloat(_fill, currentHealth / _character.MaxHealth);
			_healthBarMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
		}

		private IEnumerator AlignCamera()
		{
			_mainCamera ??= Camera.main;
			
			while (enabled)
			{
				var mainCameraTransform = _mainCamera.transform;
				var forward = _healthBarMeshRenderer.transform.position - mainCameraTransform.position;
				forward.Normalize();
				var up = Vector3.Cross(forward, mainCameraTransform.right);
				_healthBarMeshRenderer.transform.rotation = Quaternion.LookRotation(forward, up);

				yield return new WaitForFixedUpdate();
			}
		}
	}
}