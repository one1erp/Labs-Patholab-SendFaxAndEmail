using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using System.Diagnostics;//for debugger :)

using LSEXT;
using LSSERVICEPROVIDERLib;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;


using ADODB;


using Patholab_Common;
using Patholab_DAL_V1;

namespace SendFaxAndEmail
{

    [ComVisible(true)]
    [ProgId("SendFaxAndEmail.SendFaxAndEmail")]
    public class SendFaxAndEmailCls : IWorkflowExtension
    {
        INautilusServiceProvider sp;
        private DataLayer dal;
        private const string Type = "1";
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess,
        uint dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition,
        uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        public void Execute(ref LSExtensionParameters Parameters)
        {
            try
            {

                #region params
                
                string tableName = Parameters["TABLE_NAME"];

                sp = Parameters["SERVICE_PROVIDER"];
                var rs = Parameters["RECORDS"];



                Debugger.Launch();
                //Recordset rs = Parameters["RECORDS"];
                string firstSDG = rs["SDG_ID"].Value.ToString();
                rs.MoveLast();
                string tableID = rs.Fields["SDG_ID"].Value.ToString();
                string workstationId = Parameters["WORKSTATION_ID"].ToString();




                #endregion
                ////////////יוצר קונקשן//////////////////////////
                var ntlCon = Utils.GetNtlsCon(sp);
                Utils.CreateConstring(ntlCon);
                /////////////////////////////           
                dal = new DataLayer();
                dal.Connect(ntlCon);
               var sampleID = "";
                var sampleName = "";
                var sampleDscription = "";
                long sdgId = long.Parse(tableID);
                string filePath;
                string recipentName;
                string recipentType;
                string recipentID;
                string phoneNumber;
                //MessageBox.Show(name);
                if (tableName == "SDG")
                {

                    filePath="";
                    recipentName = "";
                    recipentType = "";
                    recipentID = "";
                    phoneNumber = "";
                    var FaxAndEmail = new FaxAndEmail(dal, filePath, phoneNumber, recipentName, recipentType, recipentID, sdgId);
                    
                
                    //SDG sdg = dal.get(Convert.ToInt32(rs.Fields["TEST_ID"].Value));
                    //sampleID = test.Aliquot.SampleId.ToString();
                    //sampleName = test.Aliquot.Sample.Name;
                    //sampleDscription = test.Aliquot.Sample.Description;
                }
                else
                {
                    MessageBox.Show("זה לא סדג");
                    //sampleName = rs.Fields["NAME"].Value;
                    //string description = rs.Fields["DESCRIPTION"].Value;
                    //if (description != null)
                    //{
                    //    sampleDscription = pg.ReverseString(description);
                    //}
                    //else
                    //{
                    //    sampleDscription = "";
                    //}
                    //var temp = rs.Fields["SAMPLE_ID"].Value;
                    //sampleID = temp.ToString();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("נכשלה שליחת התוצאה");
                Logger.WriteLogFile(ex);
            }
        }

      
    }
}
