using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class GamePlay : GameMode
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] protected UIWidgetData _pauseMenuWidgetData;
        [SerializeField] protected UIWidgetData _levelWidgetData;
        [SerializeField] protected UIWidgetData _gameOverWidgetData;
        [SerializeField] protected int startingLevel = 1;
        [SerializeField] protected int currentLevel = 1;
        [SerializeField] protected int maxLevel = 100;
        [SerializeField] protected int startingScore = 0;
        [SerializeField] protected int currentScore = 0;
        [SerializeField] protected int startingLives = 3;
        [SerializeField] protected int currentLives = 3;
        [SerializeField] protected int startingCoins = 0;
        [SerializeField] protected int currentCoins = 0;
        
        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        protected IntChannel _request_IncreaseLevel_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        protected IntChannel _request_IncreaseScore_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        protected IntChannel _request_DecreaseLives_Channel;
        [SerializeField] Channel _On_PlayerDied_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        protected IntChannel _request_IncreaseCoins_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
        [SerializeField] 
        protected Channel _request_ResetGame_Channel;

        [Header("Outgoing Events")]
        public UIWidgetDataEvent OnRequestLoadUIWidgetDataEvent;
        public UIWidgetDataEvent OnRequestUnLoadUIWidgetDataEvent;
        public UnityEvent OnRequestLoadSaveDataEvent;
        public UnityEvent OnGameStartEvent;
        public GameEndResultEvent OnGameEndEvent;
        public IntEvent OnLevelChangedEvent;
        public IntEvent OnScoreChangedEvent;
        public IntEvent OnLivesChangedEvent;
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
            _request_IncreaseCoins_Channel.channelEvent.AddListener(OnRecieve_RequestIncreaseCoins);
            _On_PlayerDied_Channel.channelEvent.AddListener(OnRevieve_OnPlayerDied);
            _request_ResetGame_Channel.channelEvent.AddListener(OnRevieve_RequestResetGame);
        }

        protected override void TearDownChannels()
        {
            base.TearDownChannels();
            _request_IncreaseLevel_Channel.channelEvent.RemoveListener(OnRecieve_RequestIncreaseLevel);
            _request_IncreaseScore_Channel.channelEvent.RemoveListener(OnRecieve_RequestIncreaseScore);
            _request_DecreaseLives_Channel.channelEvent.RemoveListener(OnRecieve_RequestDecreaseLives);
            _request_IncreaseCoins_Channel.channelEvent.RemoveListener(OnRecieve_RequestIncreaseCoins);
            _On_PlayerDied_Channel.channelEvent.RemoveListener(OnRevieve_OnPlayerDied);
            _request_ResetGame_Channel.channelEvent.RemoveListener(OnRevieve_RequestResetGame);
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

        protected void OnRecieve_RequestIncreaseCoins(int amount)
        {
            IncreaseCoins(amount);
        }

        protected void OnRevieve_OnPlayerDied()
        {
            EndGame(GameEndCondition.Loss);
        }
        
        protected override void OnRecieve_OnOptionsMenuQuit()
        {
            OnRequestLoadUIWidgetDataEvent?.Invoke(_pauseMenuWidgetData);
        }

        protected virtual void OnRevieve_RequestResetGame()
        {
            Reset();
        }

        #endregion

        #region Main Functions ================================

        public virtual void IncreaseLevel(int amount)
        {
            currentLevel += amount;
            if (currentLevel > maxLevel)
            {
                currentLevel = maxLevel;
            }
            OnLevelChangedEvent?.Invoke(currentLevel);
        }

        public virtual void IncreaseScore(int amount)
        {
            currentScore += amount;
            OnScoreChangedEvent?.Invoke(currentScore);
        }

        public virtual void DecreaseLives(int amount)
        {
            currentLives -= amount;
            if (currentLives < 0)
            {
                currentLives = 0;
                EndGame(GameEndCondition.Win);
            }
            OnLivesChangedEvent?.Invoke(currentLives);
        }

        public virtual void IncreaseCoins(int amount)
        {
            currentCoins += amount;
            OnCoinsChangedEvent?.Invoke(currentCoins);
        }

        public virtual void EndGame(GameEndCondition endCondition)
        {
            GameEndResult result = new GameEndResult
            {
                endCondition = endCondition,
                levelReached = currentLevel,
                score = currentScore,
                coins = currentCoins
            };
            OnGameEndEvent?.Invoke(result);
            OnRequestUnLoadUIWidgetDataEvent?.Invoke(_levelWidgetData);
            OnRequestLoadUIWidgetDataEvent?.Invoke(_gameOverWidgetData);
        }

        public virtual void Reset()
        {
            currentLevel = startingLevel;
            currentScore = startingScore;
            currentLives = startingLives;
            currentCoins = startingCoins;

            if(_levelWidgetData != null)
            {
                OnRequestLoadUIWidgetDataEvent?.Invoke(_levelWidgetData);
            }
        }

        #endregion
    }
}