using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Card
    {
        public enum CardSuit { Clubs, Diamonds, Hearts, Spades }

        private int rank;
        private CardSuit suit;
        private int imageIndex;

        public int Rank { get => rank; set => rank = value; }
        internal CardSuit Suit { get => suit; set => suit = value; }
        public int ImageIndex { get => imageIndex; set => imageIndex = value; }

        public Card()
        {

        }

        public Card(int rank, CardSuit suit, int imageIndex)
        {
            this.Rank = rank;
            this.Suit = suit;
            this.ImageIndex = imageIndex;
        }

        //More constructors needed?

        public String getRankSuit()
        {
            // Return a rankSuit string:   "13C"

            String suitFirstLetter = suit.ToString();
            suitFirstLetter = suitFirstLetter.Substring(0, 1);

            String rankString = "";
            rankString = rank.ToString();

            return rankString + suitFirstLetter;

        }

        public int getRank()
        {
            //Returns just the rank as an int for the loop to build the deck
            int rankInt;
            rankInt = rank;
            return rankInt;

        }

    }
}
