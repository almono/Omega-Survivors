using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBuff : MonoBehaviour
{
    public float buffDuration = 30f, maxBuffDuration = 30f, buffValue = 1f;
    public string buffName; // set buff name from icon name
    public Sprite buffImage;
    public TMP_Text buffDurationText;

    // Start is called before the first frame update
    void Start()
    {
        Transform childTransform = this.transform.Find("BuffCounter");
        buffDurationText = childTransform.gameObject.GetComponent<TMP_Text>();
        //TempBuffController.instance.AddBuff(this);
    }

    // Update is called once per frame
    void Update()
    {
        buffDuration -= Time.deltaTime;
        buffDurationText.text = buffDuration.ToString("00");

        if (buffDuration <= 0)
        {
            TempBuffController.instance.RemoveBuff(buffName);
            Destroy(gameObject);
        }
    }

    public void SetIcon(string pickupName)
    {
        buffName = pickupName;
        Transform childImage = this.transform.Find("Image");
        Sprite sprite = Resources.Load<Sprite>("Potions/" + pickupName);
        childImage.gameObject.GetComponent<Image>().sprite = sprite;
    }
}
