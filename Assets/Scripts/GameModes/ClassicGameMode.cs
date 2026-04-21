using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ClassicGameMode : GameMode
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] private UIWidgetData _pauseMenuWidgetData;
        [SerializeField] private UIWidgetData _levelWidgetData;
        [SerializeField] private int currentLevel = 1;
        [SerializeField] private int maxLevel = 100;
        [SerializeField] private int currentScore = 0;
        [SerializeField] private int currentLives = 3;
        [SerializeField] private int currentHealth = 100;
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int currentCoins = 0;
        
        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        IntChannel _request_IncreaseLevel_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        IntChannel _request_IncreaseScore_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        IntChannel _request_DecreaseLives_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        IntChannel _request_UpdateHealth_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        IntChannel _request_IncreaseCoins_Channel;

        [Header("Outgoing Events")]
        public UIWidgetDataEvent OnRequestLoadUIWidgetDataEvent;
        public UnityEvent OnRequestLoadSaveDataEvent;
        public UnityEvent OnGameStartEvent;
        public GameEndResultEvent OnGameEndEvent;
        public IntEvent OnLevelChangedEvent;
        public IntEvent OnScoreChangedEvent;
        public IntEvent OnLivesChangedEvent;
        public IntEvent OnHealthChangedEvent;
        public IntEvent OnCoinsChangedEvent;


        #endregion

        #region Setup ================================

        public override void Setup()
        {
            // add code here
            OnGameStartEvent?.Invoke();
            
            base.Setup();

            OnRequestLoadSaveDataEvent?.Invoke();

            if(_levelWidgetData != null)
            {
                OnRequestLoadUIWidgetDataEvent?.Invoke(_levelWidgetData);
            }
        }

        public override void Teardown()
        {
            // add code here
            
            base.Teardown();
        }

        protected override void SetupChannels()
        {
            base.SetupChannels();
            _request_IncreaseLevel_Channel.channelEvent.AddListener(OnRecieve_RequestIncreaseLevel);
            _request_IncreaseScore_Channel.channelEvent.AddListener(OnRecieve_RequestIncreaseScore);
            _request_DecreaseLives_Channel.channelEvent.AddListener(OnRecieve_RequestDecreaseLives);
            _request_UpdateHealth_Channel.channelEvent.AddListener(OnRecieve_RequestUpdateHealth);
            _request_IncreaseCoins_Channel.channelEvent.AddListener(OnRecieve_RequestIncreaseCoins);
        }

        protected override void TearDownChannels()
        {
            base.TearDownChannels();
            _request_IncreaseLevel_Channel.channelEvent.RemoveListener(OnRecieve_RequestIncreaseLevel);
            _request_IncreaseScore_Channel.channelEvent.RemoveListener(OnRecieve_RequestIncreaseScore);
            _request_DecreaseLives_Channel.channelEvent.RemoveListener(OnRecieve_RequestDecreaseLives);
            _request_UpdateHealth_Channel.channelEvent.RemoveListener(OnRecieve_RequestUpdateHealth);
            _request_IncreaseCoins_Channel.channelEvent.RemoveListener(OnRecieve_RequestIncreaseCoins);
        }

        #endregion

        #region Channel Responses ================================
        
        protected void OnRecieve_RequestIncreaseLevel(int amount)
        {
            IncreaseLevel(amount);
        }

        protected void OnRecieve_RequestIncreaseScore(int amount)
        {
            IncreaseScore(amount);
        }

        protected void OnRecieve_RequestDecreaseLives(int amount)
        {
            DecreaseLives(amount);
        }

        protected void OnRecieve_RequestUpdateHealth(int amount)
        {
            UpdateHealth(amount);
        }

        protected void OnRecieve_RequestIncreaseCoins(int amount)
        {
            IncreaseCoins(amount);
        }
        
        protected override void OnRecieve_OnOptionsMenuQuit()
        {
            OnRequestLoadUIWidgetDataEvent?.Invoke(_pauseMenuWidgetData);
        }

        #endregion

        #region Main Functions ================================

        public void IncreaseLevel(int amount)
        {
            currentLevel += amount;
            if (currentLevel > maxLevel)
            {
                currentLevel = maxLevel;
            }
            OnLevelChangedEvent?.Invoke(currentLevel);
        }

        public void IncreaseScore(int amount)
        {
            currentScore += amount;
            OnScoreChangedEvent?.Invoke(currentScore);
        }

        public void DecreaseLives(int amount)
        {
            currentLives -= amount;
            if (currentLives < 0)
            {
                currentLives = 0;
                EndGame(GameEndCondition.Win);
            }
            OnLivesChangedEvent?.Invoke(currentLives);
        }

        public void UpdateHealth(int amount)
        {
            currentHealth += amount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth < 0)
            {
                currentHealth = 0;

                EndGame(GameEndCondition.Win);
            }
            OnHealthChangedEvent?.Invoke(currentHealth);
        }

        public void IncreaseCoins(int amount)
        {
            currentCoins += amount;
            OnCoinsChangedEvent?.Invoke(currentCoins);
        }

        public void EndGame(GameEndCondition endCondition)
        {
            GameEndResult result = new GameEndResult
            {
                endCondition = endCondition,
                levelReached = currentLevel,
                score = currentScore,
                coins = currentCoins
            };
            OnGameEndEvent?.Invoke(result);
        }

        #endregion
    }
}