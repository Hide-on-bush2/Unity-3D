using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstController : MonoBehaviour,ISceneController,IUserAction
{

    //private List<GameObject> patrolList;
    public GameObject hero;
    private PatrolFactory patrolFactory;
    private RockFactory rockFactory;
    public enum Status { Win,Loss,Gaming};
    public Status status;
    //private List<Sprite> patrolList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        Director director = Director.getInstance();
        director.setFPS(60);
        director.currentSceneController = this;
      
        patrolFactory = new PatrolFactory();
        rockFactory = new RockFactory();
        patrolFactory.Awake();
        rockFactory.Awake();

        director.currentSceneController.LoadResources();
        status = Status.Gaming;

        
        //patrolList = new List<GameObject>();
        //patrolList = new List<GameObject>();
        //director.currentSceneController.LoadResources();
    }

    // Update is called once per frame
    void Update()
    {
        if(status == Status.Gaming)
        {
            patrolFactory.Update();
            rockFactory.Update();
        }
        
        
    }

    public void LoadResources()
    {
        //for (int i = 0; i < 4; i++)
        //{
        //    GameObject patrol = Instantiate(Resources.Load<GameObject>("prefabs/patrol"));
        //    float randX = Random.Range(-2f, 2f);
        //    float randY = Random.Range(-2f, 2f);
        //    patrol.transform.position = new Vector2(randX, randY);
        //    patrolList.Add(patrol);
        //}

        hero = Instantiate(Resources.Load<GameObject>("prefabs/hero"));
        hero.transform.position = new Vector2(2, 2);

    }
    public void Pause()
    {

    }
    public void Resume()
    {

    }

    void OnEnable()
    {
        EventManager.GameOverChange += GameOver;
            
    }

    void DestroyAll()
    {
        patrolFactory.DestroyAll();
        rockFactory.DestroyAll();
        Destroy(hero);
    }


    void GameOver(bool win)
    {
        if (win)
        {
            status = Status.Win;
            DestroyAll();
            Debug.Log("You win");
        }
        else
        {
            status = Status.Loss;
            DestroyAll();
            Debug.Log("You loss");
        }
    }

    public Status getStatus()
    {
        return status;
    }
    
}
