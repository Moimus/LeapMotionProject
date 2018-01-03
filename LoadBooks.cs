using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadBooks : MonoBehaviour {
    public GameObject activeSlide;
    List<GameObject> slides;
    int slideIndex = 0;

	// Use this for initialization
	void Start () {
        //GetComponent<SpriteRenderer>().sprite = image2Sprite(Application.dataPath + "/Resources/Categories/Classic/Cover/cover02.jpg");
        //createBooks("Science");
        slides = new List<GameObject>();
        createCategories();
        activeSlide = slides[0];
	}
	
	// Update is called once per frame
	void Update () {


	}

    //don't use, for internal use only!
    private Texture2D image2Texture(string filePath)
    {
        Texture2D texture;
        byte[] byteArray;

        if(File.Exists(filePath))
        {
            byteArray = File.ReadAllBytes(filePath);
            texture = new Texture2D(1, 1);
            texture.LoadImage(byteArray);
            return texture;
        }
        else
        {
            Debug.LogError("Unable to load " + filePath);
            return null;
        }
    }

    private Sprite image2Sprite(string filePath)
    {
        Debug.Log("Loading: " + filePath);
        Sprite sprite = new Sprite();
        Texture2D spriteTexture = image2Texture(filePath);
        sprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        return sprite;
    }

    //Read .bookFiles and return content as array
    private string[] lexBook(string filePath)
    {
        string[] dataArray = File.ReadAllLines(filePath);
        foreach(string line in dataArray)
        {
            Debug.Log("lexBook: " + line);
        }

        return dataArray;
    }

    private void createCategories()
    {
        float margin = 1.5f;
        float marginSum = 0;

        string[] files = Directory.GetDirectories(Application.dataPath + "/Resources/Categories/");

        foreach(string file in files)
        {
            string[] splitString = file.Split("/".ToCharArray());
            string categoryName = splitString[splitString.Length - 1];
            Debug.Log("createCategories: " + categoryName);
            GameObject bookSlide = createBooks(categoryName);
            slides.Add(bookSlide);
            bookSlide.transform.position = new Vector3(0, 0, marginSum);
            marginSum += margin;
        }
    }

    //Load all found .book files of a certain category and instantiate the corresponding book objects
    private GameObject createBooks(string category)
    {
        float margin = 1.5f;
        float marginSum = 0;

        string[] files = Directory.GetFiles(Application.dataPath + "/Resources/Categories/" + category + "/data");
        GameObject slideInstance = Instantiate(Resources.Load("Bookslide") as GameObject);
        slideInstance.gameObject.name = category;
        foreach (string file in files) if (!file.Contains("meta"))
        {
            GameObject bookInstance = Instantiate(Resources.Load("BookPrefab") as GameObject);
            Book book = bookInstance.GetComponent<Book>(); 
            bookInstance.transform.position = new Vector3(marginSum, 1, 0);
            marginSum += margin;
            Debug.Log("createBooks found data: " + file.Replace("\\","/"));
            if(!file.Contains(".meta"))
            {
                string[] bookData = lexBook(file);
                book.coverPath = bookData[0];
                book.author = bookData[1];
                book.releaseDate = bookData[2];
                book.isbn = bookData[3];
                book.publisher = bookData[4];
                book.toc = bookData[5];
                book.url = bookData[6];
                book.title = bookData[7];
                bookInstance.gameObject.name = bookData[7];
            }
                bookInstance.GetComponent<SpriteRenderer>().sprite = image2Sprite(Application.dataPath + book.coverPath);
                bookInstance.transform.parent = slideInstance.transform;
                slideInstance.GetComponent<BookSlide>().books.Add(bookInstance);
                slideInstance.GetComponent<BookSlide>().activeBook = slideInstance.GetComponent<BookSlide>().books[0];
        }

        return slideInstance;
    }

    public void nextSlide()
    {
        if(slideIndex < slides.Count -1)
        {
            activeSlide.GetComponent<BookSlide>().activeBook.transform.Translate(transform.forward * 0.2f);
            activeSlide.transform.Translate(transform.forward * (1.5f * slides.Count));
            foreach (GameObject slide in slides)
            {
                slide.transform.Translate(-transform.forward * 1.5f);
            }
            slideIndex++;
            activeSlide = slides[slideIndex];
        }

    }

    public void prevSlide()
    {
        if(slideIndex > 0)
        {
            foreach (GameObject slide in slides)
            {
                slide.transform.Translate(transform.forward * 1.5f);
            }
            slideIndex--;
            activeSlide = slides[slideIndex];
            activeSlide.transform.Translate(-transform.forward * (1.5f * slides.Count));
        }


    }
}
