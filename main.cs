using System;
using System.Collections.Generic;
using System.IO;

class Room
{
    public int RoomNumber;
    public string RoomType;
    public bool IsAllocated;

    public void Display()
    {
        Console.WriteLine($"Room Number: {RoomNumber}, Type: {RoomType}, Allocated: {IsAllocated}");
    }
}

class Program
{
    // List to store rooms
    static List<Room> rooms = new List<Room>();
    static string fileName = "lhms_850005885.txt";
    static string backupFileName = "lhms_850005885_backup.txt";


    static void Main(string[] args)
    {
        int choice = -1;

        while (choice != 0)
        {
            Console.Clear();
            Console.WriteLine("=== LANGHAM HOTEL MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Add Room");
            Console.WriteLine("2. Display Rooms");
            Console.WriteLine("3. Allocate Room");
            Console.WriteLine("4. Deallocate Room");
            Console.WriteLine("5. Show Allocation Details");
            Console.WriteLine("6. Billing (Under Construction)");
            Console.WriteLine("7. Save to File");
            Console.WriteLine("8. Read from File");
            Console.WriteLine("9. Backup & Clear File");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            try
            {
                choice = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                Console.ReadKey();
                continue;
            }

            switch (choice)
            {
                case 1:
                    AddRoom();
                    break;
                case 2:
                    DisplayRooms();
                    break;
                case 3:
                    AllocateRoom();
                    break;
                case 4:
                    DeallocateRoom();
                    break;
                case 5:
                    ShowAllocationDetails();
                    break;
                case 6:
                    Console.WriteLine("Billing Feature is Under Construction and will be added soon...!!!");
                    break;
                case 7:
                    SaveToFile();
                    break;
                case 8:
                    ReadFromFile();
                    break;
                case 9:
                    BackupAndClearFile();
                    break;
                case 0:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void AddRoom()
    {
        Room room = new Room();
        try
        {
            Console.Write("Enter room number: ");
            room.RoomNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter room type (Single/Double): ");
            room.RoomType = Console.ReadLine();

            room.IsAllocated = false;

            rooms.Add(room);
            Console.WriteLine("Room added successfully!");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Room not added.");
        }
    }

    static void DisplayRooms()
    {
        Console.WriteLine("=== All Rooms ===");
        foreach (Room room in rooms)
        {
            room.Display();
        }
    }

    static void AllocateRoom()
    {
        Console.Write("Enter room number to allocate: ");
        int number = Convert.ToInt32(Console.ReadLine());

        foreach (Room room in rooms)
        {
            if (room.RoomNumber == number)
            {
                if (!room.IsAllocated)
                {
                    room.IsAllocated = true;
                    Console.WriteLine("Room allocated.");
                }
                else
                {
                    Console.WriteLine("Room already allocated.");
                }
                return;
            }
        }

        Console.WriteLine("Room not found.");
    }

    static void DeallocateRoom()
    {
        Console.Write("Enter room number to deallocate: ");
        int number = Convert.ToInt32(Console.ReadLine());

        foreach (Room room in rooms)
        {
            if (room.RoomNumber == number)
            {
                if (room.IsAllocated)
                {
                    room.IsAllocated = false;
                    Console.WriteLine("Room deallocated.");
                }
                else
                {
                    Console.WriteLine("Room was not allocated.");
                }
                return;
            }
        }

        Console.WriteLine("Room not found.");
    }

    static void ShowAllocationDetails()
    {
        Console.WriteLine("=== Allocated Rooms ===");
        foreach (Room room in rooms)
        {
            if (room.IsAllocated)
            {
                room.Display();
            }
        }
    }

    static void SaveToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                string time = DateTime.Now.ToString();
                writer.WriteLine($"--- Saving at {time} ---");
                foreach (Room room in rooms)
                {
                    writer.WriteLine($"{room.RoomNumber},{room.RoomType},{room.IsAllocated}");
                }
                Console.WriteLine("Data saved to file.");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("No permission to write to the file.");
        }
    }

    static void ReadFromFile()
    {
        try
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found.");
                return;
            }

            Console.WriteLine("=== File Content ===");
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading file: " + ex.Message);
        }
    }

    static void BackupAndClearFile()
    {
        try
        {
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Original file not found.");
                return;
            }

            File.AppendAllText(backupFileName, File.ReadAllText(fileName));
            File.WriteAllText(fileName, ""); // clear content
            Console.WriteLine("Backup created and file cleared.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error during backup: " + ex.Message);
        }
    }
}
