using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace WNKDotNetInternshipTraining.AdoDotNetSample;

public class AdoDotNetSample
{
    private readonly SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "LLDotNeInternshiptTrainning",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };

    public void Read()
    {
        SqlConnection connection = new SqlConnection(builder.ConnectionString);
        connection.Open();

        string sql = @"SELECT [StudentId]
      ,[StudentNo]
      ,[StudentName]
      ,[FatherName]
      ,[Address]
      ,[DateOfBirth]
      ,[IsDelete]
      ,[CreatedDateTime]
      ,[CreatedBy]
      ,[ModifiedDateTime]
      ,[ModifiedBy]
  FROM [LLDotNeInternshiptTrainning].[dbo].[Tbl_Student] Where IsDelete = 0";

        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        connection.Close();

        List<Student> students = new List<Student>();
        foreach (DataRow row in dataTable.Rows)
        {
            Student student = new Student()
            {
                StudentId = Convert.ToInt32(row["StudentId"]),
                StudentNo = Convert.ToString(row["StudentNo"]).Trim(),
                StudentName = Convert.ToString(row["StudentName"]).Trim(),
                FatherName = Convert.ToString(row["FatherName"]).Trim(),
                Address = Convert.ToString(row["Address"]).Trim(),
                DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                IsDelete = Convert.ToString(row["IsDelete"]) == "1" || Convert.ToString(row["IsDelete"]).Equals("true", StringComparison.OrdinalIgnoreCase),
                CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]),
                CreatedBy = Convert.ToString(row["CreatedBy"]).Trim(),
                ModifiedDateTime = row["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDateTime"]),
                ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : Convert.ToString(row["ModifiedBy"]).Trim(),
            };
            students.Add(student);

            // Clean & simple print structure matching your friend's layout exactly
            System.Console.WriteLine($"StudentID: {student.StudentId}, StudentNo: {student.StudentNo} \t , StudentName: {student.StudentName} \t , DateOfBirth: {student.DateOfBirth.ToString("dd/MMM/yyyy")}");
            System.Console.WriteLine("***");
        }
    }

    public void Edit()
    {
        string sql = @"SELECT [StudentId]
      ,[StudentNo]
      ,[StudentName]
      ,[FatherName]
      ,[Address]
      ,[DateOfBirth]
      ,[IsDelete]
      ,[CreatedDateTime]
      ,[CreatedBy]
      ,[ModifiedDateTime]
      ,[ModifiedBy]
  FROM [LLDotNeInternshiptTrainning].[dbo].[Tbl_Student] Where StudentId = @StudentId and IsDelete = 0";

        SqlConnection connection = new SqlConnection(builder.ConnectionString);
        connection.Open();
        int id = 4;

        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@StudentId", id);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        adapter.Fill(dataTable);
        connection.Close();

        if (dataTable.Rows.Count == 0)
        {
            System.Console.WriteLine("Data not found");
            return;
        }

        DataRow row = dataTable.Rows[0];
        Student student = new Student()
        {
            StudentId = Convert.ToInt32(row["StudentId"]),
            StudentNo = Convert.ToString(row["StudentNo"]).Trim(),
            StudentName = Convert.ToString(row["StudentName"]).Trim(),
            FatherName = Convert.ToString(row["FatherName"]).Trim(),
            Address = Convert.ToString(row["Address"]).Trim(),
            DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
            IsDelete = Convert.ToString(row["IsDelete"]) == "1" || Convert.ToString(row["IsDelete"]).Equals("true", StringComparison.OrdinalIgnoreCase),
            CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]),
            CreatedBy = Convert.ToString(row["CreatedBy"]).Trim(),
            ModifiedDateTime = row["ModifiedDateTime"] == DBNull.Value ? null : Convert.ToDateTime(row["ModifiedDateTime"]),
            ModifiedBy = row["ModifiedBy"] == DBNull.Value ? null : Convert.ToString(row["ModifiedBy"]).Trim(),
        };

        System.Console.WriteLine($"StudentID: {student.StudentId}, StudentNo: {student.StudentNo} \t , StudentName: {student.StudentName} \t , DateOfBirth: {student.DateOfBirth.ToString("dd/MMM/yyyy")}");
        System.Console.WriteLine("***");
    }

    public void Create()
    {
        Student student = new Student()
        {
            StudentNo = "STU002",
            StudentName = "Kyaw Ba",
            FatherName = "U Ba",
            Address = "Yangon",
            DateOfBirth = new DateTime(2004, 4, 10),
            IsDelete = false,
            CreatedDateTime = DateTime.Now,
            CreatedBy = "Admin"
        };

        SqlConnection connection = new SqlConnection(builder.ConnectionString);
        connection.Open();

        string sql = @"INSERT INTO [dbo].[Tbl_Student]
           ([StudentNo],[StudentName],[FatherName],[Address],[DateOfBirth],[IsDelete],[CreatedDateTime],[CreatedBy])
     VALUES
           (@StudentNo,@StudentName,@FatherName,@Address,@DateOfBirth,@IsDelete,@CreatedDateTime,@CreatedBy)";

        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@StudentNo", student.StudentNo);
        command.Parameters.AddWithValue("@StudentName", student.StudentName);
        command.Parameters.AddWithValue("@FatherName", student.FatherName);
        command.Parameters.AddWithValue("@Address", student.Address);
        command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
        command.Parameters.AddWithValue("@IsDelete", student.IsDelete ? "1" : "0");
        command.Parameters.AddWithValue("@CreatedDateTime", student.CreatedDateTime);
        command.Parameters.AddWithValue("@CreatedBy", student.CreatedBy);

        int result = command.ExecuteNonQuery();
        connection.Close();

        Console.WriteLine(result > 0 ? "Saving Successful." : "Saving Failed.");
        System.Console.WriteLine("***");
    }

    public void Update()
    {
        Student student = new Student()
        {
            StudentId = 2,
            StudentNo = "STU002",
            StudentName = "Aung Min",
            FatherName = "U Lwin",
            Address = "Mandalay",
            DateOfBirth = new DateTime(2000, 5, 2),
            IsDelete = false,
            ModifiedBy = "Admin"
        };

        SqlConnection connection = new SqlConnection(builder.ConnectionString);
        connection.Open();

        string sql = @"UPDATE [dbo].[Tbl_Student]
                   SET [StudentName] = @StudentName
                      ,[FatherName] = @FatherName
                      ,[Address] = @Address
                      ,[DateOfBirth] = @DateOfBirth
                      ,[IsDelete] = @IsDelete
                      ,[ModifiedDateTime] = @ModifiedDateTime
                      ,[ModifiedBy] = @ModifiedBy
                 WHERE StudentNo = @StudentNo";

        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@StudentNo", student.StudentNo);
        command.Parameters.AddWithValue("@StudentName", student.StudentName);
        command.Parameters.AddWithValue("@FatherName", student.FatherName);
        command.Parameters.AddWithValue("@Address", student.Address);
        command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
        command.Parameters.AddWithValue("@IsDelete", student.IsDelete ? "1" : "0");
        command.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
        command.Parameters.AddWithValue("@ModifiedBy", student.ModifiedBy);

        int result = command.ExecuteNonQuery();
        connection.Close();

        Console.WriteLine(result > 0 ? "Updating Successful." : "Updating Failed.");
        System.Console.WriteLine("***");
    }

    public void Delete()
    {
        using SqlConnection connection = new SqlConnection(builder.ConnectionString);
        connection.Open();
        int id = 4;

        string sql = @"UPDATE [dbo].[Tbl_Student] 
                   SET [IsDelete] = @IsDelete, 
                       [ModifiedDateTime] = @ModifiedDateTime, 
                       [ModifiedBy] = @ModifiedBy 
                   WHERE [StudentId] = @StudentId";

        using SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@StudentId", id);
        command.Parameters.AddWithValue("@IsDelete", "1");
        command.Parameters.AddWithValue("@ModifiedDateTime", DateTime.Now);
        command.Parameters.AddWithValue("@ModifiedBy", "Admin");

        int result = command.ExecuteNonQuery();
        connection.Close();

        Console.WriteLine(result > 0 ? "Student marked as deleted successfully." : "Delete failed.");
        System.Console.WriteLine("***");
    }
}