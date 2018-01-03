using Assets.Scripts;
using Leap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureControl : MonoBehaviour
{
    Controller controller;
    public GameObject audioSourceA;
    public GameObject audioSourceB;
    public GameObject audioSourceC;
    FrameListener frameListener = new FrameListener();
    bool menuActive = false;
    GameObject circleMenu;
    float gestureCooldown = 1.5f;

    // Use this for initialization
    void Start()
    {
        controller = new Controller();

        controller.FrameReady += frameListener.OnFrame;
    }

    // Update is called once per frame
    void Update()
    {
        if (gestureCooldown >= 0.0f)
        {
            gestureCooldown -= Time.deltaTime;
        }
        else if(gestureCooldown <= 0)
        {
            asyncUpdate();
        }
    }

    private void OnApplicationQuit()
    {
        //controller.Dispose();
    }

    private void showMenu()
    {
        //circleMenu = Instantiate(Resources.Load("Menu/CircleMenu") as GameObject);
        menuActive = true;
    }

    private void hideMenu()
    {
        //Destroy(circleMenu);
        menuActive = false;
    }

    void asyncUpdate()
    {
        BookSlide activeSlide = GameObject.Find(GameObject.Find("Init").GetComponent<LoadBooks>().activeSlide.name).GetComponent<BookSlide>();
        LoadBooks library = GameObject.Find("Init").GetComponent<LoadBooks>();
        if (frameListener.twoFingerSwipe)
        {
            if (frameListener.indexTipVelocityX < -500)
            {
                gestureCooldown = 1.5f;
                Debug.Log("TwoFingerSwipe");
                if(activeSlide.activeBook.GetComponent<Book>().detailView)
                {
                    activeSlide.hideDetails();
                    GetComponent<UIControl>().hideUI();
                    GetComponent<UIControl>().sceneControl.GetComponent<SceneControl>().defaultLightSetup();
                }
                activeSlide.instantNextBook();
                audioSourceA.GetComponent<AudioSource>().Play();
            }
            else if (frameListener.indexTipVelocityX > 500)
            {
                gestureCooldown = 1.5f;
                if (activeSlide.activeBook.GetComponent<Book>().detailView)
                {
                    activeSlide.hideDetails();
                    GetComponent<UIControl>().hideUI();
                    GetComponent<UIControl>().sceneControl.GetComponent<SceneControl>().defaultLightSetup();
                }
                activeSlide.instantPrevBook();
                audioSourceA.GetComponent<AudioSource>().Play();
            }
            else if(frameListener.indexTipVelocityZ > 500)
            {
                if (activeSlide.activeBook.GetComponent<Book>().detailView)
                {
                    activeSlide.hideDetails();
                    GetComponent<UIControl>().hideUI();
                    GetComponent<UIControl>().sceneControl.GetComponent<SceneControl>().defaultLightSetup();
                }
                gestureCooldown = 1.5f;
                library.nextSlide();
                audioSourceB.GetComponent<AudioSource>().Play();
            }
            else if (frameListener.indexTipVelocityZ < -500 && frameListener.indexTipVelocityX < 300 && frameListener.indexTipVelocityX > -300)
            {
                if (activeSlide.activeBook.GetComponent<Book>().detailView)
                {
                    activeSlide.hideDetails();
                    GetComponent<UIControl>().hideUI();
                    GetComponent<UIControl>().sceneControl.GetComponent<SceneControl>().defaultLightSetup();
                }
                gestureCooldown = 1.5f;
                library.prevSlide();
                audioSourceB.GetComponent<AudioSource>().Play();
            }
        }

        else if(frameListener.LRotation)
        {
            if (!activeSlide.activeBook.GetComponent<Book>().detailView)
            {
                GetComponent<UIControl>().showUI();
                GetComponent<UIControl>().sceneControl.GetComponent<SceneControl>().detailViewLightSetup();
                GameObject.Find(GameObject.Find("Init").GetComponent<LoadBooks>().activeSlide.name).GetComponent<BookSlide>().showDetails();
                audioSourceC.GetComponent<AudioSource>().Play();
            }

        }

    }


}
