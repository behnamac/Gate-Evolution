using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ConditionCloth { progress, FellBack }
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [HideInInspector] public ConditionCloth conditionCloth;
    [HideInInspector] public ClothType thisCloth;


    [Header("Sexynest Controller")]
    [SerializeField, Range(0, 1)] float choiceBadGate;
    [SerializeField, Range(0, 1)] float choiceGoodGate;
    [HideInInspector] public float currentSexynest;
    [SerializeField] TextMeshPro sexynetText;
    float thisClothValue;
    float nextClothValue;

    [Header("Cloth Controller")]
    [SerializeField] ClothController[] clothController;
    Dictionary<ClothType, ClothController> clothDic;

    Animator[] anim;


    private void Awake()
    {
        Instance = this;

        clothDic = new Dictionary<ClothType, ClothController>();

        for (int i = 0; i < clothController.Length; i++)
            clothDic.Add(clothController[i].clothType, clothController[i]);
    }

    // Start is called before the first frame update
    void Start()
    {
 
        HeaderUpTheHead.Instance.ChangeClothText(clothDic[ClothType.Neutral].clothType.ToString());



        for (int i = 0; i < clothController.Length; i++)
            clothController[i].SetActive(false);

        clothDic[ClothType.Neutral].SetActive(true);

        nextClothValue = 0.25f;

    }
    //It is called once when the start key is pressed
    void StartMove() 
    {
        HeaderUpTheHead.Instance.UpdateHotBar(currentSexynest / nextClothValue);
        for (int i = 0; i < anim.Length; i++)
            anim[i].SetBool("Move", true);
    }
    //After pressing the start key, each frame is called

    void Win() 
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetBool("Move", false);
            anim[i].SetTrigger("Dance");
            anim[i].SetInteger("RandomDance", Random.Range(0, 4));
        }
    }

    void Sexynest(float getValue) 
    {
        currentSexynest += getValue;
        currentSexynest = Mathf.Clamp01(currentSexynest);
    }


    [System.Obsolete]
    void SetCloth(ClothType clothName)
    {
        ParticleManager.PlayParticle("Cloth", new Vector3(transform.position.x, transform.position.y + 1, transform.position.z + 1), Quaternion.Euler(-90, 0, 0));
        //Diactive Cloth
        for (int i = 0; i < clothController.Length; i++)
        {
            if (clothController[i].active)
            {
                clothController[i].SetActive(false);
            }
        }

        //Active Cloth
        clothDic[clothName].SetActive(true);
        HeaderUpTheHead.Instance.ChangeClothText(clothDic[clothName].clothType.ToString());

        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetBool("Move", true);
            anim[i].SetTrigger("Spine");
        }
    }

    void DownHot(float value)
    {
        if (currentSexynest < thisClothValue)
        {
            Debug.Log("Bad");
            conditionCloth = ConditionCloth.FellBack;
            ParticleManager.PlayParticle("EmojiSad", new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z + 1), Quaternion.Euler(-90, 0, 0));
            thisClothValue -= 0.25f; nextClothValue -= 0.25f;
            if (thisCloth != ClothType.Neutral)
                SetCloth(thisCloth-1);
            if (thisCloth - 1 >= ClothType.Neutral)
                thisCloth = thisCloth - 1;
        }

        currentSexynest -= value;
        currentSexynest = Mathf.Clamp(currentSexynest, 0, 1); thisClothValue = Mathf.Clamp(thisClothValue, 0, 0.75f); nextClothValue = Mathf.Clamp(nextClothValue, 0.25f, 1);
        sexynetText.text = "-" + Mathf.Round(value * 100);
        HeaderUpTheHead.Instance.UpdateHotBar(currentSexynest / nextClothValue);
        sexynetText.color = Color.red;
        Invoke("HideSexynetText", 1);
    }
    void UpHot(float value) 
    {
        currentSexynest += value;

        if (currentSexynest >= nextClothValue)
        {
            Debug.Log("Good");
            conditionCloth = ConditionCloth.progress;
            ParticleManager.PlayParticle("EmojiHappy", new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z + 1), Quaternion.Euler(-90, 0, 0));
            thisClothValue += 0.25f; nextClothValue += 0.25f;
            if (thisCloth != ClothType.Stripper)
                SetCloth(thisCloth + 1);
            if (thisCloth + 1 <= ClothType.Stripper)
                thisCloth = thisCloth + 1;
        }
        currentSexynest = Mathf.Clamp(currentSexynest, 0, 1); thisClothValue = Mathf.Clamp(thisClothValue, 0, 0.75f); nextClothValue = Mathf.Clamp(nextClothValue, 0.25f, 1);
        sexynetText.text = "+" + Mathf.Round(value * 100);
        HeaderUpTheHead.Instance.UpdateHotBar(currentSexynest / nextClothValue);
        sexynetText.color = Color.green;
        Invoke("HideSexynetText", 1);
    }
    void HideSexynetText() 
    {
        sexynetText.text = "";
    }

    [System.Obsolete]
    void SetXAmount() 
    {
        ParticleManager.PlayParticle("Money", new Vector3(transform.position.x, transform.position.y + 4.5f, transform.position.z), Quaternion.Euler(-90, 0, 0));

        currentSexynest -= 0.1005f;

        if (currentSexynest <= 0) 
        {
        }
    
    }
   /* IEnumerator GoToCenterLine() 
    {
        while (GameManagerld.Instance.conditionGame == ConditionGame.InGame)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(firstXPos, transform.position.y, transform.position.z), 2 * Time.deltaTime);
            speedForward = speedXWay;
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].SetFloat("MoveSpeed", speedXWay / firstSpeed);
            }
        }
    }*/    
 
       
}
[System.Serializable]
public class ClothController 
{
    public string clothName;
    public ClothType clothType;
    public ClothBody clothBody;
    public Transform cloth;
    [HideInInspector] public bool active;

    public void SetActive(bool setActive) 
    {
        cloth.gameObject.SetActive(setActive);
        active = setActive;
    }
}
