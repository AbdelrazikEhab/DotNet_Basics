// See https://aka.ms/new-console-template for more informationu
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
namespace HelloWorld
{

    public class Program
    {
        public static void Main(string[] args)
        {
            //  Computer computer = new Computer
            // {
            //     Motherboard = "ASUS ROG Strix",
            //     CPUCores = 8,
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 1299.99m,
            //     VideoCard = "INTEL RTX 3060"
            // };
            DataContext dapper = new DataContext();
            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            Console.WriteLine($"Current Date and Time: {rightNow}");

            // string sql = "Insert into TutorialAppSchema.Computer (Motherboard, CPUCores, HasWifi, HasLTE, Price, VideoCard) VALUES " +
            //     "('" + computer.VideoCard + "', " +
            //     computer.CPUCores + ", " +
            //     (computer.HasWifi ? 1 : 0) + ", " +
            //     (computer.HasLTE ? 1 : 0) + ", " +
            //     computer.Price + ", '" +
            //     computer.VideoCard + "')";

            // DBconnection.Execute(sql);

            string SELECTQuery = "SELECT * FROM TutorialAppSchema.Computer";
            var computers = dapper.LoadData<Computer>(SELECTQuery).ToList();
            // foreach (var comp in computers)
            // {
            //     Console.WriteLine($"Motherboard: {comp.Motherboard}, CPU Cores: {comp.CPUCores}, Has Wifi: {comp.HasWifi}, Has LTE: {comp.HasLTE}, Release Date: {comp.ReleaseDate}, Price: {comp.Price}, Video Card: {comp.VideoCard}");
            // }


            string SELECTQuery2 = "SELECT * FROM TutorialAppSchema.Computer WHERE Motherboard = @Motherboard";
            var parameters = new { Motherboard = "ASUS ROG STRIX" };
           Computer? computer = dapper.LoadDataSingle<Computer>(SELECTQuery2, parameters);
if (computer != null)
{
    Console.WriteLine($"Found computer with Motherboard: {computer.Motherboard}, " +
        $"CPU Cores: {computer.CPUCores}, " +
        $"Has Wifi: {computer.HasWifi}, " +
        $"Has LTE: {computer.HasLTE}, " +
        $"Release Date: {computer.ReleaseDate}, " +
        $"Price: {computer.Price}, " +
        $"Video Card: {computer.VideoCard}");
}
else
{
    Console.WriteLine("No computer found.");
}

            
        }
    }
}