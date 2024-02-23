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

    [Header("Movement")]
    [Tooltip("Player speed to move on the Z axis")]
    [SerializeField] float speedForward;
    [Tooltip("Player speed to move on the X axis")]
    [SerializeField] float speedHorizontal;
    [Tooltip("Player speed in X way")]
    [SerializeField] float speedXWay;
    [Tooltip("The position of the player on the X axis does not exceed this number")]
    public float minX;
    [Tooltip("The position of the player on the X axis does not exceed this number")]
    public float maxX;

    [Header("Money Controller")]
    [Tooltip("The amount that is added to the money each time")]
    [SerializeField] int addMoneyValue;
    int currntAddMoney;
    [Tooltip("This is the amount of money the player has at the beginning of the game")]
    [SerializeField] int firstMoney;

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
    Transform finishLine;

    float move;
    int currentLevelNumber;
    float firstXPos;
    float firstSpeed;
    bool finish;
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
        GameManagerld.OnStart += StartMove;
       // GameManager.InGame += Movement;
        GameManagerld.OnWin += Win;
        GameManagerld.OnLose += Lose;
        GameManagerld.OnReset += OnResetLevel;
        GameManagerld.Instance.SetMoney(firstMoney);

        UIManagerOld.Instance.UpdateSexynestBar();
        HeaderUpTheHead.Instance.ChangeClothText(clothDic[ClothType.Neutral].clothType.ToString());

        anim = GetComponentsInChildren<Animator>();
        finishLine = GameObject.FindGameObjectWithTag("Finish").transform;

        currntAddMoney = addMoneyValue;

        for (int i = 0; i < clothController.Length; i++)
            clothController[i].SetActive(false);

        clothDic[ClothType.Neutral].SetActive(true);

        nextClothValue = 0.25f;
        firstXPos = transform.position.x;
        firstSpeed = speedForward;
    }
    //It is called once when the start key is pressed
    void StartMove() 
    {
        HeaderUpTheHead.Instance.UpdateHotBar(currentSexynest / nextClothValue);
        for (int i = 0; i < anim.Length; i++)
            anim[i].SetBool("Move", true);
    }
    //After pressing the start key, each frame is called
    /*void Movement() 
    {
        if (CameraFollow.Instance.cameraType == CameraType.Front && !finish)
            move = -UIManager.Instance.joystick.HorizontalInput();
        if (CameraFollow.Instance.cameraType == CameraType.Back && !finish)
            move = UIManager.Instance.joystick.HorizontalInput();
        transform.Translate(move * speedHorizontal * Time.deltaTime, 0, speedForward * Time.deltaTime);
        Vector3 newPos = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);
        transform.position = newPos;
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetFloat("Horizontal", move);
        }
    }*/
    //Called once after winning
    void Win() 
    {
        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetBool("Move", false);
            anim[i].SetTrigger("Dance");
            anim[i].SetInteger("RandomDance", Random.Range(0, 4));
        }
    }

    //Called once after losing
    void Lose() 
    {

    }

    void Upgrade() 
    {
        GameManagerld.Instance.Upgrade();
        currentLevelNumber = 0;
    }
    void Sexynest(float getValue) 
    {
        currentSexynest += getValue;
        currentSexynest = Mathf.Clamp01(currentSexynest);
        UIManagerOld.Instance.UpdateSexynestBar();
    }

    //It is called when the player hits an game object with a money tag
    void SetMoney(Collider MoneyHit) 
    {
        GameManagerld.Instance.SetMoney(currntAddMoney * GameManagerld.Instance.levelNumber);
        currentLevelNumber += currntAddMoney;

        if (currentLevelNumber >= GameManagerld.Instance.moneyForUpgrade)
            Upgrade();

        Destroy(MoneyHit.gameObject);
    }

    //It is called when the player hits an game object with a gate tag
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
        UIManagerOld.Instance.UpdateSexynestBar();
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
        UIManagerOld.Instance.UpdateSexynestBar();
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
        GameManagerld.Instance.SetXAmount();
        UIManagerOld.Instance.UpdateSexynestBar();

        if (currentSexynest <= 0) 
        {
            GameManagerld.Instance.Win();
        }
        if (!finish) 
        {
            StartCoroutine(GoToCenterLine());
            finish = true;
            move = 0;
        }
    }
    IEnumerator GoToCenterLine() 
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money")) 
        {
            SetMoney(other);
        }
        if (other.gameObject.CompareTag("Gate"))
        {
            if (GameManagerld.Instance.moneyNumber >= other.GetComponent<GateController>().dataGate.clothPrice * GameManagerld.Instance.levelNumber)
            {
                if (other.GetComponent<GateController>().gateType == GateType.Good)
                    UpHot(choiceGoodGate);
                if (other.GetComponent<GateController>().gateType == GateType.Bad)
                    DownHot(choiceBadGate);
                GameManagerld.Instance.SetMoney(-other.GetComponent<GateController>().dataGate.clothPrice * GameManagerld.Instance.levelNumber);

                other.GetComponent<GateController>().DestroyCloth();
                ParticleManager.PlayParticle("destroyCloth", new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z + 1), Quaternion.identity);

            }
            else
                DownHot(choiceBadGate);
        }
        if (other.gameObject.CompareTag("+Collectible"))
        {
            UpHot(0.05f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("-Collectible"))
        {
            DownHot(0.05f);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("X")) 
        {
            SetXAmount();
        }
    }

    void OnResetLevel() 
    {
        GameManagerld.OnStart -= StartMove;
       // GameManager.InGame -= Movement;
        GameManagerld.OnWin -= Win;
        GameManagerld.OnLose -= Lose;
        GameManagerld.OnReset -= OnResetLevel;
    }
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
