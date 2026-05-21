using System;


namespace WNKDotNetInternshipTraining.AdoDotNetSample;

public class Student
{
    public int StudentId { get; set; }
    public string StudentNo { get; set; }
    public string StudentName { get; set; }
    public string FatherName { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string ModifiedBy { get; set; }
}