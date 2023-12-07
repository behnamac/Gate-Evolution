using UnityEngine;

    public class Triggable : MonoBehaviour
    {
        private IDestroy destroyBehavior;
        private ICollecable collectBehavior;
       // private Animator animator;


        #region Custom Methods
        public void SetCollectBehavior(ICollecable collecable)
        {
            collectBehavior = collecable;
        }
        public void SetDestroyBehavior(IDestroy destroy)
        {
            destroyBehavior = destroy;
        }

        public void PerformCollect()
        {
            destroyBehavior.ReactionMode(gameObject);
        }

        public void PerformDestroy()
        {
            collectBehavior.Collect();
        }


    #endregion

    #region Unity Methods

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        
    }


    #endregion
}
