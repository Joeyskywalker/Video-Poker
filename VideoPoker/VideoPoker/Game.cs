using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Game
    {
        public enum gameState { initalBet, holdStage, winnings}

        private gameState gameStatus;
        private int playerBalance;
        private int playerBet;

        public gameState GameStatus { get => gameStatus; }
        public int PlayerBalance { get => playerBalance; set => playerBalance = value; }
        public int PlayerBet { get => playerBet; }

        public Game()
        {
            gameStatus = gameState.initalBet;
            playerBalance = 100;
        }

        public int setBetAmount(int requestedAmount)
        {
            int allowedAmount = 0;

            if(requestedAmount > playerBalance)
            {
                allowedAmount = playerBalance;
                playerBalance -= allowedAmount;
            }
            else
            {
                allowedAmount = requestedAmount;
                playerBalance -= allowedAmount;
            }

            playerBet = allowedAmount;
            return allowedAmount;
        }

        public void nextRound()
        {
            if(gameStatus == gameState.initalBet)
            {
                gameStatus = gameState.holdStage;
            }
            else if (gameStatus == gameState.holdStage)
            {
                gameStatus = gameState.winnings;
            }
            else if(gameStatus == gameState.winnings)
            {
                gameStatus = gameState.initalBet;
                //playerBet = 0; //I have a feeling this will cause issues here
            }

        }

        public void addWinnings(int winRatio)
        {
            int winnings = 0;

            winnings = playerBet * winRatio;
            playerBalance += winnings;
            
        }

        public string getPlayerBalanceMessage()
        {
            string message;
            message = "You have $" + playerBalance.ToString() + " to bet";
            return message;
        }

        public bool isPlayerBroke()
        {
            bool broke = false;
            if (playerBalance == 0)
            {
                broke = true;
            }
            return broke;
        }
    }
}
