using System;

namespace WNKDotNetInternshipTraining.DapperSample; // Matches Student.cs

class Program
{
    static void Main(string[] args)
    {
        DapperSample dapper = new DapperSample();
        dapper.Read();
        dapper.Create();
    }
}