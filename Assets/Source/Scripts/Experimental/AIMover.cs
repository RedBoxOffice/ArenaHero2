using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace ArenaHero.Experimental
{
	public class AIMover : MonoBehaviour
	{
		[SerializeField] private Transform _target; // Target position
		
		private NavMeshWorld _world; // all NavMeshData in Scene
		private NavMeshQuery _query; // ...
		
		private void Start()
		{
			_world = NavMeshWorld.GetDefaultWorld();
			_query = new NavMeshQuery(_world, Allocator.None);
		}

		private void Update()
		{
			// World position from Target To NavMesh = location in world
			NavMeshLocation location = _query.MapLocation(_target.position, Vector3.positiveInfinity, 0);
			
			Debug.Log($"target position = {_target.position}");
			Debug.Log($"location in NavMesh = {location.position}");
		}

		private void OnDisable()
		{
			_query.Dispose();
		}
	}
}