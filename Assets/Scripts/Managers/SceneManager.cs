using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

// Change
namespace Digx7.Zygote
{
    public class SceneManager : Singleton<SceneManager>
    {
        #region Variables ================================
        
        // [Header("Variables")]
        [Header("Incoming Channels")]
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/SceneManager")]
        [SerializeField] 
        private SceneDataChannel _request_changeSceneData_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/SceneManager")]
        [SerializeField] 
        private SceneDataChannel _request_addSceneData_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/SceneManager")]
        [SerializeField] 
        private SceneDataChannel _request_removeSceneData_Channel;
        [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/SceneManager")]
        [SerializeField] 
        private SceneDataChannel _request_setActiveScene_Channel;

        [Header("Outgoing Events")]
        public UnityEvent OnChangeSceneEvent;
        public UnityEvent OnChangeSceneFinishedEvent;
        public SceneContextEvent OnUpdateSceneContextEvent;

        private bool onChangeSceneCoroutineIsGoing = false;
        private bool onChangeSceneFinishedCoroutineIsGoing = false;
        
        #endregion

        #region Setup ================================

        public override void SafeOnEnable()
        {
            SetupChannels();
        }

        public override void SafeOnDisable()
        {
            TearDownChannels();
        }

        private void SetupChannels()
        {
            _request_changeSceneData_Channel.channelEvent.AddListener(OnRecieve_OnChangeScene);
            _request_addSceneData_Channel.channelEvent.AddListener(OnRecieve_OnAddScene);
            _request_removeSceneData_Channel.channelEvent.AddListener(OnRecieve_OnUnloadScene);
            _request_setActiveScene_Channel.channelEvent.AddListener(OnRecieve_OnSetActiveScene);

            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnRecieve_OnAcitveSceneChanged;
        }

        private void TearDownChannels()
        {
            _request_changeSceneData_Channel.channelEvent.RemoveListener(OnRecieve_OnChangeScene);
            _request_addSceneData_Channel.channelEvent.RemoveListener(OnRecieve_OnAddScene);
            _request_removeSceneData_Channel.channelEvent.RemoveListener(OnRecieve_OnUnloadScene);
            _request_setActiveScene_Channel.channelEvent.RemoveListener(OnRecieve_OnSetActiveScene);
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnRecieve_OnAcitveSceneChanged;
        }

        #endregion

        #region Channel Responses ================================

        protected void OnRecieve_OnChangeScene(SceneData data)
        {
            if(onChangeSceneCoroutineIsGoing) return;

            Debug.Log("SceneManager: OnChangeScene");
            OnChangeSceneEvent.Invoke();
            StartCoroutine(OnChangeSceneCoroutine(data));
        }

        protected void OnRecieve_OnAddScene(SceneData data)
        {
            OnChangeSceneEvent.Invoke();
            
            LoadSceneMode mode = LoadSceneMode.Additive;
            UpdateContext(data.context);
            LoadScene(data.sceneName, mode);
        }

        protected void OnRecieve_OnUnloadScene(SceneData data)
        {
            UnloadScene(data);
        }

        protected void OnRecieve_OnAcitveSceneChanged(Scene current, Scene next)
        {
            if(onChangeSceneFinishedCoroutineIsGoing) return;

            StartCoroutine(OnChangeSceneFinishedCoroutine());
        }

        protected void OnRecieve_OnSetActiveScene(SceneData data)
        {
            SetActiveScene(data);
        }

        #endregion

        #region Main Functions ================================

        private void LoadScene(string name, LoadSceneMode mode = LoadSceneMode.Single)
        {
            Debug.Log($"SceneManger: LoadScene({name}, {mode})");
            // UnityEngine.SceneManagement.SceneManager.LoadScene(name, mode);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name, mode);
        }

        private void UnloadScene(SceneData data)
        {
            Debug.Log($"MenuIssue: SceneManger: UnloadScene({data.sceneName})");
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(data.sceneName);
        }

        private void UpdateContext(SceneContext newContext)
        {
            OnUpdateSceneContextEvent.Invoke(newContext);
        }

        private void SetActiveScene(SceneData data)
        {
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(data.sceneName);
            if(scene.IsValid())
            {
                UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
            }
            else
            {
                Debug.LogWarning($"SceneManager: SetActiveScene: Scene {data.sceneName} is not valid");
            }
        }

        // COROUTINES ======================================

        private IEnumerator OnChangeSceneCoroutine(SceneData data)
        {
            onChangeSceneCoroutineIsGoing = true;
            yield return new WaitForSeconds(0.5f);
            UpdateContext(data.context);
            LoadScene(data.sceneName);
            onChangeSceneCoroutineIsGoing = false;
        }

        private IEnumerator OnChangeSceneFinishedCoroutine()
        {
            onChangeSceneFinishedCoroutineIsGoing = true;
            yield return new WaitForSeconds(0.5f);
            OnChangeSceneFinishedEvent.Invoke();
            onChangeSceneFinishedCoroutineIsGoing = false;
        }

        #endregion
    }
}
