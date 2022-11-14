using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StrongPasswordGen
{
    class Program
    {
        static RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();

        public static string randompasswordgenerator()
        {

            int PasswordAmount = 0;
            int PasswordLength = 0;

            string CapitalLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string SmallLetters = "qwertyuiopasdfghjklzxcvbnm";
            string Digits = "0123456789";
            string SpecialCharacters = "!@#$^&";
            string AllChar = CapitalLetters + SmallLetters + Digits + SpecialCharacters;



            // Console.WriteLine("\nHow many passwords should be generated?:");
            //PasswordAmount = int.Parse(Console.ReadLine());
            PasswordAmount = 1;
            //Console.WriteLine("Enter the password length (chars):");
            //PasswordLength = int.Parse(Console.ReadLine());
            PasswordLength = 8;

            string[] AllPasswords = new string[PasswordAmount];

            do
            {

                for (int i = 0; i < PasswordAmount; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int n = 0; n < PasswordLength; n++)
                    {
                        sb = sb.Append(GenerateChar(AllChar));
                    }

                    AllPasswords[i] = sb.ToString();
                }

                Console.WriteLine("Generated passwords:");
                //while(true){
                //Task.Delay(TimeSpan.FromSeconds(2)).Wait();
                foreach (string singlePassword in AllPasswords)
                {
                    return singlePassword;
                }
                Task.Delay(TimeSpan.FromSeconds(60)).Wait();
            } while (true);

        }

        private static char GenerateChar(string availableChars)
        {
            var byteArray = new byte[1];
            char c;
            do
            {
                provider.GetBytes(byteArray);
                c = (char)byteArray[0];

            } while (!availableChars.Any(x => x == c));

            return c;
        }
    }

}
