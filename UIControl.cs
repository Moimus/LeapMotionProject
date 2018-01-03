using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
    //Class controls Input & UI display
    public GameObject uiCanvas;
    public GameObject sceneControl;

	// Use this for initialization
	void Start () {
        hideUI();
	}
	
	// Update is called once per frame
	void Update () {
        //DEBUG STUFF
        if (Input.GetMouseButtonDown(0))
        {
            //StartCoroutine(GameObject.Find("Classic").GetComponent<BookSlide>().nextBook());
            GameObject.Find(GameObject.Find("Init").GetComponent<LoadBooks>().activeSlide.name).GetComponent<BookSlide>().instantNextBook();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GameObject.Find(GameObject.Find("Init").GetComponent<LoadBooks>().activeSlide.name).GetComponent<BookSlide>().instantPrevBook();
        }
        else if(Input.GetKeyDown(KeyCode.U))
        {
            showUI();
            GameObject.Find(GameObject.Find("Init").GetComponent<LoadBooks>().activeSlide.name).GetComponent<BookSlide>().showDetails();
            //sceneControl.GetComponent<SceneControl>().dimSun();
            sceneControl.GetComponent<SceneControl>().detailViewLightSetup();
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            hideUI();
            GameObject.Find(GameObject.Find("Init").GetComponent<LoadBooks>().activeSlide.name).GetComponent<BookSlide>().hideDetails();
            sceneControl.GetComponent<SceneControl>().defaultLightSetup();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameObject.Find("Init").GetComponent<LoadBooks>().nextSlide();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            GameObject.Find("Init").GetComponent<LoadBooks>().prevSlide();
        }
    }

    public void setUIContent(string Headline, string Content)
    {
        uiCanvas.transform.Find("headline").GetComponent<Text>().text = Headline;
        uiCanvas.transform.Find("content").GetComponent<Text>().text = Content;
    }

    public void showUI()
    {
        uiCanvas.SetActive(true);
    }

    public void hideUI()
    {
        uiCanvas.SetActive(false);
    }
}
