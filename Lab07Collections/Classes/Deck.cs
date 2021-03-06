﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Lab07Collections.Classes;

namespace Lab07Collections
{
    public class Deck<T> : IEnumerable
    {
        T[] cards = new T[5];
        int counter;
        /// <summary>
        /// Method allows for us to add a card to the front
        /// of our collection
        /// </summary>
        /// <param name="card">Card object to be added</param>
        public void Add(T card)
        {
            if (counter == cards.Length)
            {
                Array.Resize(ref cards, (cards.Length * 2));
            }
            cards[counter++] = card;
        }
        /// <summary>
        /// Takes a given index and removes the card at that specific
        /// index. Then collapses the collection and resets counter
        /// </summary>
        /// <param name="index">Index to be removed</param>
        public bool Remove(int index)
        {
            try
            {
                cards[index] = default;
                for (int i = 0; i < counter; i++)
                {
                    if (i >= index) cards[i] = cards[i + 1];
                }
                if (counter < (cards.Length / 2)) Array.Resize(ref cards, (cards.Length / 2));
                counter--;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Oh no, I don't have that!");
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Used to find a specific card with in a deck.
        /// Useful for proving shuffling
        /// </summary>
        /// <param name="index">index to be found</param>
        /// <returns>Object if found, else default</returns>
        public T Find(int index)
        {
            try
            {
                T found = cards[index];
                return found;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("oops I don't have that!");
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        /// <summary>
        /// Method used to shuffle the deck.
        /// </summary>
        public void Shuffle()
        {
            T[] shuffler = new T[cards.Length];
            string[] checker = new string[counter];
            Random random = new Random();
            int ranNum = random.Next(0, counter);

            for (int i = 0; i < counter; i++)
            {
                while (Array.IndexOf(checker, ranNum.ToString()) != -1)
                {
                    ranNum = random.Next(0, counter);
                }
                checker[i] = ranNum.ToString();
                shuffler[i] = cards[Int32.Parse(checker[i])];
                ranNum = random.Next(0, counter);
            }
            cards = shuffler;
        }
        /// <summary>
        /// Method used to show the amount of 
        /// allocation for the Collection
        /// </summary>
        /// <returns>The total length</returns>
        public int GetLength()
        {
            return cards.Length;
        }
        /// <summary>
        /// Method used to show only the indexes
        /// that are currently in use
        /// </summary>
        /// <returns>Length of how many in use</returns>
        public int Length()
        {
            return counter;
        }
        /// <summary>
        /// Method allows us to utilize a foreach
        /// loop
        /// </summary>
        /// <returns>A value if the index is assigned</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < counter; i++)
            {
                yield return cards[i];
            }
        }
        //Magic
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //Add shuffle
        //Add remove
    }
}
