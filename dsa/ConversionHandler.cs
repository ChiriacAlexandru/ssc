using System;
using System.Text;

public static class ConversionHandler
{
    public static byte[] StringToByteArray(string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static string ByteArrayToHexString(byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "");
    }

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
