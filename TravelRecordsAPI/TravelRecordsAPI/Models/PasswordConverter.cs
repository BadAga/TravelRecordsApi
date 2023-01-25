using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;


namespace TravelRecordsAPI.Models
{
    public class PasswordConverter
    {
        private string simplePassword; //unhashed password
        private string hashedPassword;

        public PasswordConverter(string simplePassword)
        {
            this.simplePassword = simplePassword;
            this.hashedPassword=String.Empty;
            HashPassword();
        }

        private void HashPassword()
        {
            SHA256 sHA256 = SHA256.Create();
            //changing simple passowrd to an array of bytes
            Byte[] passwodBytes=Encoding.Default.GetBytes(simplePassword);
            Byte[] hashedPassBytes=sHA256.ComputeHash(passwodBytes);

            String hashedPassword = Encoding.UTF8.GetString(hashedPassBytes);

            this.hashedPassword=hashedPassword;
        }

        public bool Compare(string hashedPasswordFromDB) => this.hashedPassword.Equals(hashedPasswordFromDB) ? true : false;
       
        public String GetHashedPassword()
        {
            return this.hashedPassword;
        }


    }
}
