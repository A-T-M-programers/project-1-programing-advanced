using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Xml;

namespace project_1_programing_advanced
{
    public partial class Project_ControlPanel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        int age, iD;
        string firstN, lastN;
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("project-Update.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("project-ADD.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("project-delete.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string str = "Data Source=DESKTOP-OS4TRPA;Initial Catalog=Doctors;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(str))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("project.xml"));
                DataTable dtDept = ds.Tables["Doctor"];
                con.Open();
                using (SqlBulkCopy bc = new SqlBulkCopy(con))
                {
                    bc.DestinationTableName = "Doctor";
                    bc.ColumnMappings.Add("ID", "ID");
                    bc.ColumnMappings.Add("FirstN", "FirstN");
                    bc.ColumnMappings.Add("LastN", "LastN");
                    bc.ColumnMappings.Add("Age", "Age");
                    bc.WriteToServer(dtDept);
                }
                con.Close();
                XDocument xDocument = XDocument.Load(Server.MapPath("project.xml"));
                xDocument.Root.Elements().Remove();
                xDocument.Save(Server.MapPath("project.xml"));
                Response.Write("<B>File XML Added Successfully</B>");
            }

        }

        protected void Button5_Click1(object sender, EventArgs e)
        {
            using (XmlReader reader = XmlReader.Create(Server.MapPath("project.xml")))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        using (var Doc = new DContext())
                        {
                            //return only when you have START tag  
                            switch (reader.Name.ToString())
                            {
                                case "ID":
                                    iD = Convert.ToInt32(reader.ReadString());
                                    continue;
                                case "FirstN":
                                    firstN = reader.ReadString();
                                    continue;
                                case "LastN":
                                    lastN = reader.ReadString();
                                    continue;
                                case "Age":
                                    age = Convert.ToInt32(reader.ReadString());
                                    var d = new Doctors() { Age = this.age, ID = this.iD, FirstN = this.firstN, LastN = this.lastN };
                                    Doc.D.Add(d);
                                    Doc.SaveChanges();
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                    Response.Write("<B>File Entity FramWork Added Successfully</B>");
                }
            }
        }
    }
}