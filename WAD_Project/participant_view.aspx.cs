using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class participant_view : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlConnection conn;
    SqlDataReader Read;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Test
        /*String test = "";

        for (int i = 0; i < Session.Count; i++)
        {
            var crntSession = Session.Keys[i];
            test = test + string.Concat(crntSession, "=", Session[crntSession]) + "<br />";
        }*/
        String id="",ic="",name="",email="", phoneno="", paid="",price="",shirt="",size="",food="",discount="",medal="",catg="",cap="",bag="";
        //lblTest.Text = Session["IC"].ToString();

        conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dbtest;Integrated Security=True");
        cmd = new SqlCommand("SELECT * FROM [PARTICIPANT] WHERE ([part_icno] = '" + Session["IC"].ToString() + "')", conn);
        conn.Open();
        Read = cmd.ExecuteReader();
        while (Read.Read())
        {
            id = Read["part_ID"].ToString();
            ic = Read["part_icno"].ToString();
            name = Read["part_name"].ToString();
            email = Read["part_email"].ToString();
            phoneno = Read["part_phoneno"].ToString();
            paid = Read["part_paid"].ToString();
        }
        conn.Close();

         lblIC.Text = ic; 
         lblName.Text = name; 
         lblEmail.Text = email; 
         lblNumber.Text = phoneno;

         cmd = new SqlCommand("SELECT * FROM [PACKAGE] WHERE ([part_ID] = '" + id + "')", conn);
         conn.Open();
         Read = cmd.ExecuteReader();
         while (Read.Read())
         {
             price = Read["pack_price"].ToString();
             shirt = Read["pack_shirt"].ToString();
             size = Read["shirt_size"].ToString();
             food = Read["pack_food"].ToString();
             discount = Read["pack_discount"].ToString();
             medal = Read["pack_medal"].ToString();
             catg = Read["pack_category"].ToString();
             cap = Read["pack_cap"].ToString();
             bag = Read["pack_bag"].ToString();

         }
         conn.Close();

        if (catg == "3"  || catg == "5"){
        lblCatg.Text = "Kid Superhero</br>Age 5 - 12 years old";
        lblCatDist.Text = catg + "KM";
        lblCatPrice.Text = "RM25";
        }
        else 
        {
            lblCatg.Text = "Adult Superhero</br>Age 13 - 45 years old";
            lblCatDist.Text = catg + "KM";
            lblCatPrice.Text = "RM40";
        }

        if (medal == "True")
            medal = "Medal <br/>";
        else
            medal = "";
        if (shirt == "True")
            shirt = "Shirt (" + size + ") <br/>";
        else
            shirt = "";
        if (food == "1")
            food = "Breakfast <br/>";
        else
            food = "";
        if (bag == "True")
            bag = "Bag <br/>";
        else
            bag = "";

        lblAddons.Text = medal + shirt + food + bag;

        lblDiscount.Text = "RM" + discount;
        lblTotal.Text = "RM" + price;

        if (paid == "True")
        {
            btnPay.Visible = true;
            btnPay.Visible = false;
            lblTest.Text = "You have paid";
        }
        else
            lblTest.Text = " ";

      }

    protected void lblDone_Click(object sender, EventArgs e)
    {
        Response.Redirect("landing_page.aspx");
    }
}