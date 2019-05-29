using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Deck
    {
        private List<Card> listDeck = new List<Card>();

        public Deck()
        {
            buildDeck();
        }

        public void shuffleDeck()
        {
            //Rebuild the deck
            buildDeck();
        }

        public Card drawSpecificRankCard(int rankToRemove)
        {
            //This is for testing, probably won't use in the final product
            Card card = new Card();
            for (int i = 0; i < listDeck.Count; i++)
            {
                if (listDeck[i].getRank() == rankToRemove)
                {
                    card = (Card)listDeck[i];
                    listDeck.RemoveAt(i);
                    break;  // We found the value, exit the loop
                }
            }

            return card;
        }

        public Card dealRandomCard()
        {
            //Draw a random card and return it, like in Blackjack
            Card card = new Card();
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            //NOT RANDOM

            int randNumber = rand.Next(listDeck.Count()-1); //-1 to account for the loop starting at 0

            card = (Card)listDeck[randNumber];
            listDeck.RemoveAt(randNumber);

            return card;
        }

        private void buildDeck()
        {
            //Create the deck
            //Same as blackjack except suit is enum
            //Builds and rebuilds deck, same method
            listDeck.Clear();
            // Imaga list order,  AC, AD, AH, AS, 2C 2D, 2H, 2S, 3, 4.... K   
            int imgCounter = 0;
            int rank = 1;

            for (int i = 0; i < 13; i++)
            {
                Card card = new Card(rank, Card.CardSuit.Clubs, imgCounter);
                listDeck.Add(card);
                imgCounter++;

                card = new Card(rank, Card.CardSuit.Diamonds, imgCounter);
                listDeck.Add(card);
                imgCounter++;

                card = new Card(rank, Card.CardSuit.Hearts, imgCounter);
                listDeck.Add(card);
                imgCounter++;

                card = new Card(rank, Card.CardSuit.Spades, imgCounter);
                listDeck.Add(card);
                imgCounter++;

                // all four suits added
                rank++;
            }
        }
    }
}
