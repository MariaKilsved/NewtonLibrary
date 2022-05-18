using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace NewtonLibraryManager.Models
{


    public static class SecurePasswordHasher
    {
        public static string Hash(string pass)
        {

            //Hash a password using salt and streching
            byte[] encrypted = KeyDerivation.Pbkdf2(
                password: pass,
                salt: Encoding.UTF8.GetBytes("j78Y#p)/saREN!y3@"),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100,
                numBytesRequested: 64);

            string base64Encryption = Convert.ToBase64String(encrypted);
            Console.WriteLine(base64Encryption);
            return base64Encryption;
        }


        //    private const int SaltSize = 16;
        //    private const int HashSize = 64;

        //    public static string Hash(string pass, int iterations)
        //    {
        //        string salt1 = "oftrVkcTGsXvacTSUwgVjQ==";
        //        byte[] salt = Encoding.ASCII.GetBytes(salt1);


        //        var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, iterations);
        //        var hash = pbkdf2.GetBytes(HashSize);

        //        var hashBytes = new byte[SaltSize + HashSize];
        //        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        //        Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

        //        var base64Hash = Convert.ToBase64String(hashBytes);

        //        return String.Format(base64Hash);
        //    }

        //    public static string Hash(string pass)
        //    {
        //        return Hash(pass, 10000);
        //    }

        //    public static bool IsHashSupported(string hashString)
        //    {
        //        return hashString.Contains("$MYHASH$V1$");
        //    }

        //    //public static bool Verify(string pass, string hashedpass)
        //    //{
        //    //    //if (!IsHashSupported(hashedpass))
        //    //    //{
        //    //    //    throw new NotSupportedException("The hashtype is not supported");
        //    //    //}

        //    //    //var splittedHashString = hashedpass.Replace("$MYHASH$V1$", "").Split('$');
        //    //    //var iterations = int.Parse(splittedHashString[0]);
        //    //    //var base64Hash = splittedHashString[1];

        //    //    var hashbytes = Convert.FromBase64String(pass);

        //    //    var salt = new byte[SaltSize];
        //    //    Array.Copy(hashbytes, 0, salt, 0, SaltSize);

        //    //    var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, iterations);
        //    //    byte[] hash = pbkdf2.GetBytes(HashSize);

        //    //    for (int i = 0; i < HashSize; i++)
        //    //    {
        //    //        if (hashbytes[i + SaltSize] != hash[i])
        //    //        {
        //    //            return false;
        //    //        }
        //    //    }
        //    //    return true;
        //    //}
    }
}
