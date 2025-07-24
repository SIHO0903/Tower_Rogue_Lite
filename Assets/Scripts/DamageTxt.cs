using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;


public class DamageTxt : MonoBehaviour
{
    float moveSpeed = 1f;
    float disappearTime = 0.5f;
    float disappearSpeed;

    TextMeshPro text;
    Color alpha;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;

    }
    private void OnEnable()
    {
        alpha.a = 1;
        disappearSpeed = 0f;
        StartCoroutine(DamageTxtPopUp());
    }

    IEnumerator DamageTxtPopUp()
    {
        while (gameObject.activeSelf)
        {
            disappearSpeed += Time.deltaTime;
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

            text.color = alpha;
            yield return new WaitForEndOfFrame();
            if (disappearSpeed >= disappearTime)
                gameObject.SetActive(false);
        }

    }
    void SetUp(float damage, Color color, float fontSize)
    {
        text.text = damage.ToString();
        alpha = color;
        text.fontSize = fontSize;
        text.alignment = TextAlignmentOptions.Center;
        text.fontStyle = FontStyles.Bold;
    }

    public static DamageTxt Create(Vector3 pos, float damage, Color color, float fontSize = 1.5f)
    {
        GameObject damageTextObj = PoolManager.Instance.Get(PoolEnum.Etc, "DamageTxt", pos, Quaternion.identity);
        DamageTxt damageText = damageTextObj.GetComponent<DamageTxt>();
        damageTextObj.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-0.05f, 0.05f) + pos.x, pos.y, -2f);


        damageText.SetUp(damage, color, fontSize);

        if (damage == 0)
            return null;
        else
            return damageText;
    }


}