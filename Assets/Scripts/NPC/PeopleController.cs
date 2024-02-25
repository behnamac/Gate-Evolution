using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PeopleType { JustThrowMoney, TrowMoneyAndCheer, JustCheer }
public class PeopleController : MonoBehaviour
{
    [SerializeField] PeopleType peopleType;
    [Header("Throw GameObject")]
    [Tooltip("Assign the game of the object to be thrown to this variable from within the prefabs folder")]
    [SerializeField] Rigidbody throwable;
    [Tooltip("The point at which the thrower spawns")]
    [SerializeField] Transform throwingSpawnPoint;
    [Tooltip("The amount you want the game object to be thrown forward")]
    [SerializeField] float amountOfForwardThrow;

    [Header("Trigger Controller")]
    [SerializeField] LayerMask hitLayer;
    [SerializeField] Vector3 triggerCenter;
    [SerializeField] Vector3 triggerSize = Vector3.one;

    Animator anim;
    SkinnedMeshRenderer meshRenderer;
    bool isTrigger;
    float randomPos;
    bool visible;
    Rigidbody money;
    // Start is called before the first frame update
    void Start()
    {
       // GameManagerld.InGame += CheckHitPlayer;
       // GameManagerld.OnReset += OnResetLevel;

        anim = GetComponent<Animator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        anim.SetInteger("RandomIdle", Random.Range(0, 3));
    }

    private void Update()
    {
        if (meshRenderer.isVisible)
            visible = true;
        if (visible && !meshRenderer.isVisible)
            DestroyGameObject();
    }

    //After pressing the start key, each frame is called
    void CheckHitPlayer()
    {
        Vector3 newCenter = new Vector3(transform.position.x + triggerCenter.x, transform.position.y + triggerCenter.y, transform.position.z + triggerCenter.z);
        if (Physics.CheckBox(newCenter, triggerSize, Quaternion.identity, hitLayer) && !isTrigger)
        {
            PlayAnimation();
        }
    }
    void PlayAnimation() 
    {
        if (peopleType == PeopleType.JustThrowMoney || peopleType == PeopleType.TrowMoneyAndCheer)
        {
          //  randomPos = Random.Range(PlayerController.Instance.minX, PlayerController.Instance.maxX);
            StartCoroutine(LookAt(new Vector3(randomPos, 0, transform.position.z + amountOfForwardThrow)));
            anim.SetTrigger("Throw");
        }

        if (peopleType == PeopleType.JustCheer) 
        {
            Cheer();
        }
        isTrigger = true;
    }
    public void Throw()
    {
        //Throw
        Vector3 targetThrow = new Vector3(randomPos, PlayerController.Instance.transform.position.y, transform.position.z + amountOfForwardThrow);
        Vector3 VO = CalculateVelocity(targetThrow, throwingSpawnPoint.position, 1);
       /* for (int i = 0; i < GameManagerld.Instance.levelNumber; i++)
        {
            if (i == 0)
                money = Instantiate(throwable, throwingSpawnPoint.position, Quaternion.identity);
            else 
            {
                var m = Instantiate(throwable, new Vector3(money.transform.position.x, money.transform.position.y + 0.04f * i, money.transform.position.z), Quaternion.identity);
                m.transform.SetParent(money.transform);
                m.isKinematic = true;
                m.GetComponent<Collider>().enabled = false;
                m.GetComponent<RotateMoney>().enabled = false;
            }
        }*/
        money.velocity = VO;

        if (peopleType == PeopleType.TrowMoneyAndCheer)
            Cheer();
    }

    [System.Obsolete]
    void Cheer() 
    {
        if (PlayerController.Instance.conditionCloth == ConditionCloth.progress)
        {
            anim.SetTrigger("Like");
            anim.SetInteger("RandomLike", Random.Range(0, 7));
            ParticleManager.PlayParticle("EmojiHappy", new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z), Quaternion.Euler(-90, 0, 0));
        }
        if (PlayerController.Instance.conditionCloth == ConditionCloth.FellBack)
        {
            anim.SetTrigger("Dislike");
            anim.SetInteger("RandomDislike", Random.Range(0, 5));
            ParticleManager.PlayParticle("EmojiSad", new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z), Quaternion.Euler(-90, 0, 0));
        }
        StartCoroutine(LookAt(PlayerController.Instance.transform.position));
    }

    IEnumerator LookAt(Vector3 target) 
    {
        while (true) 
        {
            yield return new WaitForEndOfFrame();
            var dir = target - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;
        float Vxz = Sxz / time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    void OnResetLevel() 
    {
      //  GameManagerld.InGame -= CheckHitPlayer;
       // GameManagerld.OnReset -= OnResetLevel;
    }
    void DestroyGameObject() 
    {
       // GameManagerld.InGame -= CheckHitPlayer;
       // GameManagerld.OnReset -= OnResetLevel;
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 newCenterBox = new Vector3(transform.position.x + triggerCenter.x, transform.position.y + triggerCenter.y, transform.position.z + triggerCenter.z);
        Gizmos.DrawWireCube(newCenterBox, triggerSize);
        Vector3 newCenterSphere = new Vector3(transform.position.x, transform.position.y, transform.position.z + amountOfForwardThrow);
        Gizmos.DrawSphere(newCenterSphere, 0.1f);
    }
}
