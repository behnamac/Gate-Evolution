using UnityEngine;

namespace Collectable
{
    public class GateController : MonoBehaviour
    {
        [SerializeField] private Color goodColor = Color.green;
        [SerializeField] private Color badColor = Color.red;
        [SerializeField] private Renderer[] mesh;
        private Triggable _collectableController;
        private void Awake()
        {
            _collectableController = GetComponent<Triggable>();

            if (_collectableController)
            {
                ChangeColor();
            }
        }

        private void ChangeColor()
        {
           /* Color color;
            if (_collectableController.collectValue > 0)
            {
                color = goodColor;
            }
            else
            {
                color = badColor;
            }

            for (int i = 0; i < mesh.Length; i++)
            {
                mesh[i].material.color = color;
            }*/
        }
    }
}
