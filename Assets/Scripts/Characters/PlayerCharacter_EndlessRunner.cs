using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Digx7.Zygote;
using Digx7.ThrustPets;

public class PlayerCharacter_EndlessRunner : PlayerCharacter 
{

    #region Variables ================================

    [Header("Variables")]
    [SerializeField] int _maxHealth = 3;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if(value is int)
            {
                _maxHealth = value;
                if (_currentHealth > _maxHealth)
                {
                    CurrentHealth = _maxHealth;
                }
            }
        }
    }
    [SerializeField] int _startingHealth = 3;
    [SerializeField] int _currentHealth = 3;
    public int CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        private set
        {
            if(value is int)
            {
                // Set IsDead = true ONLY when current health goes from positive to zero the first time
                // This prevents setting IsDead = true eveytime heal is updated well less than zero
                if(_currentHealth > 0 && value <= 0 && !IsDead)
                {
                    IsDead = true;
                    OnDie.Invoke();
                }
                // Set IsDead = false ONLY when current health goes from zero to positive the first time
                // This prevents setting IsDead = false eveytime heal is updated well greater than zero
                else if(_currentHealth <= 0 && value > 0 && IsDead)
                {
                    IsDead = false;
                }
                
                _currentHealth = value;

                if(_currentHealth < 0)
                {
                    _currentHealth = 0;
                }
                else if(_currentHealth > _maxHealth)
                {
                    _currentHealth = _maxHealth;
                }

                OnCurrentHealthUpdate.Invoke(_currentHealth);
            }
        }
    }
    [SerializeField] bool _isDead = false;
    public bool IsDead
    {
        get
        {
            return _isDead;
        }
        private set
        {
            if(value is bool)
            {
                _isDead = value;
            }
        }
    }

    [Header("References")]
    [SerializeField] Animator animator;
    [SerializeField] LaneMovement laneMovement;
    [SerializeField] Transform playerSkinHolder;
    [SerializeField] PlayerPowerUpComponent playerPowerUpComponent;
    private GameObject playerSkin;

    [Header("Incoming Channels")]
    [CreateScriptableObjectButton("Assets/ScriptableObjects/Channels/GameMode")]
    [SerializeField] 
    protected Channel _request_ResetGame_Channel;

    [Header("Outgoing Events")]
    public IntEvent OnCurrentHealthUpdate;
    public UnityEvent OnDie;

    public bool IsInvincible = false;

    #endregion

    #region Setup ================================

    public override void Setup(int newID = 0)
    {
        _request_ResetGame_Channel.channelEvent.AddListener(OnReceive_RequestReset);

        CurrentHealth = _startingHealth;
        
        playerSkin = Instantiate(PlayerDataManager.Instance.PlayerSkin.RuntimePrefab, playerSkinHolder);
        animator = playerSkin.GetComponent<Animator>();

        animator.SetBool("GamePlay", true);

        
        base.Setup(newID);
    }

    public override void Teardown()
    {
        _request_ResetGame_Channel.channelEvent.RemoveListener(OnReceive_RequestReset);

        base.Teardown();
    }

    #endregion

    #region Channel Respons Methods ===================

    public virtual void OnReceive_RequestReset()
    {
        CurrentHealth = _startingHealth;
    }

    #endregion

    #region  Main Functions ================================

    public override void UpdateDesiredMoveDirection(Vector2 newDesiredDirection)
    {
        base.UpdateDesiredMoveDirection(newDesiredDirection);

        laneMovement.TryChangeLanes(desiredMoveDirection.x);
    }

    public void TakeDamage()
    {
        if(IsDead || IsInvincible) return;

        // Debug.Log($"PlayerCharacter_EndlessRunner: TakeDamage() IsInvincible = {IsInvincible}");

        CurrentHealth--;
        StartCoroutine(StopAndStartPlayer(1f));
    }

    public void Stuck()
    {
        if(IsDead || IsInvincible) return;
        
        // Debug.Log($"PlayerCharacter_EndlessRunner: Stuck() IsInvincible = {IsInvincible}");
        
        StartCoroutine(StopAndStartPlayer(3f));
    }

    public void GiveCoin()
    {
        if(IsDead) return;

    }

    public void TryToUsePowerUp()
    {
        playerPowerUpComponent.TryUsePowerUp();
    }

    IEnumerator StopAndStartPlayer(float timeDelay) 
    {
        laneMovement.enabled = false;
        animator.SetBool("Stuck", true);

        // playerMeshRenderer.material = invisibleMaterials[1];

        // yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(timeDelay);

        Vector3 pos = transform.position;
        pos.z += 1.7f;
        transform.position = pos;

        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[2];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[3];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[1];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[2];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[3];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[1];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[2];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[3];
        // yield return new WaitForSeconds(0.1f);
        // playerMeshRenderer.material = invisibleMaterials[0];

        if (!IsDead) 
        {
            laneMovement.enabled = true;
            animator.SetBool("Stuck", false);
        }
    }

    #endregion
}