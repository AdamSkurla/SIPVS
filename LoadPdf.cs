namespace SIPVS
{
    public class LoadPdf
    {
       public static string LoadFileAsBase64(string filePath)
{
    try
    {
        // Read the file as bytes.
        byte[] fileBytes = File.ReadAllBytes(filePath);

        // Convert the bytes to a Base64 string.
        string base64String = Convert.ToBase64String(fileBytes);
        
        // string base64String = Convert.ToBase64String(fileBytes).Replace("\r\n", "").Replace("\n", "");


        // Console.WriteLine("File converted to Base64.");
        // Console.WriteLine(base64String);

        return base64String;
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("File not found.");
        return null;
    }
    catch (IOException)
    {
        Console.WriteLine("An error occurred while reading the file.");
        return null;
    }
}

    }
}