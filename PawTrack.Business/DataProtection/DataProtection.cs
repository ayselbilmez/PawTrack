using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.DataProtection
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protector;

        public DataProtection(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("PawTrack");
        }

        public string Encrypt(string text)
        {
            return _protector.Protect(text);
        }

        public string Decrypt(string protectedText)
        {
            return _protector.Unprotect(protectedText);
        }
    }
}

