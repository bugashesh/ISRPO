using System;

namespace Lab__3
{
    public class Person
    {
        public string name = "Undefined";
        public int age;

        public void Print()
        {
            Console.WriteLine($"Имя: {name}  Возраст: {age}");
        }
    }

    internal class Book
    {
        public int pages;
        public string name;
        public float weight;

        public void getInfoBook()
        {
            Console.WriteLine("В книге " + name + " находиться " + pages + " страниц. ");
            Console.WriteLine("При этом она весит " + weight);
        }
    }
}
