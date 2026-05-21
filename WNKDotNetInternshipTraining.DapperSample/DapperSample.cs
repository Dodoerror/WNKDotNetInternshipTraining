using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WNKDotNetInternshipTraining.DapperSample;

public class DapperSample
{
    private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "LLDotNeInternshiptTrainning",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };

    // CREATE
    public void Create()
    {
        string sql = @"INSERT INTO Tbl_Student
        (
            StudentNo,
            StudentName,
            FatherName,
            Address,
            DateOfBirth,
            IsDelete,
            CreatedDateTime,
            CreatedBy,
            ModifiedDateTime,
            ModifiedBy
        )
        VALUES
        (
            @StudentNo,
            @StudentName,
            @FatherName,
            @Address,
            @DateOfBirth,
            @IsDelete,
            @CreatedDateTime,
            @CreatedBy,
            @ModifiedDateTime,
            @ModifiedBy
        )";

        Student student = new Student()
        {
            StudentNo = "S-007",
            StudentName = "Mg Mg",
            FatherName = "U Ba",
            Address = "Yangon",
            DateOfBirth = new DateTime(2004, 10, 4),
            IsDelete = false,
            CreatedDateTime = DateTime.Now,
            CreatedBy = "1",
            ModifiedDateTime = DateTime.Now,
            ModifiedBy = "1"
        };

        using IDbConnection db = new SqlConnection(builder.ConnectionString);
        db.Open();

        db.Execute(sql, student);

        Console.WriteLine("Create Successful");
    }

    // READ
    public void Read()
    {
        string sql = "SELECT * FROM Tbl_Student WHERE IsDelete = 0";

        using IDbConnection db = new SqlConnection(builder.ConnectionString);
        db..Open();

        List<Student> students = db.Query<Student>(sql).ToList();

        foreach (Student item in students)
        {
            Console.WriteLine($"{item.StudentId} - {item.StudentName}");
        }
    }

    // UPDATE
    public void Update()
    {
        string sql = @"UPDATE Tbl_Student
                       SET StudentName = @StudentName
                       WHERE StudentId = @StudentId";

        Student student = new Student()
        {
            StudentId = 1,
            StudentName = "Updated Mg Mg"
        };

        using IDbConnection db = new SqlConnection(builder.ConnectionString);
        db.Open();

        db.Execute(sql, student);

        Console.WriteLine("Update Successful");
    }

    // DELETE
    public void Delete()
    {
        string sql = @"DELETE FROM Tbl_Student
                       WHERE StudentId = @StudentId";

        using IDbConnection db = new SqlConnection(builder.ConnectionString);
        db.Open();

        db.Execute(sql, new { StudentId = 1 });

        Console.WriteLine("Delete Successful");
    }
}