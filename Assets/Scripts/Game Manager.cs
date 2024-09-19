using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Bricks[] bricks { get; private set; }   
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //subscribe 
        //used when game manager is in global and not in level1   
        SceneManager.sceneLoaded+=OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        //for not creating more than a particular level
        //  if (level > 10)
        //  {
        //   SceneManager.LoadScene("WinScreen");
        //   }
        // else
        //{
        //   SceneManager.LoadScene("Level" + level);
        //}
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene,LoadSceneMode mode)
    {
        this.ball=FindObjectOfType<Ball>();
        this.paddle= FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Bricks>();
    }
    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
      //  for(int i=0;i<this.bricks.Length;i++)
      //  {
      //      this.bricks[i].ResetBrick();
     //   }
    }
    private void GameOver()
    {
        //OPTION 1:-
        //this is for extra scene that we create and is to be diaplayed...
        // SceneManager.LoadScene("Game Over!");
        //OR
        //Option 2:-
        //To restart the game
        NewGame();
    }
    public void Miss()
    {
        this.lives--;
        if(this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }
    public void Hit(Bricks brick)
    {
        this.score += brick.points;
        if(Cleared())
        {
            LoadLevel(this.level + 1);  
        }
    }
    private bool Cleared()
    {
      for(int i=0;i<this.bricks.Length;++i)
        {
            if(this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable)
            {
                return false;
            }       

        }
        return true;
        }
    }

