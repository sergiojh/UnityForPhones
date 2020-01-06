using System.Security.Cryptography;
using System.Text;
/// <summary>
/// Clase que se encarga de la seguridad del progreso.
/// </summary>
public class SecurityHelper
{
    MD5 md5;

    public SecurityHelper()
    {
        //Crea un hash MD5
        md5 = MD5.Create();
    }
    /// <summary>
    /// Encripta el archivo
    /// </summary>
    /// <param name="data">Contenido del archivo que se va a encriptar.</param>
    /// <returns>Texto encriptado.</returns>
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
