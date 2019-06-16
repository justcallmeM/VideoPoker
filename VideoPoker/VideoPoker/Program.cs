using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoPoker
{
    public class Program
    {
        static void Main()
        {
            //We return the hand and the deck after the cards have been dealt.
            Tuple<string[],string[]> handNDeck = InitialDeal();

            //simple design logic - printing the cards that have been dealt.
            Console.WriteLine("Initial deal of cards");
            foreach(string card in handNDeck.Item1)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine("--------------");

            //ability to change cards.
            string[] handAfterChange = ChangeCards(handNDeck.Item1, handNDeck.Item2);

            //simple design logic - printing the cards after they have been changed.
            Console.WriteLine("--------------");

            Console.WriteLine("Hand of cards after the change");
            foreach(string card in handAfterChange)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine("--------------");

            //result and prize. (straight and straight flush are not included).
            var result = ShowResult(handAfterChange);

            //printing out the result
            Console.WriteLine(result.Item1 + " Prize: " + result.Item2);

            Console.ReadKey(true);
        }

        static Tuple<string[], string[]> InitialDeal()
        {
            Random rnd = new Random();
            //initializing the deck of cards.
            string[] allCards = { "HA", "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "HT", "HJ", "HQ", "HK",
                                  "DA", "D2", "D3", "D4", "D5", "D6", "H7", "D8", "D9", "DT", "DJ", "DQ", "DK",
                                  "CA", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "CT", "CJ", "CQ", "CK",
                                  "SA", "S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "ST", "SJ", "SQ", "SK"};
            string[] dealtHand = new string[5];

            //randomly assinging the dealtHand string array its strings - cards.
            for(int i=0; i<dealtHand.Length; i++)
            {
                int randomDeckCard = rnd.Next(allCards.Length);
                dealtHand[i] = allCards[randomDeckCard];
                //eliminating the card that has been dealt.
                allCards = allCards.Where(card => card != allCards[randomDeckCard]).ToArray();
            }

            //returning 5 card hand and the remainder of the deck.
            return new Tuple<string[], string[]>(dealtHand, allCards);
        }

        static string[] ChangeCards(string[] currentHand, string[] deck)
        {
            Random rnd = new Random();
            string[] newHand = new string[5];
            int numberOfCards = 0;

            //How many cards do you want to change?
            Console.WriteLine("Please enter a number of cards you'd like to change");
            string answer = Console.ReadLine();
            while (!Int32.TryParse(answer, out numberOfCards) || numberOfCards > 5 || numberOfCards < 0)
            {
                Console.WriteLine("Please enter a correct number");
                answer = Console.ReadLine();
            }

            //if 5 cards, then replace all, if anything else, then do the logic.
            if (numberOfCards == 5)
            {
                for (int i = 0; i < newHand.Length; i++)
                {
                    int randomDeckCard = rnd.Next(deck.Length);
                    newHand[i] = deck[randomDeckCard];
                    //removing the dealt card from the deck.
                    deck = deck.Where(card => card != deck[randomDeckCard]).ToArray();
                }
                Console.WriteLine("Your cards have been changed");
            }
            else if(numberOfCards == 0)
            {
                newHand = currentHand;
                Console.WriteLine("You decided that you like your current cards");
            }
            //I want to change 4 cards
            else
            {
                List<int> cardsToChange = new List<int>();
                int numberOfCard = 0;
                //Write the numbers of the cards you'd like to change, from left to right, 0 to 4.
                Console.WriteLine("Please write the numbers of the cards you'd like to change.");
                Console.WriteLine("0 being the first card, and  4 being the last one.");
                Console.WriteLine("After writing one number, please press enter");
                for (int i = 0; i < numberOfCards; i++)
                {
                    string answ = Console.ReadLine();
                    //while loop responsible for not letting the player choose a card, which is not in range and repeat the same card.
                    while (!Int32.TryParse(answ, out numberOfCard) || numberOfCards > 5 || numberOfCards < 0 || cardsToChange.Contains(numberOfCard))
                    {
                        Console.WriteLine("Please enter a correct number");
                        answ = Console.ReadLine();
                    }
                    cardsToChange.Add(numberOfCard);
                }

                foreach(int cardToChange in cardsToChange)
                {
                    int randomDeckCard = rnd.Next(deck.Length);
                    currentHand[cardToChange] = deck[randomDeckCard];
                    //removing the dealt card from the deck
                    deck = deck.Where(card => card != deck[randomDeckCard]).ToArray();
                }

                newHand = currentHand;

            }

            return newHand;
        }

        //I am very unhappy with this logic, but it is the only one I could write in this time.
        public static Tuple<string, int> ShowResult(string[] hand)
        {
            List<string> handWithoutSuit = new List<string>();
            List<int> handWithoutSuitInt = new List<int>();
            List<string> handWithoutValues = new List<string>();
            Dictionary<char, int> dictSuit = new Dictionary<char, int>();
            Dictionary<int, int> dictValue = new Dictionary<int, int>();
            string handWithoutValuesString = String.Empty;
            int sumOfHand = 0;
            string result = String.Empty;
            int prize = 0;

            //Separating each card
            foreach(string card in hand)
            {
                handWithoutValues.Add(card.Substring(0, 1));
                handWithoutSuit.Add(card.Substring(1, 1));
            }

            //in the list, which only has the value of the card we change the letters to numbers.
            for(int i = 0; i < 5; i++)
            {
                switch (handWithoutSuit[i])
                {
                    case "T":
                        handWithoutSuitInt.Add(10);
                        break;
                    case "J":
                        handWithoutSuitInt.Add(11);
                        break;
                    case "Q":
                        handWithoutSuitInt.Add(12);
                        break;
                    case "K":
                        handWithoutSuitInt.Add(13);
                        break;
                    case "A":
                        handWithoutSuitInt.Add(14);
                        break;
                    default:
                        Int32.TryParse(handWithoutSuit[i], out int number);
                        handWithoutSuitInt.Add(number);
                        break;
                }
            }

            //turn the list into a simple string, which will be used to make a dictionary.
            foreach(string suit in handWithoutValues)
            {
                handWithoutValuesString += suit;
            }

            //separating the string into a dictionary
            foreach (var suit in handWithoutValuesString)
            {
                //if the dictionary does not have appropriate suit
                if (!dictSuit.ContainsKey(suit))
                {
                    //we add the suit and make the count 1.
                    dictSuit.Add(suit, 1);
                }
                else
                {
                    //if the suit exists in the dictionary we increment the number.
                    dictSuit[suit]++;
                }
            }

            //adding values of the card to the dictionary.
            foreach(var value in handWithoutSuitInt)
            {
                //if the dictionary of values does not have a certain value we create the entry and it has 1 repetition.
                if (!dictValue.ContainsKey(value))
                {
                    dictValue.Add(value, 1);
                }
                else
                {
                    //if the value already exist in the dictionary we increment it.
                    dictValue[value]++;
                }
            }

            //we get the sum of the hand from the list of integers and use it to determine whether the player has a royal flush.
            foreach(int value in handWithoutSuitInt)
            {
                sumOfHand += value;
            }

            //royal flush
            //if the player has a hand, which sums to 60 and all of them have the same suit.
            if (sumOfHand == 60 && dictSuit.ContainsValue(5))
            {
                result = "royal flush";
                prize = 800;
            }
            //straight flush
            //four of a kind
            //if the dictionary, which is responsible for the values of the cards have one card that repeats four times.
            else if (dictValue.ContainsValue(4))
            {
                result = "four of a kind";
                prize = 25;
            }
            //full hosue
            //if the dictionary, which is responsible for the values of the cards have one card that repeats three times and another that repeats twice.
            else if (dictValue.ContainsValue(3) && dictValue.ContainsValue(2))
            {
                result = "full house";
                prize = 9;
            }
            //flush
            //if the hand has the same suit.
            else if (dictSuit.ContainsValue(5))
            {
                result = "flush";
                prize = 6;
            }
            //straight
            //if the dictionary, which is responsible for the values of the cards have one card that repeats three times.
            //three of a kind
            else if (dictValue.ContainsValue(3))
            {
                result = "three of a kind";
                prize = 3;
            }
            //two pair
            //if the dictionary, which is responsible for the values have a card that repeats twice
            //and if the dictionary responsible for the values has three entries.
            else if (dictValue.ContainsValue(2) && dictValue.Keys.Count() == 3)
            {
                result = "two pair";
                prize = 2;
            }
            //jacks or better
            //if the dictionary, which is responsible for the values has keys from 11 to 14.
            else if (dictValue.ContainsKey(11) || dictValue.ContainsKey(12) || dictValue.ContainsKey(13) || dictValue.ContainsKey(14))
            {
                result = "jacks or better";
                prize = 1;
            }
            else
            {
                result = "nothing";
                prize = 0;
            }

            return new Tuple<string, int>(result, prize);
        }

    }
}
