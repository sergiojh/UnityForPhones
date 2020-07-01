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
    /// Resume el contenido en un hash MD5
    /// </summary>
    /// <param name="data">Contenido del archivo que se va a resumir en hash.</param>
    /// <returns>Texto generado por el hash.</returns>
    public string CreateMD5(string data)
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
