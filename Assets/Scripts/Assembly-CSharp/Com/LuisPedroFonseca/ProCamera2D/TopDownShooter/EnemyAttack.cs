using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class EnemyAttack : MonoBehaviour
	{
		public float RotationSpeed = 2f;

		public Pool BulletPool;

		public Transform WeaponTip;

		public float FireRate = 0.3f;

		public float FireAngleRandomness = 10f;

		private bool _hasTarget;

		private Transform _target;

		private NavMeshAgent _navMeshAgent;

		private Transform _transform;

		private void Awake()
		{
			_transform = base.transform;
			_navMeshAgent = GetComponentInChildren<NavMeshAgent>();
		}

		public void Attack(Transform target)
		{
			_navMeshAgent.updateRotation = false;
			_target = target;
			_hasTarget = true;
			StartCoroutine(LookAtTarget());
			StartCoroutine(FollowTarget());
			StartCoroutine(Fire());
		}

		public void StopAttack()
		{
			_navMeshAgent.updateRotation = true;
			_hasTarget = false;
		}

		private IEnumerator LookAtTarget()
		{
			while (_hasTarget)
			{
				Vector3 lookAtPos = new Vector3(_target.position.x, _transform.position.y, _target.position.z);
				Vector3 diff = lookAtPos - _transform.position;
				Quaternion newRotation = Quaternion.LookRotation(diff, Vector3.up);
				_transform.rotation = Quaternion.Slerp(_transform.rotation, newRotation, RotationSpeed * Time.deltaTime);
				yield return null;
			}
		}

		private IEnumerator FollowTarget()
		{
			while (_hasTarget)
			{
				Vector2 rnd = Random.insideUnitCircle;
				_navMeshAgent.destination = _target.position - (_target.position - _transform.position).normalized * 5f + new Vector3(rnd.x, 0f, rnd.y);
				yield return null;
			}
		}

		private IEnumerator Fire()
		{
			while (_hasTarget)
			{
				GameObject bullet = BulletPool.nextThing;
				bullet.transform.position = WeaponTip.position;
				bullet.transform.rotation = _transform.rotation * Quaternion.Euler(new Vector3(0f, -90f + Random.Range(0f - FireAngleRandomness, FireAngleRandomness), 0f));
				yield return new WaitForSeconds(FireRate);
			}
		}

		public static Vector2 RandomOnUnitCircle2(float radius)
		{
			Vector2 insideUnitCircle = Random.insideUnitCircle;
			insideUnitCircle.Normalize();
			return insideUnitCircle * radius;
		}
	}
}
