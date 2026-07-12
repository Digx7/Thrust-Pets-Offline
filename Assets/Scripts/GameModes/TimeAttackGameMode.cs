using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class TimeAttackGameMode : GamePlay
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] protected float startingTime = 120f;
        [SerializeField] protected float currentTime = 120f;
        [SerializeField] protected bool isTimerRunning = false;
        [SerializeField] protected int levelScoreAmount = 100;
        [SerializeField] protected int coinScoreAmount = 10;
        [SerializeField] protected int lifeScoreAmount = 50;
        [SerializeField] protected int secondRemainingScore = 10;

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
        public UnityEvent OnTimerFinishedEvent;

        #endregion

        #region Setup ================================

        public override void Setup()
        {
            base.Setup();

            StartTimer();
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
                currentTime -= Time.deltaTime;
                OnTimerChangedEvent?.Invoke(currentTime);

                if(currentTime <= 0f)
                {
                    currentTime = 0f;
                    isTimerRunning = false;
                    OnTimerFinishedEvent?.Invoke();
                    EndGame(GameEndCondition.Loss);
                }
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
            // Example scoring calculation based on time remaining and coins collected
            int score = 0;
            score += currentLevel * levelScoreAmount;   // Each level is worth 100 points
            score += currentCoins * coinScoreAmount;   // Each coin is worth 10 points
            score += currentLives * lifeScoreAmount;   // Each remaining life is worth 50 points
            score -= Mathf.RoundToInt(currentTime) * secondRemainingScore; // Each second remaining removes 10 points

            if(score < 0) score = 0;

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

        public override void Reset()
        {
            StartTimer();
            base.Reset();
        } 

        #endregion
    }
}