using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.SqlClient;

namespace WNKDotNetInternshipTraining.DapperSample;

public class DapperSample
{
    private readonly SqlConnectionStringBuilder builder =
        new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "LLDotNeInternshiptTrainning",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

    // READ
    public void Read()
    {
        string sql = @"SELECT TOP (1000)
                       [StudentId],
                       [StudentNo],
                       [StudentName],
                       [FatherName],
                       [Address],
                       [DateOfBirth],
                       [IsDelete],
                       [CreatedDateTime],
                       [CreatedBy],
                       [ModifiedDateTime],
                       [ModifiedBy]
                       FROM [LLDotNeInternshiptTrainning].[dbo].[Tbl_Student]
                       WHERE IsDelete = 0";

        using IDbConnection sqlConnection =
            new SqlConnection(builder.ConnectionString);

        sqlConnection.Open();

        List<Student> lst =
            sqlConnection.Query<Student>(sql).ToList();

        foreach (Student item in lst)
        {
            Console.WriteLine(
                $"StudentId: {item.StudentId}, " +
                $"StudentNo: {item.StudentNo}, " +
                $"StudentName: {item.StudentName}, " +
                $"FatherName: {item.FatherName}"
            );
        }
    }

    // EDIT
    public void Edit()
    {
        string sql = @"SELECT TOP (1000)
                       [StudentId],
                       [StudentNo],
                       [StudentName],
                       [FatherName],
                       [Address],
                       [DateOfBirth],
                       [IsDelete],
                       [CreatedDateTime],
                       [CreatedBy],
                       [ModifiedDateTime],
                       [ModifiedBy]
                       FROM [LLDotNeInternshiptTrainning].[dbo].[Tbl_Student]
                       WHERE StudentId = @StudentId
                       AND IsDelete = 0";

        using IDbConnection sqlConnection =
            new SqlConnection(builder.ConnectionString);

        sqlConnection.Open();

        Student item =
            sqlConnection.Query<Student>(
                sql,
                new Student
                {
                    StudentId = 1
                }
            ).FirstOrDefault();

        if (item is null)
        {
            Console.WriteLine("Data Not Found");
            return;
        }

        Console.WriteLine(
            $"StudentId: {item.StudentId}, " +
            $"StudentNo: {item.StudentNo}, " +
            $"StudentName: {item.StudentName}, " +
            $"FatherName: {item.FatherName}"
        );
    }

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
            StudentNo = "S-001",
            StudentName = "Wint",
            FatherName = "U Ba",
            Address = "Yangon",
            DateOfBirth = new DateTime(2004, 10, 4),
            IsDelete = false,
            CreatedDateTime = DateTime.Now,
            CreatedBy = "1",
            ModifiedDateTime = DateTime.Now,
            ModifiedBy = "1"
        };

        using IDbConnection sqlConnection =
            new SqlConnection(builder.ConnectionString);

        sqlConnection.Open();

        int result =
            sqlConnection.Execute(sql, student);

        Console.WriteLine(
            result > 0
            ? "Saving Successful"
            : "Saving Failed"
        );
    }

    // UPDATE
    public void Update()
    {
        string sql = @"UPDATE Tbl_Student
                       SET
                           StudentNo = @StudentNo,
                           StudentName = @StudentName,
                           FatherName = @FatherName,
                           Address = @Address,
                           DateOfBirth = @DateOfBirth,
                           ModifiedDateTime = @ModifiedDateTime,
                           ModifiedBy = @ModifiedBy
                       WHERE StudentId = @StudentId
                       AND IsDelete = 0";

        Student student = new Student()
        {
            StudentId = 1,
            StudentNo = "S-001",
            StudentName = "Updated Wint",
            FatherName = "U Tun",
            Address = "Mandalay",
            DateOfBirth = new DateTime(2004, 10, 4),
            ModifiedDateTime = DateTime.Now,
            ModifiedBy = "1"
        };

        using IDbConnection sqlConnection =
            new SqlConnection(builder.ConnectionString);

        sqlConnection.Open();

        int result =
            sqlConnection.Execute(sql, student);

        Console.WriteLine(
            result > 0
            ? "Updating Successful"
            : "Updating Failed"
        );
    }

    // DELETE
    public void Delete()
    {
        string sql = @"UPDATE Tbl_Student
                       SET
                           IsDelete = 1,
                           ModifiedDateTime = @ModifiedDateTime,
                           ModifiedBy = @ModifiedBy
                       WHERE StudentId = @StudentId";

        var student = new
        {
            StudentId = 1,
            ModifiedDateTime = DateTime.Now,
            ModifiedBy = "1"
        };

        using IDbConnection sqlConnection =
            new SqlConnection(builder.ConnectionString);

        sqlConnection.Open();

        int result =
            sqlConnection.Execute(sql, student);

        Console.WriteLine(
            result > 0
            ? "Deleting Successful"
            : "Deleting Failed"
        );
    }
}