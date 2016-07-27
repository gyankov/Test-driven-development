using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Poker;

namespace PokerTests
{
    [TestFixture]
    public class TestPoker
    {

        [Test]
        public void CardsToStringShouldNotReturnNull()
        {
            var card = new Card(CardFace.Ace, CardSuit.Clubs);
            var cardToString = card.ToString();

            Assert.IsNotNull(cardToString);
        }

        [Test]
        public void CardsToStringShouldNotReturnEmptyString()
        {
            var card = new Card(CardFace.Eight, CardSuit.Clubs);

            Assert.IsNotEmpty(card.ToString());
        }

        [Test]
        public void CardsToStringShouldReturnString()
        {
            var card = new Card(CardFace.Five, CardSuit.Diamonds);
            var returnTypeOfToString = card.ToString().GetType().Name;
            var expectedResult = "gosho".GetType().Name;

            Assert.AreEqual(expectedResult, returnTypeOfToString);

        }

        [Test]
        public void CardsToStringShouldWorkProperly()
        {
            var card = new Card(CardFace.Ace, CardSuit.Hearts);
            var expectedResult = "Ace of Hearts";

            Assert.AreEqual(expectedResult, card.ToString());
        }

        [Test]
        public void HandsToStringShouldNotReturnNull()
        {
            var cards = new List<ICard>()
            {
               new Card(CardFace.Ace, CardSuit.Diamonds),
               new Card(CardFace.Four, CardSuit.Hearts),
               new Card(CardFace.King, CardSuit.Spades),
               new Card(CardFace.Queen, CardSuit.Clubs),

            };

            var hand = new Hand(cards);

            Assert.IsNotNull(hand.ToString());

        }

        [Test]
        public void HandsToStringShouldNotReturnEmpty()
        {
            var cards = new List<ICard>()
            {
               new Card(CardFace.Ace, CardSuit.Diamonds),
               new Card(CardFace.Four, CardSuit.Hearts),
               new Card(CardFace.King, CardSuit.Spades),
               new Card(CardFace.Queen, CardSuit.Clubs),

            };

            var hand = new Hand(cards);

            Assert.IsNotEmpty(hand.ToString());

        }

        [Test]
        public void HandsToStringShouldReturnString()
        {
            var cards = new List<ICard>()
            {
               new Card(CardFace.Ace, CardSuit.Diamonds),
               new Card(CardFace.Four, CardSuit.Hearts),
               new Card(CardFace.King, CardSuit.Spades),
               new Card(CardFace.Queen, CardSuit.Clubs),

            };

            var hand = new Hand(cards);
            var expectedType = "gosho".GetType().Name;

            Assert.IsNotEmpty(hand.ToString().GetType().Name,expectedType);

        }

        [Test]
        public void HandsToStringShouldWorkProperly()
        {
            var cards = new List<ICard>()
            {
               new Card(CardFace.Ace, CardSuit.Diamonds),
               new Card(CardFace.Four, CardSuit.Hearts),
              
            };

            var hand = new Hand(cards);
            var expectedResult = "Ace of Diamonds\r\nFour of Hearts";

            Assert.AreEqual(hand.ToString().Trim(), expectedResult);

        }

        [Test]
        public void IsValidHandShouldThrowExceptionWhenNullHandIsPassed()
        {
            var handChecker = new PokerHandsChecker();
            Assert.Throws<ArgumentNullException>(()=> handChecker.IsValidHand(null));
        }

        [Test]
        public void IsValidHandShouldThrowExceptionWhenThenHandDoesntConsistOfExactlyFiveCards()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Hearts),

            };

            var hand = new Hand(cards);

            Assert.Throws<InvalidOperationException>(() => handChecker.IsValidHand(hand));
        }

        [Test]
        public void IsValidHandShouldReturnTrueWhenAllCardsAreDifferent()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Hearts),
                new Card(CardFace.Ace, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Hearts),
                new Card(CardFace.Jack, CardSuit.Hearts),
                
            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsValidHand(hand);

            Assert.True(result && expectedResult);

        }

        [Test]
        public void IsValidHandShouldReturnFalseWhenNotAllCardsAreDifferent()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Hearts),
                new Card(CardFace.Jack, CardSuit.Hearts),

            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsValidHand(hand);

            Assert.False(result && expectedResult);

        }
        [Test]
        public void IsFlushShouldReturnTrueWhenTheHandIsValidAndAllCardsHaveSameFace()
        {

            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Clubs),
                new Card(CardFace.Five, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Jack, CardSuit.Clubs),

            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsFlush(hand);

            Assert.True(expectedResult && result);
        }

        [Test]
        public void IsFlushShouldReturnFalseWhenTheHandIsValidAndNotAllCardsHaveSameFace()
        {

            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Clubs),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Jack, CardSuit.Clubs),

            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsFlush(hand);

            Assert.False(expectedResult && result);
        }

        [Test]
        public void IsFourOfAKindShouldReturnTrueWhenHandIsValidAndThereAreFourCardsWithSameFace()
        {

            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Diamonds),
                new Card(CardFace.Ace, CardSuit.Hearts),
                new Card(CardFace.Ace, CardSuit.Spades),
                new Card(CardFace.Jack, CardSuit.Clubs),

            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsFourOfAKind(hand);

            Assert.True(expectedResult && result);

        }

        [Test]
        public void IsFourOfAKindShouldReturnFalseWhenHandIsValidAndThereAreNotFourCardsWithSameFace()
        {

            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Diamonds),
                new Card(CardFace.Ace, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Jack, CardSuit.Clubs),

            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsFourOfAKind(hand);

            Assert.False(expectedResult && result);

        }

        [Test]
        public void IsFullHouseShouldReturnTrueWhenHandIsValidAndHandIsFullHouse()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Diamonds),
                new Card(CardFace.Ace, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Queen, CardSuit.Clubs),

            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsFullHouse(hand);

            Assert.True(expectedResult && result);

        }

        [Test]
        public void IsFullHouseShouldReturnFalseWhenHandIsValidAndHandIsNotFullHouse()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ace, CardSuit.Clubs),
                new Card(CardFace.Ace, CardSuit.Diamonds),
                new Card(CardFace.Ace, CardSuit.Hearts),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };

            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsFullHouse(hand);

            Assert.False(expectedResult && result);

        }

        [Test]
        public void IsStraightShouldReturnTrueWhenHandIsValidAndIsStraight()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Six, CardSuit.Hearts),
                new Card(CardFace.Seven, CardSuit.Spades),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsStraight(hand);

            Assert.True(expectedResult && result);
        }

        [Test]
        public void IsStraightShouldReturnFalseWhenHandIsValidAndIsNotStraight()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Six, CardSuit.Hearts),
                new Card(CardFace.Seven, CardSuit.Spades),
                new Card(CardFace.Nine, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsStraight(hand);

            Assert.False(expectedResult && result);
        }

        [Test]
        public void IsStraightFlushShouldReturnTrueWhenHandIsValidAndIsStraightFlush()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Five, CardSuit.Clubs),
                new Card(CardFace.Six, CardSuit.Clubs),
                new Card(CardFace.Seven, CardSuit.Clubs),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsStraightFlush(hand);

            Assert.True(expectedResult && result);
        }

        [Test]
        public void IsStraightFlushShouldReturnFalseWhenHandIsValidAndIsNotStraightFlush()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Five, CardSuit.Clubs),
                new Card(CardFace.Six, CardSuit.Clubs),
                new Card(CardFace.Seven, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsStraightFlush(hand);

            Assert.False(expectedResult && result);
        }

        [Test]
        public void IsThreeOfAKindShouldReturnTrueWhenHandIsValidAndHasThreeOfAKind()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Diamonds),
                new Card(CardFace.Four, CardSuit.Spades),
                new Card(CardFace.Seven, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsThreeOfAKind(hand);

            Assert.True(expectedResult && result);

        }

        [Test]
        public void IsThreeOfAKindShouldReturnFalseWhenHandIsValidAndHasNoThreeOfAKind()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Diamonds),
                new Card(CardFace.Five, CardSuit.Spades),
                new Card(CardFace.Seven, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsThreeOfAKind(hand);

            Assert.False(expectedResult && result);

        }
        
        [Test]
        public void IsTwoPairShouldReturnTrueWhenHandIsValidAndHasTwoPairs()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Diamonds),
                new Card(CardFace.Five, CardSuit.Spades),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsTwoPair(hand);

            Assert.True(expectedResult && result);

        }

        [Test]
        public void IsTwoPairShouldReturnFalseWhenHandIsValidAndHasNoTwoPairs()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Diamonds),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsTwoPair(hand);

            Assert.False(expectedResult && result);

        }

        [Test]
        public void IsOnePairShouldReturnTrueWhenHandIsValidAndHasPair()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Four, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Diamonds),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsOnePair(hand);

            Assert.True(expectedResult && result);

        }

        [Test]
        public void IsOnePairShouldReturnFalseWhenHandIsValidAndHasNoPair()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ten, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Diamonds),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsOnePair(hand);

            Assert.False(expectedResult && result);

        }

        [Test]
        public void isHighCardShouldReturnTrueWhenHandIsValidAndHasHighCard()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ten, CardSuit.Clubs),
                new Card(CardFace.Four, CardSuit.Diamonds),
                new Card(CardFace.Queen, CardSuit.Spades),
                new Card(CardFace.Five, CardSuit.Diamonds),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsHighCard(hand);

            Assert.True(expectedResult && result);

        }

        [Test]
        public void isHighCardShouldReturnFalseWhenHandIsValidAndHasGreaterCombinationThanHighCard()
        {
            var handChecker = new PokerHandsChecker();
            var cards = new List<ICard>()
            {
                new Card(CardFace.Ten, CardSuit.Clubs),
                new Card(CardFace.Ten, CardSuit.Diamonds),
                new Card(CardFace.Ten, CardSuit.Spades),
                new Card(CardFace.Ten, CardSuit.Hearts),
                new Card(CardFace.Eight, CardSuit.Clubs),

            };


            var hand = new Hand(cards);
            var expectedResult = true;
            var result = handChecker.IsHighCard(hand);

            Assert.False(expectedResult && result);

        }

    }
}
