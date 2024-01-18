using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Game1Controller : MonoBehaviour
{
    public int levelWinScore;

    public const int columns = 4;
    public const int rows = 2;

    public const float Xspace = 4f;
    public const float Yspace = -5f;

    [SerializeField] private Bulmaca startObject;
    [SerializeField] private Sprite[] images;

    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }
    public List<GameObject> list;
    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3 };
        locations = Randomiser(locations);

        Vector3 startPosition = startObject.transform.position;

        for(int i =0; i < columns ; i++)
        {
            for(int j = 0; j < rows ; j++)
            {
                Bulmaca gameImage;
                if(i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else
                {
                    gameImage = Instantiate(startObject) as Bulmaca;
                }

                int index = j* columns + i;
                int id = locations[index];

                gameImage.ChangeSprite(id, images[id]);

                float positionX = (Xspace * i) + startPosition.x;
                float positionY = (Yspace * j) + startPosition.y;

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
                list.Add(gameImage.gameObject);
            }
        }
 
    }
    private Bulmaca firstOpen;
    private Bulmaca secondOpen;

    private int score = 0;
    private int attemps = 0;

    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro attempsText;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    public void imageOpened(Bulmaca startObject)
    {
        if (firstOpen == null)
        {
            firstOpen = startObject;
        }
        else
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == secondOpen.spriteId)
        {
            AudioManager.instance.Play("Correct");
            score++;
            scoreText.text = "Score: " + score;
        }
        else
        {
            AudioManager.instance.Play("Wrong");
            yield return new WaitForSeconds(0.5f);

            firstOpen.Close();
            secondOpen.Close();
        }
        attemps++;
        attempsText.text = "Attempts: " + attemps;
        firstOpen = null;
        secondOpen = null;

        CheckLevelFinish();
    }

    public void CheckLevelFinish()
    {
        if (score == levelWinScore) {
            UIManager.instance.OpenWinPanel(true);
            for (int i = list.Count-1; i >= 0; i--)
            {
                Destroy(list[i]);
            }
        } 
    }
}