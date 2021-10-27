using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shader02_valueChanger : MonoBehaviour
{

    // changeAmount, fadeAmount, lengthMin, lengthMax, length, alarm, increasing, increasingTarget

    public Material mat;

    [Header("Position X Variables")]
    public string xReference;
    public float xChangeAmount;
    public float xFadeAmount;
    public float xLengthMin;
    public float xLengthMax;
    public float xLength;
    public float xAlarm;
    public float xIncreasing;
    public float xIncreasingTarget;
    public Dictionary<string, float> posXValues= new Dictionary<string, float>();

    [Header("Position Y Variables")]
    public string yReference;
    public float yChangeAmount;
    public float yFadeAmount;
    public float yLengthMin;
    public float yLengthMax;
    public float yLength;
    public float yAlarm;
    public float yIncreasing;
    public float yIncreasingTarget;
    public Dictionary<string, float> posYValues= new Dictionary<string, float>();

    [Header("Angle Variables")]
    public string angleReference;
    public float angleChangeAmount;
    public float angleFadeAmount;
    public float angleLengthMin;
    public float angleLengthMax;
    public float angleLength;
    public float angleAlarm;
    public float angleIncreasing;
    public float angleIncreasingTarget;
    public Dictionary<string, float> angleValues= new Dictionary<string, float>();

    [Header("Colour Variables")]
    public float colourChangeAmount;
    public float colourFadeAmount;
    public float colourLengthMin;
    public float colourLengthMax;
    public float colourLength;
    public float colourAlarm;
    public float colourIncreasing;
    public float colourIncreasingTarget;
    public Dictionary<string, float> colourRed = new Dictionary<string, float>();
    public Dictionary<string, float> colourGreen = new Dictionary<string, float>();
    public Dictionary<string, float> colourBlue = new Dictionary<string, float>();

    void Start() {
        mat = GetComponent<SpriteRenderer>().material;

        posXValues.Add("changeAmount", xChangeAmount);
        posXValues.Add("fadeAmount", xFadeAmount);
        posXValues.Add("lengthMin", xLengthMin);
        posXValues.Add("lengthMax", xLengthMax);
        posXValues.Add("length", xLength);
        posXValues.Add("alarm", xAlarm);
        posXValues.Add("increasing", xIncreasing);
        posXValues.Add("increasingTarget", xIncreasingTarget);

        posYValues.Add("changeAmount", yChangeAmount);
        posYValues.Add("fadeAmount", yFadeAmount);
        posYValues.Add("lengthMin", yLengthMin);
        posYValues.Add("lengthMax", yLengthMax);
        posYValues.Add("length", yLength);
        posYValues.Add("alarm", yAlarm);
        posYValues.Add("increasing", yIncreasing);
        posYValues.Add("increasingTarget", yIncreasingTarget);

        angleValues.Add("changeAmount", angleChangeAmount);
        angleValues.Add("fadeAmount", angleFadeAmount);
        angleValues.Add("lengthMin", angleLengthMin);
        angleValues.Add("lengthMax", angleLengthMax);
        angleValues.Add("length", angleLength);
        angleValues.Add("alarm", angleAlarm);
        angleValues.Add("increasing", angleIncreasing);
        angleValues.Add("increasingTarget", angleIncreasingTarget);

        Dictionary<string, float> colourValues = new Dictionary<string, float>();
        colourValues.Add("changeAmount", colourChangeAmount);
        colourValues.Add("fadeAmount", colourFadeAmount);
        colourValues.Add("lengthMin", colourLengthMin);
        colourValues.Add("lengthMax", colourLengthMax);
        colourValues.Add("length", colourLength);
        colourValues.Add("alarm", colourAlarm);
        colourValues.Add("increasing", colourIncreasing);
        colourValues.Add("increasingTarget", colourIncreasingTarget);

        colourRed = colourValues;
        colourGreen = colourValues;
        colourBlue = colourValues;
    }

    void Update() {
        posXValues = FadeValue(xReference, posXValues);
        posYValues = FadeValue(yReference, posYValues);
        angleValues = FadeValue(angleReference, angleValues);

        colourRed = FadeValue("_VoroR", colourRed, true);
        // colourGreen = FadeValue("_VoroG", colourGreen, true);
        // colourBlue = FadeValue("_VoroB", colourBlue, true);
    }

    Dictionary<string, float> FadeValue(string shaderRef, Dictionary<string, float> dict, bool mod = false) {

        float currentVal = mat.GetFloat(shaderRef);
        Debug.Log(shaderRef + currentVal.ToString());

        currentVal += dict["changeAmount"] * dict["increasing"] * Time.deltaTime;
        if (dict["increasingTarget"] == -1) {
            dict["alarm"] += Time.deltaTime;
            if (dict["alarm"] >= dict["length"]) {
                dict["alarm"] = 0;
                dict["length"] = Random.Range(dict["lengthMin"], dict["lengthMax"]);
                dict["increasingTarget"] = Random.Range(0, 0 - Mathf.Sign(dict["increasing"]));
            }
        }
        else {
            if (dict["increasingTarget"] < 0) {
                dict["increasing"] -= dict["fadeAmount"] * Time.deltaTime;
                if (dict["increasing"] <= dict["increasingTarget"]) {
                    dict["increasingTarget"] = -1;
                }
            }
            else if (dict["increasingTarget"] > 0) {
                dict["increasing"] += dict["fadeAmount"] * Time.deltaTime;
                if (dict["increasing"] >= dict["increasingTarget"]) {
                    dict["increasingTarget"] = -1;
                }
            }
        }

        if (mod == true) {
            currentVal = currentVal % 255;
        }
        mat.SetFloat(shaderRef, currentVal);
        return dict;
    }
}
