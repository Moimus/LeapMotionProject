using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSlide : MonoBehaviour {

    public List<GameObject> books;
    public GameObject activeBook;
    GameObject uiControl;
    public int activeBookIndex = 0;

	// Use this for initialization
	void Start () {
        List<GameObject> books = new List<GameObject>();
        uiControl = GameObject.Find("UIControl");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator nextBook()
    {
        if(activeBookIndex < books.Count -1)
        {
            activeBookIndex++;
            activeBook = books[activeBookIndex];
            float movementLeft = 1.5f;

            while(movementLeft > 0)
            {
                transform.Translate(-transform.right * 0.05f);
                movementLeft -=  0.05f;
                yield return new WaitForSeconds(Time.deltaTime);
            }

        }

    }

    public void instantNextBook()
    {
        if (activeBookIndex < books.Count - 1)
        {
            activeBook.transform.Translate(transform.forward * 0.2f);
            activeBookIndex++;
            activeBook = books[activeBookIndex];
            activeBook.transform.Translate(-transform.forward * 0.2f);
            transform.Translate(-transform.right * 1.5f);
            //uiControl.GetComponent<UIControl>().setUIContent(activeBook.GetComponent<Book>().title, "");
        }
    }

    public void instantPrevBook()
    {
        if (activeBookIndex > 0)
        {
            activeBook.transform.Translate(transform.forward * 0.2f);
            activeBookIndex--;
            activeBook = books[activeBookIndex];
            activeBook.transform.Translate(-transform.forward * 0.2f);
            transform.Translate(transform.right * 1.5f);
            //uiControl.GetComponent<UIControl>().setUIContent(activeBook.GetComponent<Book>().title, "");
        }
    }

    public void showDetails()
    {
        if(!activeBook.GetComponent<Book>().detailView)
        {
            activeBook.transform.Translate(-transform.right * 0.5f);
            activeBook.transform.Rotate(-transform.up * 15);
            uiControl.GetComponent<UIControl>().setUIContent(activeBook.GetComponent<Book>().title, activeBook.GetComponent<Book>().author + "\n" + activeBook.GetComponent<Book>().isbn + "\n" + activeBook.GetComponent<Book>().publisher);
            activeBook.GetComponent<Book>().detailView = true;
        }

    }

    public void hideDetails()
    {
        if (activeBook.GetComponent<Book>().detailView)
        {
            activeBook.transform.Translate(transform.right * 0.5f);
            activeBook.transform.Rotate(transform.up * 15);
            activeBook.GetComponent<Book>().detailView = false;
        }

    }


}
