using System.Collections;
using TMPro;
using UnityEngine;

public class JamesCard : MonoBehaviour
{
    public delegate void CallbackDelegateNull();
    public delegate void CallbackDelegateAnimBool(string stringIn, bool boolIn);

    string[] equivalentStrings = new string[]
    {
        "happiness",
        "population",
        "to a die",
        "dice next turn",
        "card next turn",
        "money"
    };

    public TextMeshPro dcText;
    public TextMeshPro headerText;
    public TextMeshPro bodyText;
    public Transform diePosition;

    PhysicsDie dieOnCard;

    public CardDataContainer cardData;

    private Vector3 m_TargetPosition = Vector3.zero;

    private bool m_TargetOnScreen = false;

    private bool m_Moving = false;

    private Animator m_Anim = null;

    private float m_AnimationTimer = 0.0f;

    private float m_CurrentAnimationDuration = 0.0f;

    public float m_MoveSpeed = 2.0f;

    private float m_MoveStartTime = 0.0f;

    private float m_MoveLength = 0.0f;

    public Vector3 cachedPosition;

    bool safelyOffscreen;

    public int index;

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_CurrentAnimationDuration = m_Anim.GetCurrentAnimatorStateInfo(0).length;
        cachedPosition = transform.position;
    }

    // Start is called before the first frame update
    private void Start()
    {
        LoadData();
    }

    private void Update()
    {

        #region OLD
        //// Don't move while animation is playing.
        //if (m_AnimationTimer >= m_CurrentAnimationDuration)
        //{
        //    // If currently moving, lerp to target.
        //    if (m_Moving)
        //    {
        //        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, ((Time.time - m_MoveStartTime) * m_MoveSpeed) / m_MoveLength);
        //    }

        //    // Arrived at destiation.
        //    if ((transform.position - m_TargetPosition).magnitude <= 0.1f)
        //    {
        //        m_Moving = false;
        //        // If coming on screen, play reveal card animation.
        //        if (m_TargetOnScreen)
        //        {
        //            m_Anim.Play("CardUnflip");
        //            m_AnimationTimer = 0.0f;
        //            m_CurrentAnimationDuration = m_Anim.GetCurrentAnimatorStateInfo(0).length;
        //        }
        //        // This card has left screen.
        //        else
        //        {
        //            CardManager.instance.UpdateCardsOffScreen();
        //        }
        //    }
        //}
        //else
        //{
        //    m_AnimationTimer += Time.deltaTime;
        //}
        #endregion
    }

    //public void SetTargetPosition(Vector3 target, bool targetOnScreen)
    //{
    //    m_TargetPosition = target;
    //    m_TargetOnScreen = targetOnScreen;

    //    m_Moving = true;

    //    m_MoveLength = Vector3.Distance(transform.position, m_TargetPosition);
    //    m_MoveStartTime = Time.time;

    //    // Hide card while moving off screen.
    //    if (!m_TargetOnScreen)
    //    {
    //        m_Anim.Play("CardFlip");
    //        m_AnimationTimer = 0.0f;
    //        m_CurrentAnimationDuration = m_Anim.GetCurrentAnimatorStateInfo(0).length;
    //    }
    //}

    public IEnumerator MoveToPoint(Vector3 point)
    {
        while (Vector3.Distance(transform.position, point) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, m_MoveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        transform.position = point;
    }

    public IEnumerator MoveToPoint(Vector3 point, CallbackDelegateNull callback)
    {
        yield return MoveToPoint(point);
        callback();
    }

    public IEnumerator MoveToPoint(Vector3 point, CallbackDelegateAnimBool callback, string callbackString, bool callbackBool)
    {
        yield return MoveToPoint(point);
        callback(callbackString, callbackBool);
    }

    void LoadData()
    {
        if (!cardData)
        {
            Debug.LogError($"Card {this.name} is missing a Card Data asset!");
            return;
        }
        headerText.text = cardData.header;
        dcText.text = cardData.dc.ToString();
        bodyText.text = $"<color=green><b>Success:</b></color>\n{StringConstructor(cardData.successEffects)}\n<color=red><b>Failure:</b></color>\n{StringConstructor(cardData.failureEffects)}";
    }

    string StringConstructor(CardEffect[] effects)
    {
        if (effects.Length == 0)
        {
            return "   No effect";
        }
        string[] output = new string[effects.Length];
        for (int i = 0; i < effects.Length; i++)
        {
            output[i] = $"  {(effects[i].m_Severity > 0 ? "+" : "")}{effects[i].m_Severity} {equivalentStrings[(int)effects[i].m_Effect]}";
        }

        return string.Join("\n", output);
    }

    public void SetDie(PhysicsDie die)
    {
        dieOnCard = die;
        Debug.Log("Die set!");
    }

    public void SetCardData(CardDataContainer cdc)
    {
        cardData = cdc;
        headerText.text = cardData.header;
        dcText.text = cardData.dc.ToString();
        bodyText.text = $"<color=green><b>Success:</b></color>\n{StringConstructor(cardData.successEffects)}\n<color=red><b>Failure:</b></color>\n{StringConstructor(cardData.failureEffects)}";
    }

    public void CheckResult()
    {
        JamesManager.instance.DoEffects((dieOnCard == null || dieOnCard.GetResult() < cardData.dc) ? cardData.failureEffects : cardData.successEffects);
    }

    public PhysicsDie GetDie()
    {
        return dieOnCard;
    }

    public Animator GetAnimator() => m_Anim;

    public void MoveCardOffscreen() => StartCoroutine(MoveToPoint(CardManager.instance.m_OffScreenPosition.position, Offscreen));

    void Offscreen() => CardManager.instance.safelyOffscreen = (CardManager.instance.safelyOffscreen | 1 << index);
}
