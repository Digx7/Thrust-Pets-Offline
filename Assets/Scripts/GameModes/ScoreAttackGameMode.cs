using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ScoreAttackGameMode : GamePlay
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] protected float startingTime = 0f;
        [SerializeField] protected float currentTime = 0f;
        [SerializeField] protected bool isTimerRunning = false;
        [SerializeField] protected int coinGoal = 10;
        [SerializeField] protected int levelScoreAmount = 100;
        [SerializeField] protected int coinScoreAmount = 10;
        [SerializeField] protected int missedCoinScoreAmount = 5;
        [SerializeField] protected int lifeScoreAmount = 50;
        [SerializeField] protected int timeScoreCap = 10000;
        [SerializeField] protected int timeScoreScaler = 10;

        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        protected Channel _request_StartTimer_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        protected Channel _request_StopTimer_Channel;

        [Header("Outgoing Events")]
        [SerializeField] 
        public FloatEvent OnTimerChangedEvent;
        public IntEvent OnCoinGoalChangedEvent;

        #endregion

        #region Setup ================================

        public override void Setup()
        {
            base.Setup();

            OnCoinGoalChangedEvent?.Invoke(coinGoal);

            StartTimer();
        }

        public override void Teardown()
        {
            OnCoinGoalChangedEvent?.Invoke(0);

            base.Teardown();
        }

        protected override void SetupChannels()
        {
            base.SetupChannels();

            _request_StartTimer_Channel.channelEvent.AddListener(OnRecieve_RequestStartTimer);
            _request_StopTimer_Channel.channelEvent.AddListener(OnRecieve_RequestStopTimer);
        }

        protected override void TearDownChannels()
        {
            base.TearDownChannels();

            _request_StartTimer_Channel.channelEvent.RemoveListener(OnRecieve_RequestStartTimer);
            _request_StopTimer_Channel.channelEvent.RemoveListener(OnRecieve_RequestStopTimer);
        }
        
        #endregion

        #region Channel Responses ================================

        public void OnRecieve_RequestStartTimer()
        {
            isTimerRunning = true;
        }

        public void OnRecieve_RequestStopTimer()
        {
            isTimerRunning = false;
        }

        #endregion

        #region Main Functions ================================

        public virtual void Update()
        {
            if(isTimerRunning)
            {
                currentTime += Time.deltaTime;
                OnTimerChangedEvent?.Invoke(currentTime);
            }
        }

        public override void IncreaseCoins(int amount)
        {
            base.IncreaseCoins(amount);

            if(currentCoins >= coinGoal)
            {
                StopTimer();
                EndGame(GameEndCondition.Win);
            }
        }

        public virtual void StartTimer()
        {
            currentTime = startingTime;
            isTimerRunning = true;
            OnTimerChangedEvent?.Invoke(currentTime);
        }

        public virtual void StopTimer()
        {
            isTimerRunning = false;
        }

        public override int CalculateEndGameScoreResult()
        {
            // Example scoring calculation based on time and coins
            int score = 0;
            score += currentLevel * levelScoreAmount;
            score += currentCoins * coinScoreAmount;   
            score += currentLives * lifeScoreAmount;   

            if(currentCoins >= coinGoal)
            {
                score += Mathf.Max(0, (int)(timeScoreCap - currentTime * timeScoreScaler)); // Faster times are worth more points, with a cap 
            }
            else
            {
                score -= (coinGoal - currentCoins) * missedCoinScoreAmount; // Each coin short of the goal reduces score
            }

            return score;
        }

        public override float CalculateEndGameTimeResult()
        {
            return currentTime;
        }

        public override void EndGame(GameEndCondition endCondition)
        {
            StopTimer();
            base.EndGame(endCondition);
        }

        #endregion
    }
}