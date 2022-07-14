using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace BusInformationManagementProgram_Input
{
    public partial class Form1 : Form
    {
        class BusRecord
        {
            public int busNumber;
            public int arrivalDate;
            public int arrivalTime;
            public int departTime;
            public bool atSchool;
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void ReadFile(int busNumber, string type)
        {
            if (type == "school")
            {
                type = "SchoolBuses/";
            }
            else
            {
                type = "SportBuses/";
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(type + "BusData_" + busNumber + ".xml");

            XmlNodeList xmlBusRecord = doc.GetElementsByTagName("BusData");

            busRecord = new BusRecord[xmlBusRecord.Count];

            int index = 0;
            foreach (XmlNode busData in xmlBusRecord)
            {
                int arrivalDate = Convert.ToInt32(busData.Attributes["date"].Value);
                BusRecord b = new BusRecord();
                int arrivalTime = Convert.ToInt32(busData["arrivalTime"].InnerText);
                int departTime = Convert.ToInt32(busData["departureTime"].InnerText);
                bool status = Convert.ToBoolean(busData["atSchool"].InnerText);

                b.busNumber = busNumber;
                b.arrivalDate = arrivalDate;
                b.arrivalTime = arrivalTime;
                b.departTime = departTime;
                b.atSchool = status;

                busRecord[index] = b;
                index++;
            }
        }
        void CreateRecord(string busNumber, string time, string date, string status)
        {
            string type = "SchoolBuses/";
            BusRecord[] record = busRecordArray[Convert.ToInt32(busNumber)];
            BusRecord b = new BusRecord();
            b.busNumber = Convert.ToInt32(busNumber) + 1;
            b.arrivalDate = Convert.ToInt32(date);
            record.Append(b);

            XmlDocument doc = new XmlDocument();
            doc.Load(type + "BusData_" + busNumber + ".xml");
            XmlNode root = doc.DocumentElement;
            //Create a new node.
            XmlElement busData = doc.CreateElement("BusData");
            XmlElement atSchool = doc.CreateElement("atSchool");
            XmlElement arrivalStatus = doc.CreateElement("arrivalTime");
            XmlElement departureTime = doc.CreateElement("departureTime");
            busData.AppendChild(atSchool);
            root.AppendChild(busData);
            doc.Save(type + "BusData_" + busNumber + ".xml");
        }

        string CheckStatus(int busNumber, string date)
        {
            BusRecord[] record = busRecordArray[busNumber];
            int end = record.Length - 1;
            if (record[end].arrivalDate == Convert.ToInt32(date))
            {
                if (record[end].atSchool == true)
                {
                    return "In";
                }
                else
                {
                    return "Out";
                }
            }
            return "none";
        }

        void FillComboBox(string filePath)
        {
            string[] fileText = System.IO.File.ReadAllLines(filePath);
            foreach(string line in fileText) 
            {
                cmbBxBusNumber.Items.Add(line);
            }
        }

        BusRecord[] busRecord;
        List<BusRecord[]> busRecordArray = new List<BusRecord[]>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //string time = DateTime.Now.ToString("HH:mm");
            FillComboBox("BusList.txt");

            for(int i = 1; i<22; i++)
            {
                ReadFile(i, "school");
                busRecordArray.Add(busRecord);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm");
            string date = DateTime.Now.ToString("dd/MM/yy");

            currentTime.Text = time;
            currentDate.Text = date;
            string status = CheckStatus(Convert.ToInt32(cmbBxBusNumber.SelectedIndex.ToString()), DateTime.Now.ToString("ddMMyy"));
            /*if(status == "none")
            {
                btnCheck.Text = "Check In";
            }
            else if (status == "In")
            {
                btnCheck.Text = "Check Out";
            }
            else
            {
                btnCheck.Text = "Check In";
            }
            */

            CreateRecord(cmbBxBusNumber.SelectedIndex.ToString(), DateTime.Now.ToString("HHmm"), DateTime.Now.ToString("ddMMyy"), status);
        }
    }
}
