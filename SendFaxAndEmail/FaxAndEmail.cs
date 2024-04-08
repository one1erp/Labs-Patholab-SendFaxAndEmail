using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patholab_Common;
using Patholab_DAL_V1;
//using MR_ClinicalDocument;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;


namespace SendFaxAndEmail
{
    class FaxAndEmail
    {

        DataLayer dal;
        private string workstationId;
        private string newEntry;


        public FaxAndEmail(DataLayer dal, string filePath,string phoneNumber, string recipentName, string recipentType, string recipentID, long SdgId)
        {
            try
            {

                //connect to dal
                Patholab_DAL_V1.SDG currentSDg = dal.FindBy<SDG>(s => s.SDG_ID == SdgId).SingleOrDefault();

                 
                DateTime sendTime = DateTime.Now;

                //convert pdf file to base 64

                //todo: get the real pdf
                string inputFileName = "c:\\algo.pdf";
                byte[] pdfBytes = File.ReadAllBytes(inputFileName);
                string[] pdfBase64 = new string[1];
              




            }
            catch (Exception ex)
            {
                MessageBox.Show("נכשלה שליחת התוצאה");
                Logger.WriteLogFile(ex);
            }

        }

        private string toYYMMDDFormat(DateTime? dateVal)
        {
            if (dateVal.HasValue)
                return ((DateTime)dateVal).ToString("yyyyMMdd");
            else
                return "";
        }

       
        private string nvl(string stringOrNull)
        {
            return stringOrNull == null ? "" : stringOrNull;
        }
    }
}
