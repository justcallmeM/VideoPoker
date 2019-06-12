using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoPoker
{
    class Program
    {
        static void Main()
        {
            //We return the hand and the deck after the cards have been dealt.
            Tuple<string[],string[]> handNDeck = InitialDeal();

            //Testinis kodas irodyti, jog logika veikia.
            /*TODO:
             * kortu skaiciu ivedimams padaryti limitus.
             * Laimejimo logika.
             */

            Console.WriteLine("Initial deal of cards");
            foreach(string card in handNDeck.Item1)
            {
                Console.WriteLine(card);
            }

            Console.WriteLine("--------------");

            string[] handAfterChange = ChangeCards(handNDeck.Item1, handNDeck.Item2);

            Console.WriteLine("--------------");

            Console.WriteLine("Hand of cards after the change");
            foreach(string card in handAfterChange)
            {
                Console.WriteLine(card);
            }

            Console.ReadKey(true);
        }

        static Tuple<string[], string[]> InitialDeal()
        {
            Random rnd = new Random();
            //Using French naming system.
            //possible to shufle the deck later.
            string[] allCards = { "HA", "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10", "HJ", "HQ", "HK",
                                  "TA", "T2", "T3", "T4", "T5", "T6", "H7", "T8", "T9", "T10", "TJ", "TQ", "TK",
                                  "CA", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "CJ", "CQ", "CK",
                                  "PA", "P2", "P3", "P4", "P5", "P6", "P7", "P8", "P9", "P10", "PJ", "PQ", "PK"};
            string[] dealtHand = new string[5];

            for(int i=0; i<dealtHand.Length; i++)
            {
                int randomDeckCard = rnd.Next(allCards.Length);
                dealtHand[i] = allCards[randomDeckCard];
                allCards = allCards.Where(card => card != allCards[randomDeckCard]).ToArray();
            }

            //returning 5 card hand and the remaining deck.
            return new Tuple<string[], string[]>(dealtHand, allCards);
        }

        static string[] ChangeCards(string[] currentHand, string[] deck)
        {
            Random rnd = new Random();
            string[] newHand = new string[5];
            int numberOfCards = 0;

            //How many cards do you want to change?
            //nepamirsti while loop
            //limit the number to positive from 0 to 5
            /*Console.WriteLine("Please enter a number of cards you'd like to change");
            while (!Int32.TryParse(Console.ReadLine(), out numberOfCards) || numberOfCards > 5 || numberOfCards < 0)
            {
                Console.WriteLine("Please enter a correct number");
                Int32.TryParse(Console.ReadLine(), out numberOfCards);
            }*/
            Console.WriteLine("Please enter a number of cards you'd like to change");
            Int32.TryParse(Console.ReadLine(), out numberOfCards);
            //if 5 cards, then replace all, if anything else, then do the logic.
            if (numberOfCards == 5)
            {
                for (int i = 0; i < newHand.Length; i++)
                {
                    int randomDeckCard = rnd.Next(deck.Length);
                    newHand[i] = deck[randomDeckCard];
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
                //Write the numbers of the cards you'd like to change, from left to right, 0 to 4.
                Console.WriteLine("Please write the numbers of the cards you'd like to change.");
                Console.WriteLine("0 being the first card, and  4 being the last one.");
                Console.WriteLine("After writing one number, please press enter");
                for (int i = 0; i < numberOfCards; i++)
                {
                    //while loop, negali kartotis, negali but daugiau nei 4 ir maziau nei 0
                    Int32.TryParse(Console.ReadLine(), out int numberOfCard);
                    cardsToChange.Add(numberOfCard);
                }

                foreach(int cardToChange in cardsToChange)
                {
                    int randomDeckCard = rnd.Next(deck.Length);
                    currentHand[cardToChange] = deck[randomDeckCard];
                    deck = deck.Where(card => card != deck[randomDeckCard]).ToArray();
                }

                newHand = currentHand;

            }

            return newHand;
        }
    }
}
