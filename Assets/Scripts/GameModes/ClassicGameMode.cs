using UnityEngine;
using UnityEngine.Events;

namespace Digx7.Zygote
{
    public class ClassicGameMode : GamePlay
    {
        #region Variables ================================

        [Header("Variables")]
        [SerializeField] protected int levelScoreAmount = 100;
        [SerializeField] protected int coinScoreAmount = 10;
        [SerializeField] protected int lifeScoreAmount = 50;


        #endregion

        #region Setup ================================

        

        #endregion

        #region Channel Responses ================================
        
        

        #endregion

        #region Main Functions ================================

        public override int CalculateEndGameScoreResult()
        {
            // Example scoring calculation based on level, coins, and lives
            int score = 0;
            score += currentLevel * levelScoreAmount; // Each level is worth 100 points
            score += currentCoins * coinScoreAmount;   // Each coin is worth 10 points
            score += currentLives * lifeScoreAmount;   // Each remaining life is worth 50 points
            return score;
        }

        #endregion
    }
}