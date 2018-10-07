﻿using System;
using DriveAccessors;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinaryDriveAccessorDebug
{
    class Program
    {
        public const string Path = "file.dat";
        public const string AddressStoragePath = "addresses.dat";

        private static BinaryDriveAccessor<Person> dataManager;
        private static Person[] people;

        static void Main(string[] args)
        {
            #region Drive accessor debug.

            File.Create(Path).Close();

            dataManager = new BinaryDriveAccessor<Person>(Path);

            people = new Person[]
            {
                new Person("John", "Doh", 37),
                new Person("Rick", "Morgan", 25),
                new Person("Joe", "Dash", 17)
            };

            dataManager.AddRecord(people[0]);
            dataManager.AddRecord(people[1]);
            dataManager.AddRecord(people[2]);

            Person extracted = dataManager.GetNextRecord();

            Console.WriteLine(extracted);
            Console.WriteLine(extracted.Equals(people[0]));
            Console.WriteLine();

            extracted = dataManager.GetNextRecord();

            Console.WriteLine(extracted);
            Console.WriteLine(extracted.Equals(people[1]));
            Console.WriteLine();

            int i = 0;

            foreach (Person person in dataManager)
                Console.WriteLine(person.Equals(people[i++]));

            Console.WriteLine(dataManager.GetNextRecord().Equals(people[2]));
            Console.WriteLine();

            Person guy = dataManager.GetNextRecord();

            Console.WriteLine(guy == null);

            dataManager.Reset();
            guy = dataManager.GetNextRecord();

            Console.WriteLine(guy);
            Console.WriteLine(guy.Equals(people[0]));

            #endregion

            Console.WriteLine();

            #region Indexed storage debug.

            File.Create(AddressStoragePath).Close();

            IIndexedStorage<long> storage = new AddressStorage(AddressStoragePath) { 32, 13, 15 };

            Console.WriteLine($"First address: {storage[0]}");
            Console.WriteLine($"All: {String.Join(", ", storage)}");
            Console.WriteLine($"Second address: {storage[1]}");

            #endregion
        }
    }
}
