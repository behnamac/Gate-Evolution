using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance;
        public Transform target;

        [SerializeField] private float maxXClamp = 0.1f;
        [SerializeField] private float SmoothFollow = 1.5f;

        private Vector3 offset;
        private Vector3 totalOffest;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            offset = transform.position - target.position;
            totalOffest = transform.position;
        }

        private void Follow()
        {
            totalOffest = target.position + offset;
            totalOffest.x = Mathf.Clamp(totalOffest.x,-maxXClamp, maxXClamp);
            transform.position = Vector3.Slerp(transform.position, totalOffest, SmoothFollow*Time.deltaTime);
        }

        private void LateUpdate()
        {
            Follow();
        }
    }
}

