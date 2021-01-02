using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class EntityLayer
    {
        private int ID;
        private string AD;
        private string SOYAD;
        private string MAIL;
        private string SIFRE;
        private string HESAPNO;

        public int ID1 { get => ID; set => ID = value; }
        public string ADI { get => AD; set => AD = value; }
        public string SOYADI { get => SOYAD; set => SOYAD = value; }
        public string EMAIL { get => MAIL; set => MAIL = value; }
        public string SIFRESI { get => SIFRE; set => SIFRE = value; }
        public string HESAP_NO { get => HESAPNO; set => HESAPNO = value; }
    }
}
