using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ScoreAttackGameMode : GamePlay
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] protected float startingTime = 120f;
        [SerializeField] protected float currentTime = 120f;
        [SerializeField] protected bool isTimerRunning = false;

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

        #endregion
    }
}