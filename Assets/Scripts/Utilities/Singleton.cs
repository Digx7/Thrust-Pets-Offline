using UnityEngine;

namespace Digx7.Zygote
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Variables ============================
        public static T Instance { get; private set; }

        protected bool isSafeInstance = false;

        #endregion

        
        #region Main Methods ============================
        protected virtual bool CheckIfSafe()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning("Another Singleton " + Instance + " was found so deleting this one");
                Destroy(this.gameObject);
                return false;
            }
            else
            {
                isSafeInstance = true;
                DontDestroyOnLoad(this);

                Instance = this as T;
                return true;
            }
        }
        
        #endregion

        #region Unity Methods ============================
        public virtual void Awake()
        {
            if (CheckIfSafe())
            {
                SafeAwake();
            }
        }

        public virtual void OnEnable()
        {
            if (CheckIfSafe())
            {
                SafeOnEnable();
            }
        }

        public virtual void OnDisable()
        {
            if (isSafeInstance)
            {
                SafeOnDisable();
            }
        }

        public virtual void Start()
        {
            if (isSafeInstance)
            {
                SafeStart();
            }
        }

        public virtual void Update()
        {
            if (isSafeInstance)
            {
                SafeUpdate();
            }
        }

        #endregion

        #region Safe Methods ============================

        // Only called if this singleton is the valid Instance
        // Allows us to use Awake() in its normal call order without dealing with the edge case of multiple Instances existing at once.
        public virtual void SafeAwake()
        {
            
        }

        public virtual void SafeOnEnable()
        {
            
        }

        public virtual void SafeOnDisable()
        {
            
        }

        public virtual void SafeStart()
        {
            
        }

        public virtual void SafeUpdate()
        {
            
        }
    
        #endregion
    }
}
