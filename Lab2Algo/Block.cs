using System;
using System.Collections.Generic;

namespace Lab2Algo
{
    public class Block
    {
        private readonly int number;
        public int Comparsions { get; set; }
        public List<Element> Elements { get; }
        //capacity
        public Block(int number)
        {
            this.number = number;
            Elements = new List<Element>();
        }

        public void Delete(int key)
        {
            int[] num = new int[Elements.Count];
            for (var i = 0; i < Elements.Count; i++)
            {
                num[i] = Elements[i].Key;
            }
            int n = BinarySearch(num, key);
            if (n == -1)
                throw new ArgumentException("Such index doesnt exists");
            Elements.RemoveAt(n);
        }
        public void Change(int key, string data)
        {
            Element element = Find(key);
            if(element == null)
                throw new ArgumentException("Such index doesnt exists");
            element.Value = data;
        }
        public void Add(int key, string value)
        {
            if (value == null || value.Equals(""))
                throw new ArgumentException("String cant be empty");
            if (Find(key) != null)
                throw new ArgumentException("Such index already exists");
            Element elem = new Element(key, value);
            if (Elements.Count == 0)
            {
                Elements.Add(elem);
                return;
            }
            int[] num = new int[Elements.Count];
            for (var i = 0; i < Elements.Count; i++)
            {
                num[i] = Elements[i].Key;
            }

            Comparsions = 0; // delete
            Elements.Insert(BinaryInsert(num,key),elem);
        }

        public Element Find(int key)
        {
            int[] num = new int[Elements.Count];
            for (var i = 0; i < Elements.Count; i++)
            {
                num[i] = Elements[i].Key;
                //получаем массив значений ключей
            }
            int n = BinarySearch(num, key);
            if (n == -1)
                return null;
           return Elements[n];
        }
        private int BinarySearch(int[] array, int searchedValue)
        {
            int left = 0;
            int right = array.Length - 1;
            while (left <= right)
            {
                //index of middle element
                var middle = (left + right) / 2;

                if (searchedValue == array[middle])
                {
                    Comparsions++;
                    return middle;
                }
                else if (searchedValue < array[middle])
                {
                    Comparsions++;
                    right = middle - 1;
                }
                else
                {
                    Comparsions+=2;
                    left = middle + 1;
                }
            }
            //nothing was founded
            return -1;
        }

        public static int BinaryInsert(int[] array, int searchedValue)
        {
            int left = 0;
            int right = array.Length - 1;
            var middle = -1;
            while (left <= right)
            {
                //index of middle element
                middle = (left + right) / 2;

                if (searchedValue == array[middle])
                {
                    return middle;
                }
                else if (searchedValue < array[middle])
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }
            //to which place insert
            if ((middle == 0 && searchedValue > array[0]) || (middle == array.Length - 1 && searchedValue > array[^1]))
                return ++middle;
            return middle;
        }
        
    }
}