/* 
 * Project Name: LANGHAM Hotels
 * Author Name:Linh Pham
 * Date:16 July 2024
 * Application Purpose: Hotel Management System
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public static void AddRoom(string folderPath, string fileName)
        {
            try
            {
                string filePath = Path.Combine(folderPath, fileName);

                int roomNo, roomFloor;
                string employeeName, employeePosition;
                Console.WriteLine("Please input the information below");

                //Room details
                Console.WriteLine("Room number");
                roomNo = int.Parse(Console.ReadLine());
                Console.WriteLine("Room Floor");
                roomFloor = int.Parse(Console.ReadLine());


                // Employee
                Console.WriteLine("Employee Name");
                employeeName = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Employee Position");
                employeePosition = Convert.ToString(Console.ReadLine());

                using (StreamWriter roomLst = new StreamWriter(filePath, true))
                {
                    roomLst.WriteLine("This room are added to the system at "+ DateTime.Now);
                    roomLst.WriteLine(" Room Number : {0}", roomNo);
                    roomLst.WriteLine(" Room Floor : {0}", roomFloor);

                    roomLst.WriteLine(" Employee Name : {0}", employeeName);
                    roomLst.WriteLine(" Employee Position : {0}", employeePosition);
                    roomLst.WriteLine("***********************************************************************************");
                }

                Console.WriteLine("Added a room successfully");

            }catch(FormatException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        

    }
    // Custom Class - Customer
    public class Customer
    {
        //public int CustomerNo();
        //public string CustomerName();
    }
    // Custom Class - RoomAllocation
    public class RoomlAllocaltion
    {
        public static void AllocateAndDeallocateRoom(string folderPath, string fileName, int status)
        {
            try
            {
                string filePath = Path.Combine(folderPath, fileName);

                int roomNo, roomFloor;
                string customerName, customerPhoneNumber, employeeName, employeePosition;
                string checkInDate = "0", checkOutDate = "0";
                string msg_result = status == 1 ? "The room is allocated successfully" : "The room is returned successfully";

                Console.WriteLine("Please input the information below");

                //Room details
                Console.WriteLine("Room number");
                roomNo = int.Parse(Console.ReadLine());
                Console.WriteLine("Room Floor");
                roomFloor = int.Parse(Console.ReadLine());

                // Date
                if (status == 1)
                {
                    Console.WriteLine("Checkin Date dd/mm/yy");
                    checkInDate = Convert.ToString(Console.ReadLine());
                    Console.WriteLine("Checkout Date dd/mm/yy");
                    checkOutDate = Convert.ToString(Console.ReadLine());
                }
                //Customer
                Console.WriteLine("Customer Full Name");
                customerName = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Customer Phone Number");
                customerPhoneNumber = Convert.ToString(Console.ReadLine());

                // Employee
                Console.WriteLine("Employee Name");
                employeeName = Convert.ToString(Console.ReadLine());
                Console.WriteLine("Employee Position");
                employeePosition = Convert.ToString(Console.ReadLine());


                using (StreamWriter roomLst = new StreamWriter(filePath, true))
                {
                    string msg = status == 1 ? "This room are allocated at " : "The room is returned at ";
                    roomLst.WriteLine(msg + DateTime.Now);
                    roomLst.WriteLine(" Room Number : {0}", roomNo);
                    roomLst.WriteLine(" Room Floor : {0}", roomFloor);

                    roomLst.WriteLine(" Customer Full Name : {0}", customerName);
                    roomLst.WriteLine(" Customer Phone Number : {0}", customerPhoneNumber);

                    if (status == 1)
                    {
                        roomLst.WriteLine(" Checkin Date : {0}", checkInDate);
                        roomLst.WriteLine(" Checkout Date : {0}", checkOutDate);
                    }

                    roomLst.WriteLine(" Employee Name : {0}", employeeName);
                    roomLst.WriteLine(" Employee Position : {0}", employeePosition);
                    roomLst.WriteLine("***********************************************************************************");
                }

                Console.WriteLine(msg_result);

            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        
    }
    public class FileManagement
    {
        
        public static void SaveFile(int IsFileExist, string filePath, string content)
        {
            try
            {

                TextWriter tw = new StreamWriter(filePath, true);
                tw.Write(content);
                tw.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public static void DiplayFileData(string filePath)
        {
            try
            {
                string allFileText = File.ReadAllText(filePath);
                Console.WriteLine(allFileText);
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /*
        public static int IsFileExist(string filePath)
        {
            int isExist;
            if (File.Exists(filePath)) isExist = 1;
            else isExist = 0;

            return isExist;
            
        }
        */
        public static void CreateAFile(string folderPath, string fileName)
        {
            string filePath = Path.Combine(folderPath, fileName);
            if (File.Exists(filePath))
            {
                FileStream fs = new FileStream(folderPath + fileName, FileMode.Create);
                Console.WriteLine("File {0} is created successfully", fileName);
            }
            else
            {
                Console.WriteLine("File {0} Exists", fileName);
                Console.ReadLine();
            }
        }

        public static void CreateABackup(string folderPath, string fileName1, string fileName2, string fileName_backup_tmp, string fileName_backup)
        {
            string filePath1 = Path.Combine(folderPath, fileName1);
            string filePath2 = Path.Combine(folderPath, fileName2);
            string filePath_backup_tmp = Path.Combine(folderPath, fileName_backup_tmp);
            string filePath_backup = Path.Combine(folderPath, fileName_backup);
            try
            {
                //Read content of filename
                string roomLst = File.ReadAllText(filePath1);
                string roomAllocateLst = File.ReadAllText(filePath2);
                if (roomLst.Length > 0 || roomAllocateLst.Length > 0)
                {
                    // Create a Backup version for 2 files onto a new file
                    using(StreamWriter backupFile = new StreamWriter(filePath_backup_tmp))
                    {
                        backupFile.WriteLine("***********************************************************************************");
                        backupFile.WriteLine("                              BACK UP AT {0}                ", DateTime.Now);
                        backupFile.WriteLine("***********************************************************************************");
                        backupFile.WriteLine("--------------------------THESE DATA FROM ROOM LIST--------------------------------");
                        backupFile.WriteLine(roomLst);
                        backupFile.WriteLine("-------------------------THESE DATA FROM ROOM ALLOCATE LIST------------------------");
                        backupFile.WriteLine(roomAllocateLst);
                        backupFile.WriteLine("***********************************************************************************");
                    }


                    //Copying from LHMS to back up file
                    File.Copy(filePath_backup_tmp, filePath_backup);


                    //Cleaning the LHMS file
                    using(FileStream fs = new FileStream(filePath_backup_tmp, FileMode.Open))
                    {
                        fs.SetLength(0);
                    }
                }
                else
                {
                    Console.WriteLine("File is empty");
                }
            }catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
        public static string fileName_backup_tmp = "lhms_764707736.txt";
        public static string fileName_roomLst = "RoomList.txt";
        public static string fileName_roomAllocationLst = "RoomAllocationList.txt";
        public static string fileName_backup = "lhms_764707736_backup.txt";
        public static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Main function
        static void Main(string[] args)
        {

            //int IsFileExist = Assessment2Task2.FileManagement.IsFileExist(filePath);
            char ans;
            
            string filePath_roomLst = Path.Combine(folderPath, fileName_roomLst);
            string filePath_roomAllocationLst = Path.Combine(folderPath, fileName_roomAllocationLst);
            string filePath_backup_tmp = Path.Combine(folderPath, fileName_backup_tmp);
            string filePath_Backup = Path.Combine(folderPath, fileName_backup);
            do
            {
                Console.Clear();
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("                 LANGHAM HOTEL MANAGEMENT SYSTEM                  ");
                Console.WriteLine("                            MENU                                 ");
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File - Backup");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                Console.WriteLine("***********************************************************************************");
                Console.Write("Enter Your Choice Number Here:");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        // adding Rooms function
                        Assessment2Task2.Room.AddRoom(folderPath, fileName_roomLst);
                        break;
                    case 2:
                        // display Rooms function;
                        Assessment2Task2.FileManagement.DiplayFileData(filePath_roomLst);
                        break;
                    case 3:
                        // allocate Room To Customer function
                        Assessment2Task2.RoomlAllocaltion.AllocateAndDeallocateRoom(folderPath, fileName_roomAllocationLst, 1);
                        break;
                    case 4:
                        // De-Allocate Room From Customer function
                        Assessment2Task2.RoomlAllocaltion.AllocateAndDeallocateRoom(folderPath, fileName_roomAllocationLst, 0);
                        break;
                    case 5:
                        // display Room Alocations function;
                        Assessment2Task2.FileManagement.DiplayFileData(filePath_roomAllocationLst);
                        break;
                    case 6:
                        //  Display "Billing Feature is Under Construction and will be added soon…!!!"
                        Console.WriteLine("Billing Feature is Under Construction and will be added soon…!!!");
                        break;
                    case 7:
                        // SaveRoomAllocationsToFile
                        // Read the content of the file and append it to another file called “lhms_764707736_backup.txt”
                        // and delete the content of the “lhms_764707736.txt” file”
                        Assessment2Task2.FileManagement.CreateABackup(folderPath, fileName_roomLst, fileName_roomAllocationLst, filePath_backup_tmp, fileName_backup);

                        break;
                    case 8:
                        //Show Room Allocations From a Backup File
                        Assessment2Task2.FileManagement.DiplayFileData(filePath_Backup);
                        break;
                    case 9:
                        // Exit Application
                        break;
                    case 0:
                        
                        break;
                    default:
                        break;
                }

                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
            } while (ans == 'y' || ans == 'Y');
        }
    }

}