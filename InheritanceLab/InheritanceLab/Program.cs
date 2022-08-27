using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace InheritanceLab
{
    public enum ProductTypes
    {
        Laptop,
        Smartphone,
        RoboticVacuum,
        Camera,
        AirFryer
    }

    public class Program
    {
        private static readonly int MAX_POINTS = 20;

        public static void Main()
        {
            int currentPoints = MAX_POINTS;

            String[] items = File.ReadAllLines("inputs.txt");

            foreach(String item in items)
            {
                String[] itemSplit = item.Split(",").Select(x => x.Trim()).ToArray();
                List<String> miscAttributes = itemSplit.Skip(5).ToList();
                Categorizer.Categorize((ProductTypes)int.Parse(itemSplit[0]), itemSplit[1], itemSplit[2], int.Parse(itemSplit[3]), float.Parse(itemSplit[4]), miscAttributes);
            }

            Console.WriteLine("Checking InventoryItems' validity...");

            if (Categorizer.InventoryItems.Count > items.Length)
            {
                Console.Error.WriteLine("Too many items were added to the InventoryItems list!");
                Finalize(0);
            }
            else if (Categorizer.InventoryItems.Count < items.Length)
            {
                Console.Error.WriteLine("Not every item was added to the InventoryItems list!");
                Finalize(0);
            }

            currentPoints -= CheckProperties(1);

            Console.WriteLine("Checking display methods...");

            currentPoints -= CheckDisplays(1);

            foreach (InventoryItem item in Categorizer.InventoryItems)
            {
                item.DisplayItem();
            }

            Console.WriteLine("Checking bubble sort...");

            currentPoints -= CheckBubbleSort(5);

            Finalize(currentPoints);
        }


        private static int CheckProperties(int pointsLostForEach = 1)
        {
            int pointsLost = 0;

            if (typeof(Laptop).GetProperties().Length < 10)
            {
                Console.Error.WriteLine("Not enough properties for the laptop items!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(Smartphone).GetProperties().Length < 10)
            {
                Console.Error.WriteLine("Not enough properties for the smartphone items!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(RoboticVacuum).GetProperties().Length < 9)
            {
                Console.Error.WriteLine("Not enough properties for the robotic vacuum items!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(Camera).GetProperties().Length < 8)
            {
                Console.Error.WriteLine("Not enough properties for the camera items!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(AirFryer).GetProperties().Length < 8)
            {
                Console.Error.WriteLine("Not enough properties for the air fryer items!");
                pointsLost += pointsLostForEach;
            }

            return pointsLost;
        }

        private static int CheckDisplays(int pointsLostForEach = 1)
        {
            int pointsLost = 0;

            if (typeof(Laptop).GetMethod("DisplayItem")?.GetBaseDefinition().DeclaringType == typeof(Laptop).GetMethod("DisplayItem")?.DeclaringType)
            {
                Console.Error.WriteLine("Laptop display method has not been overridden!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(Smartphone).GetMethod("DisplayItem")?.GetBaseDefinition().DeclaringType == typeof(Smartphone).GetMethod("DisplayItem")?.DeclaringType)
            {
                Console.Error.WriteLine("Smartphone display method has not been overridden!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(RoboticVacuum).GetMethod("DisplayItem")?.GetBaseDefinition().DeclaringType == typeof(RoboticVacuum).GetMethod("DisplayItem")?.DeclaringType)
            {
                Console.Error.WriteLine("Robotic vacuum display method has not been overridden!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(Camera).GetMethod("DisplayItem")?.GetBaseDefinition().DeclaringType == typeof(Camera).GetMethod("DisplayItem")?.DeclaringType)
            {
                Console.Error.WriteLine("Camera display method has not been overridden!");
                pointsLost += pointsLostForEach;
            }

            if (typeof(AirFryer).GetMethod("DisplayItem")?.GetBaseDefinition().DeclaringType == typeof(AirFryer).GetMethod("DisplayItem")?.DeclaringType)
            {
                Console.Error.WriteLine("AirFryer display method has not been overridden!");
                pointsLost += pointsLostForEach;
            }

            return pointsLost;
        }

        private static int CheckBubbleSort(int maxPointsToLose = 5)
        {
            int pointsLost = 0;

            List<InventoryItem> studentSortedList = Categorizer.PerformBubbleSort();

            if (studentSortedList == null)
            {
                Console.Error.WriteLine("The list returned from the bubble sort was null!");
                pointsLost = maxPointsToLose;
            }
            if (studentSortedList!.Count != Categorizer.InventoryItems.Count)
            {
                Console.Error.WriteLine("List from bubble sort has a different amount of elements from the InventoryItems global.");
                pointsLost = maxPointsToLose;
            }
            else
            {
                List<InventoryItem> linqSorted = ((List<InventoryItem>)DeepCopy(Categorizer.InventoryItems)!).OrderBy(x => x.Price).ToList();
                int pointsSubtracted = 0;

                for (int i = 0; i < Categorizer.InventoryItems.Count; i++)
                {
                    if (linqSorted[i].Price != studentSortedList[i].Price)
                    {
                        pointsSubtracted++;
                    }
                }

                if (pointsSubtracted > 0)
                {
                    Console.Error.WriteLine("The list was not sorted properly. There are still items out of order.");
                    pointsLost -= Math.Min(maxPointsToLose, pointsSubtracted);
                }
            }

            return pointsLost;
        }

        private static void Finalize(int points)
        {
            Console.WriteLine(points + "/" + MAX_POINTS + " scored.");
            Console.WriteLine(((float)points / MAX_POINTS * 100).ToString("0.0") + "%");
            
            try
            {
                Environment.Exit(0);
            }
            catch { }
        }

        public static object? DeepCopy(object obj)
        {
            if (obj == null)
            {
                return null;
            }
                
            Type type = obj.GetType();

            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }
            else if (type.IsArray)
            {
                Type elementType = Type.GetType(type.FullName!.Replace("[]", string.Empty))!;
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array!.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copied.SetValue(DeepCopy(array.GetValue(i)!), i);
                }
                return Convert.ChangeType(copied, obj.GetType());
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(obj.GetType())!;
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(obj)!;
                    if (fieldValue == null)
                    {
                        continue;
                    }
                    field.SetValue(toret, DeepCopy(fieldValue));
                }
                return toret;
            }
            else
            {
                return null;
            }
        }
    }
}