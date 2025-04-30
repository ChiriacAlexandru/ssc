using System;
using System.Text;

public static class ConversionHandler
{
    // Metoda pentru a transforma un string în array de octeți (byte array)
    public static byte[] StringToByteArray(string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    // Metoda pentru a transforma un array de octeți într-un șir hexadecimal
    public static string ByteArrayToHexString(byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "");
    }

    // Metoda pentru a transforma un șir hexadecimal într-un array de octeți
    public static byte[] HexStringToByteArray(string hex)
    {
        int numberChars = hex.Length;
        byte[] bytes = new byte[numberChars / 2];
        for (int i = 0; i < numberChars; i += 2)
        {
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        }
        return bytes;
    }
}
