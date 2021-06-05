using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml.Linq;
using System.Data.Entity;

namespace project_1_programing_advanced
{
    public partial class project_delete : System.Web.UI.Page
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
            using (var doc = new DContext())
            {
                GridView2.DataSource = doc.D.ToList(); 
                GridView2.DataBind();
                Response.Write("<B>File DataBase By EntityFramWork Added Successfully</B>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Valdition valdition = new Valdition();
            if (valdition.ToEmpty(TextBox1))
            {
                XDocument xDocument = XDocument.Load(Server.MapPath("project.xml"));
                xDocument.Root.Elements().Where(x => x.Element("ID").Value == TextBox1.Text).Remove();
                xDocument.Save(Server.MapPath("project.xml"));
                Response.Write("<B>File XML Added Successfully</B>");
            }
            else
            {
                Response.Write("<script> alert('not empty text') </script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            XDocument xDocument = XDocument.Load(Server.MapPath("project.xml"));
            xDocument.Root.Elements().Remove();
            xDocument.Save(Server.MapPath("project.xml"));
            Response.Write("<B>File XML Added Successfully</B>");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Project-ControlPanel.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TextBox2.Text);
            using (var doc = new DContext())
            {
                Doctors d = new Doctors();
                var f = from s in doc.D
                    where s.ID == id
                    select s;
                d = f.FirstOrDefault();
                doc.D.Remove(d);
                doc.SaveChanges();
                Response.Write("<B>File DataBase By EntityFramWork Added Successfully</B>");
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            using (var context = new DContext())
            {
                var tableNames = context.Database.SqlQuery<string>("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME NOT LIKE '%Migration%'").ToList();
                foreach (var tableName in tableNames)
                {
                    context.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", tableName));
                }

                context.SaveChanges();
            }
        }
    }
}