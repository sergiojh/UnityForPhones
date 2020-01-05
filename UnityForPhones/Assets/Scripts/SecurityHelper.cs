using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
public class SecurityHelper
{
    MD5 md5;

    public SecurityHelper()
    {
        md5 = MD5.Create();
    }

    public string encript(string data)
    {
        byte[] inputBytes = Encoding.ASCII.GetBytes(data);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }
}
