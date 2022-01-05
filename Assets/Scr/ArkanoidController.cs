using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkanoidController : MonoBehaviour
{
    private const string BALL_PREFAB_PATH = "Prefabs/Ball";
    private readonly Vector2 BALL_INIT_POSITION = new Vector2(0, -0.86f);
    
    [SerializeField]
    private GridController _gridController;
    
    [Space(20)]
    [SerializeField]
    private List<LevelData> _levels = new List<LevelData>();
    
    private int _currentLevel = 0;
    private static int _totalScore = 0;
    
    private Ball _ballPrefab = null;
    private List<Ball> _balls = new List<Ball>();

    private PowerUp myPrefab = null;    
    
    private void Start()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent += OnBallReachDeadZone;
        ArkanoidEvent.OnBlockDestroyedEvent += OnBlockDestroyed;
    }

    private void OnDestroy()
    {
        ArkanoidEvent.OnBallReachDeadZoneEvent -= OnBallReachDeadZone;
        ArkanoidEvent.OnBlockDestroyedEvent -= OnBlockDestroyed;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitGame();
        }
    }
    
    private void InitGame()
    {
        _currentLevel = 0;
        
        _gridController.BuildGrid(_levels[0]);
        SetInitialBall();
        
        ArkanoidEvent.OnGameStartEvent?.Invoke();
        ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(0, _totalScore);
    }
    
    private void SetInitialBall()
    {
        ClearBalls();

        Ball ball = CreateBallAt(BALL_INIT_POSITION);
        ball.Init();
        _balls.Add(ball);
    }
    
    private Ball CreateBallAt(Vector2 position)
    {
        if (_ballPrefab == null)
        {
            _ballPrefab = Resources.Load<Ball>(BALL_PREFAB_PATH);
        }

        return Instantiate(_ballPrefab, position, Quaternion.identity);
    }
    
    private void ClearBalls()
    {
        for (int i = _balls.Count - 1; i >= 0; i--)
        {
            _balls[i].gameObject.SetActive(false);
            Destroy(_balls[i]);
        }
        
        _balls.Clear();
    }
    
    private void OnBallReachDeadZone(Ball ball)
    {
        ball.Hide();
        _balls.Remove(ball);
        Destroy(ball.gameObject);

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        if (_balls.Count == 0)
        {
            //Game over
            ClearBalls();
            
            Debug.Log("Game Over: LOSE!!!");
            ArkanoidEvent.OnGameOverEvent?.Invoke();
        }
    }

    public static void PointsTakeOfPowerUp(int points){
        _totalScore += points;
        ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(points, _totalScore);
    }
    
    private void OnBlockDestroyed(int blockId)
    {
        
        BlockTile blockDestroyed = _gridController.GetBlockBy(blockId);
        if (blockDestroyed != null)
        {
            _totalScore += blockDestroyed.Score;
            ArkanoidEvent.OnScoreUpdatedEvent?.Invoke(blockDestroyed.Score, _totalScore);
            //condicional de probabilidad
                
                int TypePowerUp= (int)(Random.Range(6, 9));
                int type = 0;
                if (TypePowerUp <= 1){
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/50pts");
                    type = 1;
                }
                else if(TypePowerUp <= 2) {
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/100pts");
                    type = 2;
                }
                else if(TypePowerUp <= 3) {
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/250pts");
                    type = 3;
                }
                else if(TypePowerUp <= 4){
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/500pts");
                    type = 4;
                }
                else if(TypePowerUp <= 5){
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/Slow");
                    type = 5;
                }
                else if(TypePowerUp <= 6){
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/Fast");
                    type = 6;
                }
                else if (TypePowerUp <= 7){
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/Small");
                    type = 7;
                }
                else if (TypePowerUp <= 8){
                    myPrefab = Resources.Load<PowerUp>("Prefabs/PowerUps/Large");
                    type = 8;
                }else{
                    Debug.LogError("ERROR NUMBER RANDON DONT EXIST");
                }
                
                PowerUp power = Instantiate(myPrefab, blockDestroyed.transform.position, Quaternion.identity);
                power.setId(type);
        }
        
        
        if (_gridController.GetBlocksActive() == 0)
        {
            _currentLevel++;
            if (_currentLevel >= _levels.Count)
            {
                ClearBalls();
                Debug.LogError("Game Over: WIN!!!!");
            }
            else
            {
                SetInitialBall();
                _gridController.BuildGrid(_levels[_currentLevel]);
            }

        }
    }
}