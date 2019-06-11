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

            return new Tuple<string[], string[]>(dealtHand, allCards);
        }
    }
}
