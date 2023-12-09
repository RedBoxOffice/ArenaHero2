using UnityEngine;

namespace ArenaHero.Battle
{
	public class HealthView : MonoBehaviour
	{
		[SerializeField] private MeshRenderer _healthBarMeshRenderer;

		private readonly int _fill = Shader.PropertyToID("_Fill");
		
		private MaterialPropertyBlock _materialPropertyBlock;
		private ICharacter _character;
		private Camera _mainCamera;

		private void Awake()
		{
			_materialPropertyBlock = new MaterialPropertyBlock();
			_mainCamera = Camera.main;
			_character = GetComponent<ICharacter>();
			
			OnHealthChanged(_character.Data.MaxHealth);
		}

		private void OnEnable() =>
			_character.HealthChanged += OnHealthChanged;

		private void OnDisable() =>
			_character.HealthChanged -= OnHealthChanged;

		private void FixedUpdate() =>
			AlignCamera();

		private void OnHealthChanged(float currentHealth)
		{
			_healthBarMeshRenderer.GetPropertyBlock(_materialPropertyBlock);
			_materialPropertyBlock.SetFloat(_fill, currentHealth / _character.Data.MaxHealth);
			_healthBarMeshRenderer.SetPropertyBlock(_materialPropertyBlock);
		}

		private void AlignCamera()
		{
			var mainCameraTransform = _mainCamera.transform;
			var forward = _healthBarMeshRenderer.transform.position - mainCameraTransform.position;
			forward.Normalize();
			var up = Vector3.Cross(forward, mainCameraTransform.right);
			_healthBarMeshRenderer.transform.rotation = Quaternion.LookRotation(forward, up);
		}
	}
}