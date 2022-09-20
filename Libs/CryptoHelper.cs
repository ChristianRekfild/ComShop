using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ComShop.Libs
{
    /// <summary>
    /// Класс дря работы с криптохэшем SHA256
    /// </summary>
    internal class CryptoHelper
    {
        /// <summary>
        /// Создаёт хэш на основе принимаемой в качестве строки аргумента
        /// </summary>
        /// <param name="rawData">Строка для последующего вычисления хэша</param>
        /// <returns>Hash, сгененрированный из строки</returns>
        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        
    }

    

    
    
}
