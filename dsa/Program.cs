using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        Console.Write("Key size (512 to 1024; 64-bit steps): ");
        int keySize = int.Parse(Console.ReadLine() ?? "1024");

        DSACryptoServiceProvider dsaProvider = new(keySize);

        string keyData = dsaProvider.ToXmlString(true);
        Console.WriteLine("Key data (cu cheia privată):");
        Console.WriteLine(keyData);

        Console.Write("Message to be signed: ");
        string plaintext = Console.ReadLine() ?? string.Empty;

        byte[] signature = dsaProvider.SignData(ConversionHandler.StringToByteArray(plaintext));
        string signatureHex = ConversionHandler.ByteArrayToHexString(signature);
        Console.WriteLine($"Signature (hex): {signatureHex}");

        Console.Write("Message to verify: ");
        string messageToCheck = Console.ReadLine() ?? string.Empty;
        bool verified = dsaProvider.VerifyData(ConversionHandler.StringToByteArray(messageToCheck), signature);
        Console.WriteLine($"Signature is valid: {verified}");
    }
}
