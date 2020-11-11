using Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateController : MonoBehaviour {
    public int i = 0;
    public GameObject canvasPrefabs, scoreTextPrefabs, tipsTextPrefabs, windTextPrefabs;
    private int score = 0, windDir = 0, windStrength = 0;

    private const float TIPS_SHOW_TIME = 0.5f;

    private GameObject canvas, scoreText, tipsText, windText;
    private SceneController scene;
    private string[] windDirectionArray;

    // Use this for initialization
    void Start () {
        scene = SceneController.getInstance();
        scene.setGameController(this);
        canvas = Instantiate(canvasPrefabs);
        scoreText = Instantiate(scoreTextPrefabs, canvas.transform);
        tipsText = Instantiate(tipsTextPrefabs, canvas.transform);
        windText = Instantiate(windTextPrefabs, canvas.transform);

        scoreText.GetComponent<Text>().text = "分数: " + score;
        tipsText.SetActive(false);
        windDirectionArray = new string[8] { "北", "东北", "东", "东南", "南", "西南", "西", "西北" };
        changeWind();
    }

    public void changeWind()
    {
        windDir = UnityEngine.Random.Range(0,8);
        windStrength = UnityEngine.Random.Range(0, 8);
        windText.GetComponent<Text>().text = "风向: " + windDirectionArray[windDir] + " x" + windStrength;
    }

    internal void addScore(int point)
    {
        i++;
        if(i%2==0)
            score += point;
        scoreText.GetComponent<Text>().text = "分数: " + score;
        changeWind();
    }

    // Update is called once per frame
    void Update () {
		
	}

    internal int getWindDirec()
    {
        return windDir;
    }

    internal int getWindStrength()
    {
        return windStrength;
    }

    //提示命中环数
    internal void showTips(int point)
    {
        tipsText.GetComponent<Text>().text = point + " Points!\n";
        switch (point)
        {
            case 1:
                tipsText.GetComponent<Text>().text += "Poor!";
                break;
            case 2:
                tipsText.GetComponent<Text>().text += "Fair!";
                break;
            case 3:
                tipsText.GetComponent<Text>().text += "Average!";
                break;
            case 4:
                tipsText.GetComponent<Text>().text += "Good!";
                break;
            case 5:
                tipsText.GetComponent<Text>().text += "Excellent!";
                break;

        }
        tipsText.SetActive(true);
        StartCoroutine(waitForTipsDisappear());
    }

    private IEnumerator waitForTipsDisappear()
    {
        yield return new WaitForSeconds(TIPS_SHOW_TIME);
        tipsText.SetActive(false);
    }
}
