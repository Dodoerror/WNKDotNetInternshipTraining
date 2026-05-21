using System;

namespace WNKDotNetInternshipTraining.AdoDotNetSample;

class Program
{
    static void Main(string[] args)
    {
        
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        AdoDotNetSample adoDotNetSample = new AdoDotNetSample();

        adoDotNetSample.Read();
        adoDotNetSample.Edit();
        adoDotNetSample.Create();
        adoDotNetSample.Update();
        adoDotNetSample.Delete();
        adoDotNetSample.Read();
    }
}




