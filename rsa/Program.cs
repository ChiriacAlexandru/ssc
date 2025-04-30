using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        // 1. Citește dimensiunea cheii (minim 512 biți)
        Console.Write("Key size (512 to 16384; 8 bit steps): ");
        int keySize = int.Parse(Console.ReadLine() ?? "1024");

        // Verifică dacă dimensiunea cheii este validă
        if (keySize < 512 || keySize % 8 != 0)
        {
            Console.WriteLine("Dimensiunea cheii nu este validă. Trebuie să fie cel puțin 512 și multiplu de 8.");
            return;
        }

        // 2. Creează un RSA provider cu dimensiunea cheii dată
        RSACryptoServiceProvider rsaProvider = new(keySize);

        // 3. Afișează cheia cu tot cu membrii privați
        string keyData = rsaProvider.ToXmlString(true);
        Console.WriteLine("Cheia RSA (cu cheia privată):");
        Console.WriteLine(keyData);

        // 4. Citește mesajul care va fi semnat
        Console.Write("Message to be signed: ");
        string plaintext = Console.ReadLine() ?? string.Empty;

        // Verifică dacă mesajul nu este gol
        if (string.IsNullOrWhiteSpace(plaintext))
        {
            Console.WriteLine("Mesajul nu poate fi gol. Încearcă din nou.");
            return;
        }

        // 5. Semnează mesajul folosind SHA256
        byte[] signature = null;
        using (SHA256Managed sha256 = new SHA256Managed())
        {
            // Transformă mesajul în array de octeți
            byte[] dataBytes = ConversionHandler.StringToByteArray(plaintext);

            // Semnează datele cu RSA și SHA256
            signature = rsaProvider.SignData(dataBytes, sha256);
        }
        string signatureHex = ConversionHandler.ByteArrayToHexString(signature);
        Console.WriteLine($"Signature (hex): {signatureHex}");

        // 6. Verifică semnătura
        Console.Write("Message to verify: ");
        string messageToVerify = Console.ReadLine() ?? string.Empty;

        // Verifică dacă mesajul de verificat nu este gol
        if (string.IsNullOrWhiteSpace(messageToVerify))
        {
            Console.WriteLine("Mesajul de verificat nu poate fi gol. Încearcă din nou.");
            return;
        }

        using (SHA256Managed sha256 = new SHA256Managed())
        {
            byte[] messageBytes = ConversionHandler.StringToByteArray(messageToVerify);
            byte[] hashMessage = sha256.ComputeHash(messageBytes); // Creează hash-ul pentru mesajul de verificat
            bool verified = rsaProvider.VerifyData(messageBytes, sha256, signature);
            Console.WriteLine($"Signature is valid: {verified}");
        }
    }
}
