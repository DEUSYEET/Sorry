using System;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Generate Cards for the Deck, Stores them and has a way of drawing the cards
/// </summary>

namespace Sorry
{
    public class CardDeck
    {
        //Class Level Variables
        private List<Card> cardDeck = new List<Card>();
        private List<Card> discardDeck = new List<Card>();
        private int count = 0;
        private int MAX_VALUE = 11;
        private int MIN_VALUE = 1;
        public enum Card { One, Two, Three, Four, Five, Seven, Eight, Ten, Eleven, Twelve, Sorry };

        public CardDeck()
        {
            generateAndStoreCards(Card.One);
            generateAndStoreCards(Card.Two);
            generateAndStoreCards(Card.Three);
            generateAndStoreCards(Card.Four);
            generateAndStoreCards(Card.Five);
            generateAndStoreCards(Card.Seven);
            generateAndStoreCards(Card.Eight);
            generateAndStoreCards(Card.Ten);
            generateAndStoreCards(Card.Eleven);
            generateAndStoreCards(Card.Twelve);
            generateAndStoreCards(Card.Sorry);

        }

        public void generateAndStoreCards(Card card)
        {
            for (int i = count; i < (count + 4); i++)
            {
                cardDeck.Add(card);
            }
            shuffle();
            count += 4;
        }

        public void shuffle()
        {
            Random rand = new Random();
            int size = cardDeck.Count;
            while (size > 1)
            {
                size--;
                int index = rand.Next(size + 1);
                Card value = cardDeck[index];
                cardDeck[index] = cardDeck[size];
                cardDeck[size] = value;
            }
        }

        public void reshuffle()
        {
            for (int i = 0; i < discardDeck.Count; i++)
            {
                Random rand = new Random();
                int value = 0;
                value = rand.Next(discardDeck.Count);
                addToCardDeck(value);
            }
        }

        public void addToCardDeck(int index)
        {
            Card newCard = discardDeck[index];
            discardDeck.RemoveAt(index);
            cardDeck.Add(newCard);
        }

        public Card rdrawCard(Card card)
        {
            Card newcard;
            if (card == Card.One)
            {
                cardDeck.Remove(Card.One);
                discardDeck.Add(Card.One);
                newcard = Card.One;
                return newcard;
            }
            else if (card == Card.Two)
            {
                cardDeck.Remove(Card.Two);
                discardDeck.Add(Card.Two);
                newcard = Card.Two;
                return newcard;
            }
            else if (card == Card.Three)
            {
                cardDeck.Remove(Card.Three);
                discardDeck.Add(Card.Three);
                newcard = Card.Three;
                return newcard;
            }
            else if (card == Card.Four)
            {
                cardDeck.Remove(Card.Four);
                discardDeck.Add(Card.Four);
                newcard = Card.Four;
                return newcard;
            }
            else if (card == Card.Five)
            {
                cardDeck.Remove(Card.Five);
                discardDeck.Add(Card.Five);
                newcard = Card.Five;
                return newcard;
            }
            else if (card == Card.Seven)
            {
                cardDeck.Remove(Card.Seven);
                discardDeck.Add(Card.Seven);
                newcard = Card.Seven;
                return newcard;
            }
            else if (card == Card.Eight)
            {
                cardDeck.Remove(Card.Eight);
                discardDeck.Add(Card.Eight);
                newcard = Card.Eight;
                return newcard;
            }
            else if (card == Card.Ten)
            {
                cardDeck.Remove(Card.Ten);
                discardDeck.Add(Card.Ten);
                newcard = Card.Ten;
                return newcard;
            }
            else if (card == Card.Eleven)
            {
                cardDeck.Remove(Card.Eleven);
                discardDeck.Add(Card.Eleven);
                newcard = Card.Eleven;
                return newcard;
            }
            else 
            {
                cardDeck.Remove(Card.Sorry);
                discardDeck.Add(Card.Sorry);
                newcard = Card.Sorry;
                return newcard;
            }
        }
    }
}