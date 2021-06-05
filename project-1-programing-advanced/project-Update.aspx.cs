using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;

namespace project_1_programing_advanced
{
    public partial class project_Update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(Server.MapPath("project.xml"));
            if (dataSet.Tables.Count == 0)
            {
                return;
            }
            GridView1.DataSource = dataSet;
            GridView1.DataBind();
            Label1.Visible = Label2.Visible = Label3.Visible = Label4.Visible = Label6.Visible = TextBox2.Visible = TextBox3.Visible = TextBox4.Visible =Button1.Visible
                =Label8.Visible=Label9.Visible =Label10.Visible=Label11.Visible=Label12.Visible=TextBox6.Visible=TextBox7.Visible=TextBox8.Visible=Button6.Visible= false;
            using (var doc = new DContext())
            {
                GridView2.DataSource = doc.D.ToList();
                GridView2.DataBind();
                Response.Write("<B>File DataBase By EntityFramWork Added Successfully</B>");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            XDocument xmlDocument = XDocument.Load(Server.MapPath("project.xml"));
            xmlDocument.Element("Doctors")
                       .Elements("Doctor")
                       .Where(x => x.Element("ID").Value == Label6.Text).FirstOrDefault()
                       .SetElementValue("FirstN", TextBox2.Text)
                       ;
            TextBox2.Text = "";
            xmlDocument.Element("Doctors")
                       .Elements("Doctor")
                       .Where(x => x.Element("ID").Value == Label6.Text).FirstOrDefault()
                       .SetElementValue("LastN", TextBox3.Text)
                       ;
            TextBox3.Text = "";
            xmlDocument.Element("Doctors")
                       .Elements("Doctor")
                       .Where(x => x.Element("ID").Value == Label6.Text).FirstOrDefault()
                       .SetElementValue("Age",TextBox4.Text)
                       ;
            TextBox4.Text = "";

            xmlDocument.Save(Server.MapPath("project.xml"));
            Label6.Text = "";
            Response.Write("<B>File XML Added Successfully</B>");
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(Server.MapPath("project.xml"));
            GridView1.DataSource = dataSet;
            GridView1.DataBind();
            Label1.Visible = Label2.Visible = Label3.Visible = Label4.Visible = Label6.Visible = TextBox2.Visible = TextBox3.Visible = TextBox4.Visible = Button1.Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            XElement xElement = XElement.Load(Server.MapPath("project.xml"));
            var ID = from id in xElement.Elements("Doctor")
                     where (string)id.Element("ID") == TextBox5.Text
                     select id;
            foreach(XElement EL in ID)
            {
                Label6.Text = EL.Element("ID").Value;
                TextBox2.Text = EL.Element("FirstN").Value;
                TextBox3.Text = EL.Element("LastN").Value;
                TextBox4.Text = EL.Element("Age").Value;
            }
            TextBox5.Text = "";
            Label1.Visible = Label2.Visible = Label3.Visible = Label4.Visible = Label6.Visible = TextBox2.Visible = TextBox3.Visible = TextBox4.Visible=Button1.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Project-ControlPanel.aspx");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Label9.Text);
            using (var doc = new DContext())
            {
                var d = doc.D.SingleOrDefault(b => b.ID == id);
                if (d!=null)
                {
                    d.FirstN = TextBox6.Text;
                    d.LastN = TextBox7.Text;
                    d.Age = Convert.ToInt32(TextBox8.Text);
                    doc.SaveChanges();
                }
                Response.Write("<B>File DataBase By EntityFramWork Added Successfully</B>");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBox1.Text);
            using (var doc = new DContext())
            {
                Doctors d = new Doctors();
                var f = from s in doc.D
                        where s.ID == id
                        select s;
                d = f.FirstOrDefault();
                Label9.Text = d.ID.ToString();
                TextBox6.Text = d.FirstN;
                TextBox7.Text = d.LastN;
                TextBox8.Text = d.Age.ToString();
                Label8.Visible = Label9.Visible = Label10.Visible = Label11.Visible = Label12.Visible = TextBox6.Visible = TextBox7.Visible = TextBox8.Visible = Button6.Visible = true;
            }
        }
    }
}