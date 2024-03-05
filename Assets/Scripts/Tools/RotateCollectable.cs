using DG.Tweening;
using UnityEngine;

namespace Tools
{
    public class RotateCollectable : MonoBehaviour
    {

        [SerializeField] float rotationDuration = 1f;
        private readonly Vector3 _rotationAmount = new Vector3(0, 360, 0);
        private readonly LoopType loopType = LoopType.Restart;


        private void Start()
        {
            RotateObject();
        }

        private void RotateObject()
        {
            // Rotate the object infinitely
            transform.DORotate(_rotationAmount, rotationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, loopType).SetEase(Ease.Linear);
        }
    }
}
