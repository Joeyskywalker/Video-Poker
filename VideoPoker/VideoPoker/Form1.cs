using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoPoker
{
    public partial class Form1 : Form
    {
        Game pokerGame = new Game();
        Deck pokerDeck = new Deck();
        List<Card> cardsOnDisplay = new List<Card>();

        List<PictureBox> cardPictures = new List<PictureBox>();
        List<CheckBox> holdButtons = new List<CheckBox>();

        public Form1()
        {
            InitializeComponent();
            cardPictures.Add(pictureBoxCard1);
            cardPictures.Add(pictureBoxCard2);
            cardPictures.Add(pictureBoxCard3);
            cardPictures.Add(pictureBoxCard4);
            cardPictures.Add(pictureBoxCard5);

            holdButtons.Add(checkBoxHold1);
            holdButtons.Add(checkBoxHold2);
            holdButtons.Add(checkBoxHold3);
            holdButtons.Add(checkBoxHold4);
            holdButtons.Add(checkBoxHold5);

            pokerDeck.shuffleDeck();
            //showFiveNewCards();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {

            

            //Each IF statement is preparing the next round- Think ahead

            if(pokerGame.GameStatus == Game.gameState.initalBet)
            {
                

                //Steps to take during the first round, enable hold buttons
                int bet = Convert.ToInt32(numericUpDownBet.Value);
                pokerGame.setBetAmount(bet);
                numericUpDownBet.Enabled = false;
                numericUpDownBet.Value = pokerGame.PlayerBet;

                //Grab and block inital bet
                labelPayout.Text = "Hold onto the good cards";
                showFiveNewCards();

                changeHoldButtons();//Must be the last step

            }
            else if(pokerGame.GameStatus == Game.gameState.holdStage)
            {
                //Disable hold buttons, calculate winnings
                showFiveNewCards();

                //Take 5 cards and get the score
                string card1 = cardsOnDisplay[0].getRankSuit();
                string card2 = cardsOnDisplay[1].getRankSuit();
                string card3 = cardsOnDisplay[2].getRankSuit();
                string card4 = cardsOnDisplay[3].getRankSuit();
                string card5 = cardsOnDisplay[4].getRankSuit();

                PokerScore finalHand = new PokerScore(card1, card2, card3, card4, card5);
                labelPayout.Text = finalHand.scoreHand();
                pokerGame.addWinnings(finalHand.getPayoffRatio());

                
                labelPlayerBalance.Text = pokerGame.getPlayerBalanceMessage(); // Do AFTER calculating winnings

                checkForMoney();
                changeHoldButtons();//Must be the last step
            }
            else if (pokerGame.GameStatus == Game.gameState.winnings)
            {
                //Process any winnings, set up next game
                labelPayout.Text = "Place Your Bet";
                pokerDeck.shuffleDeck();
                showJokers();
                numericUpDownBet.Enabled = true;
            }

                pokerGame.nextRound();
        }

        private void showFiveNewCards()
        {
            List<Card> heldCards = new List<Card>();

            //Take note of cards on hold to readd later
            for (int i = 0; i < 5; i++)
            {
                if (holdButtons[i].Checked == true)
                {
                    heldCards.Add(cardsOnDisplay[i]);
                }
            }

                cardsOnDisplay.Clear();


            for (int i = 0; i < 5; i++)
            {
                if (holdButtons[i].Checked == false)
                {
                    
                    Card cardToAdd = pokerDeck.dealRandomCard();
                    cardsOnDisplay.Add(cardToAdd);

                    int cardImage = cardToAdd.ImageIndex;

                    cardPictures[i].Image = imageListCards.Images[cardImage];
                }
                else if (holdButtons[i].Checked == true)
                {
                    //get the card from the image
                    cardsOnDisplay.Add(heldCards[0]);
                    heldCards.RemoveAt(0);
                }
            }
        }

        private void changeHoldButtons()
        {
            if(checkBoxHold1.Enabled == true)
            {
                //Disable them
                for (int i = 0; i < 5; i++)
                {
                    holdButtons[i].Enabled = false;
                    holdButtons[i].Checked = false; // Maybe move this somewhere else, make it less confusing to see
                }
            }
            else
            {
                //Enable them
                for (int i = 0; i < 5; i++)
                {
                    holdButtons[i].Enabled = true;
                }
            }
            
        }

        private void showJokers()
        {
            cardPictures[0].Image = VideoPoker.Properties.Resources.Joker_Black;
            cardPictures[1].Image = VideoPoker.Properties.Resources.Joker_Red1;
            cardPictures[2].Image = VideoPoker.Properties.Resources.Joker_Black;
            cardPictures[3].Image = VideoPoker.Properties.Resources.Joker_Red1;
            cardPictures[4].Image = VideoPoker.Properties.Resources.Joker_Black;
            cardsOnDisplay.Clear();
        }

        private void checkForMoney()
        {
            //Stop a player if they have no money
            if(pokerGame.isPlayerBroke() == true)
            {
                buttonContinue.Enabled = false;
                labelBroke.Visible = true;
            }
        }
    }
}
