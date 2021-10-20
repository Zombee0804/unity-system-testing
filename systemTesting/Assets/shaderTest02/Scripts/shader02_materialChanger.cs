using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shader02_materialChanger : MonoBehaviour
{

    // _VoronoiAngle, _VoronoiX, _VoronoiY, _VoronoiColour

    public Material mat;

    [Header("Position Variables")]
    public float posChangeAmount;
    public float posFadeAmount;

    public float posChangeLengthMin;
    public float posChangeLengthMax;

    public float posChangeLengthX;
    public float posChangeLengthY;

    public float posChangeAlarmX;
    public float posChangeAlarmY;

    public float posIncreasingX;
    public float posIncreasingY;

    public float posIncreasingXTarget;
    public float posIncreasingYTarget;

    [Header("Angle Variables")]
    public float angChangeAmount;
    public float angFadeAmount;

    public float angChangeLengthMin;
    public float angChangeLengthMax;

    public float angChangeLength;
    public float angChangeAlarm;
    public float angIncreasing;
    public float angIncreasingTarget;

    [Header("Colour Variables")]
    public bool empty;

    void Start() {
        mat = GetComponent<SpriteRenderer>().material;
        posIncreasingX = 1;
        posIncreasingY = 1;
        posIncreasingXTarget = -1;
        posIncreasingYTarget = -1;

        angIncreasing = 1;
        angIncreasingTarget = -1;
    }

    void Update() {
        PosChange();
        AngleChange();
        ColourChange();
    }

    void PosChange() {
        float voroX = mat.GetFloat("_VoronoiX");

        voroX += posChangeAmount * posIncreasingX * Time.deltaTime;
        if (posIncreasingXTarget == -1) {
            posChangeAlarmX += Time.deltaTime;
            if (posChangeAlarmX >= posChangeLengthX) {
                posChangeAlarmX = 0;
                posChangeLengthX = Random.Range(posChangeLengthMin, posChangeLengthMax);
                posIncreasingXTarget = Random.Range(0, 0 - Mathf.Sign(posIncreasingX));
            }
        }
        else {
            if (posIncreasingXTarget < 0) {
                posIncreasingX -= posFadeAmount * Time.deltaTime;
                if (posIncreasingX <= posIncreasingXTarget) {
                    posIncreasingXTarget = -1;
                }
            }
            else if (posIncreasingXTarget > 0) {
                posIncreasingX += posFadeAmount * Time.deltaTime;
                if (posIncreasingX >= posIncreasingXTarget) {
                    posIncreasingXTarget = -1;
                }
            }
        }

        mat.SetFloat("_VoronoiX", voroX);
        
        float voroY = mat.GetFloat("_VoronoiY");

        voroY += posChangeAmount * posIncreasingY * Time.deltaTime;
        if (posIncreasingYTarget == -1) {
            posChangeAlarmY += Time.deltaTime;
            if (posChangeAlarmY >= posChangeLengthY) {
                posChangeAlarmY = 0;
                posChangeLengthY = Random.Range(posChangeLengthMin, posChangeLengthMax);
                posIncreasingYTarget = Random.Range(0, 0 - Mathf.Sign(posIncreasingY));
            }
        }
        else {
            if (posIncreasingYTarget < 0) {
                posIncreasingY -= posFadeAmount * Time.deltaTime;
                if (posIncreasingY <= posIncreasingYTarget) {
                    posIncreasingYTarget = -1;
                }
            }
            else if (posIncreasingYTarget > 0) {
                posIncreasingY += posFadeAmount * Time.deltaTime;
                if (posIncreasingY >= posIncreasingYTarget) {
                    posIncreasingYTarget = -1;
                }
            }
        }

        mat.SetFloat("_VoronoiY", voroY);
    }

    void AngleChange() {
        float ang = mat.GetFloat("_VoronoiAngle");

        ang += angChangeAmount * angIncreasing * Time.deltaTime;
        if (angIncreasingTarget == -1) {
            angChangeAlarm += Time.deltaTime;
            if (angChangeAlarm >= angChangeLength) {
                angChangeAlarm = 0;
                angChangeLength = Random.Range(angChangeLengthMin, angChangeLengthMax);
                angIncreasingTarget = Random.Range(0, 0 - Mathf.Sign(angIncreasing));
            }
        }
        else {
            if (angIncreasingTarget < 0) {
                angIncreasing -= angFadeAmount * Time.deltaTime;
                if (angIncreasing <= angIncreasingTarget) {
                    angIncreasingTarget = -1;
                }
            }
            else if (angIncreasingTarget > 0) {
                angIncreasing += angFadeAmount * Time.deltaTime;
                if (angIncreasing >= angIncreasingTarget) {
                    angIncreasingTarget = -1;
                }
            }
        }

        mat.SetFloat("_VoronoiAngle", ang);
    }

    void ColourChange() {

    }
}
