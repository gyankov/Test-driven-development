using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    public class PokerHandsChecker : IPokerHandsChecker
    {
        public bool IsValidHand(IHand hand)
        {
            if (hand==null)
            {
                throw new ArgumentNullException();
            }

            if (hand.Cards.Count!=5)
            {
                throw new InvalidOperationException();
            }

            var cards = hand.Cards;
            var allCardsAreDifferent = true;

            for (int i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                for (int j = 0; j < cards.Count; j++)
                {
                    var currCard = cards[j];

                    if (i==j)
                    {
                        continue;
                    }
                    if (card.ToString()==currCard.ToString())
                    {
                        allCardsAreDifferent = false;
                    }
                }
            }

            return allCardsAreDifferent;
        }

        public bool IsStraightFlush(IHand hand)
        {
            var handChecker = new PokerHandsChecker();
            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }  

            if (IsFlush(hand) && IsStraight(hand))
            {
                return true;
            }

            return false;
        }

        public bool IsFourOfAKind(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            var isFourOfAKind = false;
            var groupedByFace = hand.Cards.GroupBy(x => x.Face);

            if (groupedByFace.Any(x=> x.Count()==4))
            {
                isFourOfAKind = true;
            }

            return isFourOfAKind;

        }

        public bool IsFullHouse(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            var isFullHouse = false;

            var groupedByFace = hand.Cards.GroupBy(x => x.Face);

            if (groupedByFace.Any(x=> x.Count()==3) && groupedByFace.Any(x=> x.Count()==2))
            {
                isFullHouse = true;
            }

            return isFullHouse;
        }

        public bool IsFlush(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            var isFlush = true;
            var face = hand.Cards.FirstOrDefault().Suit;

            foreach (var card in hand.Cards)
            {
                if (card.Suit!=face)
                {
                    isFlush = false;
                }
            }

            return isFlush;

        }

        public bool IsStraight(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            var sorted = hand.Cards.Select(x => (int)x.Face).OrderBy(x => x).ToList();

            if (sorted.Contains((int)CardFace.Ace) && sorted.Contains((int) CardFace.Two))
            {
                var index = sorted.IndexOf((int)CardFace.Ace);
                sorted[index] = 1;
                sorted = sorted.OrderBy(x => x).ToList();
            }


            for (int i = 0; i < sorted.Count-1; i++)
            {
                if ((sorted[i]+1)!=sorted[i+1])
                {
                    return false;
                }
            }

            return true;
          
        }

        public bool IsThreeOfAKind(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            var groupedByFace = hand.Cards.GroupBy(x => x.Face);

            if (groupedByFace.Any(x=> x.Count()==3))
            {
                return true;
            }

            return false;

        }

        public bool IsTwoPair(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            var groupedByFace = hand.Cards.GroupBy(x => x.Face);

            if (groupedByFace.Count(x=>x.Count()==2)==2)
            {
                return true;
            }

            return false;
        }

        public bool IsOnePair(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            var groupedByFace = hand.Cards.GroupBy(x => x.Face);

            if (groupedByFace.Any(x => x.Count() == 2) )
            {
                return true;
            }

            return false;
        }

        public bool IsHighCard(IHand hand)
        {
            var handChecker = new PokerHandsChecker();

            if (!handChecker.IsValidHand(hand))
            {
                throw new InvalidOperationException();
            }

            if (handChecker.IsOnePair(hand)
                || handChecker.IsTwoPair(hand)
                || handChecker.IsThreeOfAKind(hand)
                || handChecker.IsStraight(hand)
                || handChecker.IsFourOfAKind(hand)
                || handChecker.IsFlush(hand)
                || handChecker.IsFullHouse(hand)
                || handChecker.IsStraightFlush(hand))
            {
                return false;
            }

            return true;
        }

        public int CompareHands(IHand firstHand, IHand secondHand)
        {
            throw new NotImplementedException();
        }

        private Dictionary<int, CardFace> CardIndex = new Dictionary<int, CardFace>()
        {
            {1,CardFace.Ace },
            {2,CardFace.Two },
            {3,CardFace.Three },
            {4,CardFace.Four },
            {5,CardFace.Five },
            {6,CardFace.Six },
            {7,CardFace.Seven },
            {8,CardFace.Eight },
            {9,CardFace.Nine },
            {10,CardFace.Ten },
            {11,CardFace.Jack },
            {12,CardFace.Queen },
            {13,CardFace.King },
            {14,CardFace.Ace },


        };
        

        
    }
}