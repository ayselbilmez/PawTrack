using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawTrack.Business.DataProtection
{
    public interface IDataProtection
    {
        string Encrypt(string text);

        string Decrypt(string protectedText);

    }
}
