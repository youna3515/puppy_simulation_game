using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Defines;

public class UI_TaskOnGoing : UI_Popup
{
    PuppyTaskType _taskType;
    public PuppyTaskType TaskType
    {
        set
        {
            _taskType = value;
        }
    }

    enum Images
    {
        SleepImage,
        TakeWalkImage,
        BathImage
    }

    IEnumerator ShowTaskOnGoing(Image image, float fadeTime, float waitTime)
    {
        image.fillAmount = 0;
        float time = 0;
        while (time < fadeTime)
        {
            image.fillAmount += Time.deltaTime / fadeTime;
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        while (time > 0)
        {
            image.fillAmount -= Time.deltaTime / fadeTime;
            time -= Time.deltaTime;
            yield return null;
        }

        GameObject.Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        SaveUIObjectByEnum<Image>(typeof(Images));

        GetUIObject<Image>((int)Images.SleepImage).gameObject.SetActive(false);
        GetUIObject<Image>((int)Images.TakeWalkImage).gameObject.SetActive(false);
        GetUIObject<Image>((int)Images.BathImage).gameObject.SetActive(false);

        switch (_taskType)
        {
            case PuppyTaskType.SleepTask:
                GetUIObject<Image>((int)Images.SleepImage).gameObject.SetActive(true);
                StartCoroutine(ShowTaskOnGoing(GetUIObject<Image>((int)Images.SleepImage), 1.0f, 2.0f));
                break;
            case PuppyTaskType.TakeWalkTask:
                GetUIObject<Image>((int)Images.TakeWalkImage).gameObject.SetActive(true);
                StartCoroutine(ShowTaskOnGoing(GetUIObject<Image>((int)Images.TakeWalkImage), 1.0f, 2.0f));
                break;
            case PuppyTaskType.TakeWashTask:
                GetUIObject<Image>((int)Images.BathImage).gameObject.SetActive(true);
                StartCoroutine(ShowTaskOnGoing(GetUIObject<Image>((int)Images.BathImage), 1.0f, 2.0f));
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
