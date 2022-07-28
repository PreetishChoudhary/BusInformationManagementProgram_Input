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

        /// <summary>
        /// The ReadFile module reads a specific file depending on the given bus number and type. The module saves the information from the file and stores it in a record.
        /// </summary>
        /// <param name="busNumber">The number of the bus being specified.</param>
        /// <param name="type">The type of bus being specified, either 'school' or 'sport'.</param>
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

        /// <summary>
        /// The CreateRecord module creates a new entry for a specified bus. The module adds to the xml document of the specified bus along with appending the new information in the respective bus record.
        /// </summary>
        /// <param name="busNumber"></param>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <param name="status"></param>
        /// <param name="type"></param>
        void CreateRecord(string busNumber, string time, string date, string status, string type)
        {
            if (type == "school")
            {
                type = "SchoolBuses/";
            }
            else
            {
                type = "SportBuses/";
            }
            BusRecord[] testRecord = busRecordArray[Convert.ToInt32(busNumber)];
            BusRecord[] record = new BusRecord[testRecord.Length + 1];
            for(int i = 0; i<testRecord.Length; i++)
            {
                record[i] = testRecord[i];
            }
            BusRecord b = new BusRecord();
            b.busNumber = Convert.ToInt32(busNumber) + 1;
            b.arrivalDate = Convert.ToInt32(date); record.Append(b);

            XmlDocument doc = new XmlDocument();
            doc.Load(type + "BusData_" + b.busNumber + ".xml");
            XmlNode root = doc.DocumentElement;
            //Create a new node.
            XmlElement busData = doc.CreateElement("BusData");
            XmlElement atSchool = doc.CreateElement("atSchool");
            XmlElement arrivalTime = doc.CreateElement("arrivalTime");
            XmlElement departureTime = doc.CreateElement("departureTime");
            departureTime.InnerText = "0";
            arrivalTime.InnerText = "0";
            b.arrivalTime = 0;
            b.departTime = 0;
            busData.AppendChild(atSchool);

            if (type == "SchoolBuses/")
            {
                busData.AppendChild(arrivalTime);
                busData.AppendChild(departureTime);

                if (status == "In")
                {
                    b.atSchool = false;
                    departureTime.InnerText = time;
                    atSchool.InnerText = "False";
                    b.departTime = Convert.ToInt32(time);
                    root.LastChild.FirstChild.InnerText = "False";
                    root.LastChild.LastChild.InnerText = time;
                    busData.AppendChild(atSchool);
                    busData.AppendChild(departureTime);

                    testRecord[testRecord.Length - 1].atSchool = b.atSchool;
                    testRecord[testRecord.Length - 1].arrivalTime = b.arrivalTime;

                    busRecordArray[Convert.ToInt32(busNumber)] = testRecord;
                }

                if (status == "none")
                {
                    b.atSchool = true;
                    busData.SetAttribute("date", date);
                    root.AppendChild(busData);
                    arrivalTime.InnerText = time;
                    atSchool.InnerText = "True";
                    b.arrivalTime = Convert.ToInt32(time);
                    busData.AppendChild(atSchool);
                    busData.AppendChild(arrivalTime);
                    busData.AppendChild(departureTime);

                    record[record.Length - 1] = b;

                    busRecordArray[Convert.ToInt32(busNumber)] = record;
                }
            }
            else
            {
                busData.AppendChild(departureTime);
                busData.AppendChild(arrivalTime);

                if (status == "Out")
                {
                    b.atSchool = true;
                    arrivalTime.InnerText = time;
                    atSchool.InnerText = "True";
                    b.arrivalTime = Convert.ToInt32(time);
                    root.LastChild.FirstChild.InnerText = "False";
                    root.LastChild.LastChild.InnerText = time;
                    busData.AppendChild(atSchool);
                    busData.AppendChild(arrivalTime);

                    testRecord[testRecord.Length - 1].atSchool = b.atSchool;
                    testRecord[testRecord.Length - 1].arrivalTime = b.arrivalTime;

                    sportBusRecordArray[Convert.ToInt32(busNumber)] = testRecord;
                }

                if (status == "none")
                {
                    b.atSchool = false;
                    busData.SetAttribute("date", date);
                    root.AppendChild(busData);
                    departureTime.InnerText = time;
                    atSchool.InnerText = "False";
                    b.departTime = Convert.ToInt32(time);
                    busData.AppendChild(atSchool);
                    busData.AppendChild(departureTime);
                    busData.AppendChild(arrivalTime);

                    record[record.Length - 1] = b;

                    sportBusRecordArray[Convert.ToInt32(busNumber)] = record;
                }
            }

            doc.Save(type + "BusData_" + b.busNumber + ".xml");
        }

        /// <summary>
        /// The CheckStatus module retrieves the record for a specified bus number and searches for an object containing a specified date. If it is able to find an object containing the date, it returns the value of the atSchool property of the object, otherwise it returns 'none'.
        /// </summary>
        /// <param name="busNumber">The number of the bus being specified.</param>
        /// <param name="date">The date being specified.</param>
        /// <returns></returns>
        string CheckStatus(int busNumber, string date)
        {
            BusRecord[] record = busRecordArray[busNumber];
            if (cmbBxBusType.SelectedIndex == 1)
            {
                record = sportBusRecordArray[busNumber];
            }
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

        /// <summary>
        /// The FillBusComboBox module reads a file containing each individual bus number and places it in a comboBox.
        /// </summary>
        /// <param name="filePath">The path for the file being read.</param>
        void FillBusComboBox(string filePath)
        {
            cmbBxBusNumber.Items.Clear();
            string[] fileText = System.IO.File.ReadAllLines(filePath);
            foreach(string line in fileText) 
            {
                cmbBxBusNumber.Items.Add(line);
            }
        }

        BusRecord[] busRecord;
        List<BusRecord[]> busRecordArray = new List<BusRecord[]>();
        List<BusRecord[]> sportBusRecordArray = new List<BusRecord[]>();
        string[] busTypes = new string[] { "School", "Sport" };

        private void Form1_Load(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm");
            string date = DateTime.Now.ToString("dd/MM/yy");

            currentTime.Text = time;
            currentDate.Text = date;

            cmbBxBusType.Items.AddRange(busTypes);

            for(int i = 1; i<22; i++)
            {
                ReadFile(i, "school");
                busRecordArray.Add(busRecord);
            }
            for (int i = 1; i < 7; i++)
            {
                ReadFile(i, "sport");
                sportBusRecordArray.Add(busRecord);
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string status = CheckStatus(Convert.ToInt32(cmbBxBusNumber.SelectedIndex.ToString()), DateTime.Now.ToString("ddMMyy"));

            string type = "school";
            if (cmbBxBusType.SelectedIndex == 1)
            {
                type = "sport";
            }
            CreateRecord(cmbBxBusNumber.SelectedIndex.ToString(), DateTime.Now.ToString("HHmm"), DateTime.Now.ToString("ddMMyy"), status, type);

            if (status == "none")
            {
                if (cmbBxBusType.SelectedIndex == 0)
                {
                    btnCheck.Text = "Check Out";
                }
                else
                {
                    btnCheck.Text = "Check In";
                }
            }
            else if (status == "In")
            {
                btnCheck.Text = "Check In";
            }
            else
            {
                btnCheck.Text = "Check Out";
            }
        }

        private void cmbBxBusNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HH:mm");
            string date = DateTime.Now.ToString("dd/MM/yy");

            currentTime.Text = time;
            currentDate.Text = date;
            string status = CheckStatus(Convert.ToInt32(cmbBxBusNumber.SelectedIndex.ToString()), DateTime.Now.ToString("ddMMyy"));
            if(status == "none")
            {
                if (cmbBxBusType.SelectedIndex == 1)
                {
                    btnCheck.Text = "Check Out";
                }
                else
                {
                    btnCheck.Text = "Check In";
                }
            }
            else if(status == "In")
            {
                 btnCheck.Text = "Check Out";
            }
            else
            {
                btnCheck.Text = "Check In";
            }
        }

        private void cmbBxBusType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbBxBusType.SelectedIndex == 0)
            {
                FillBusComboBox("BusList.txt");
            }
            else
            {
                FillBusComboBox("SportsBusList.txt");
            }
        }
    }
}
