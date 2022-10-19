using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using Tools;
using Player;

namespace AI
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private Transform npcCharacter;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Rigidbody money;
        [SerializeField] private float forwardTarget;
        [SerializeField] private float xAxisMax;
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMoveController>())
            {
                ThrowMoney();
                GetComponent<Collider>().enabled = false;
            }
        }

        private void ThrowMoney()
        {
            float forward = transform.position.z + forwardTarget;
            float horizontal = Random.Range(-xAxisMax, xAxisMax);
            var thisTransform = transform;
            var shootPointPos = shootPoint.position;
            Vector3 targetThrow = new Vector3(horizontal, thisTransform.position.y, forward);
            Vector3 vo = Calculate.CalculateVelocity(targetThrow, shootPointPos, 1);

            var mon = Instantiate(money, shootPointPos, quaternion.identity);
            mon.velocity = vo;

            npcCharacter.DOLookAt(targetThrow, 0.2f);
        }
    }
}
