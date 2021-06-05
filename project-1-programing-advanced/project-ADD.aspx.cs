using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

namespace project_1_programing_advanced
{
    public partial class project_ADD : System.Web.UI.Page
    {
        int iD = 0;
        string connectionstring = "Data Source=DESKTOP-OS4TRPA;Initial Catalog=Doctors;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static string path = @"C:\Users\Tofiq\source\repos\project-1-programing-advanced\project-1-programing-advanced\project.xml";
        XDocument xDocument = XDocument.Load(path);
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(xDocument.Root.Document.ToString());
            DataSet dataset = new DataSet();
            dataset.ReadXml(Server.MapPath("project.xml"));
            DataTable dataTable = dataset.Tables["Doctor"];
            try
            {
                if (dataset.Tables.Count==0)
                {
                    SqlConnection sqlConnection = new SqlConnection(connectionstring);
                    SqlCommand sqlCommand = new SqlCommand("select max(ID) from Doctor;",sqlConnection);
                    sqlConnection.Open();

                    

                    iD = Convert.ToInt32(sqlCommand.ExecuteScalar());

                    sqlConnection.Close();
                    iD++;
                    return;
                }
                var id = dataTable.Rows;
                foreach (DataRow dr in id)
                {
                    iD = Convert.ToInt32(dr["ID"]);
                    iD++;
                }
            }
            catch (Exception E)
            {
                Response.Write(E.Message);
                return;
            }
            

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            xDocument.Element("Doctors").Add(new XElement("Doctor",
                new XElement("ID", iD),
                new XElement("FirstN", TextBox2.Text),
                new XElement("LastN", TextBox3.Text),
                new XElement("Age", TextBox4.Text)
                ));
            xDocument.Save(path);
            Response.Write("<B>File XML Added Successfully</B>");
            iD++;

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Project-ControlPanel.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            using(var doc = new DContext())
            {
                var d = new Doctors() { Age = Convert.ToInt32(TextBox4.Text), ID = this.iD, FirstN = TextBox2.Text, LastN = TextBox3.Text };
                doc.D.Add(d);
                doc.SaveChanges();
                Response.Write("<B>File DataBase By EntityFramWork Added Successfully</B>");
            }
        }
    }
}