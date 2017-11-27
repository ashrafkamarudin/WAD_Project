using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class participant_register : System.Web.UI.Page
{
    SqlCommand cmd;
    SqlConnection conn;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void chckTshirt_CheckedChanged(object sender, EventArgs e)
    {
        lblSize.Visible = false;
        ddlSize.Visible = false;
        if (chckTshirt.Checked)
        {
            lblSize.Visible = true;
            ddlSize.Visible = true;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=dbtest;Integrated Security=True");
        

        // variables
        int total = 0, disc = 0;

        // Personal Info (session test)
        /*Session["name"] = txtName.Text;
        Session["age"] = txtAge.Text;
        Session["ic"] = txtIC.Text;
        Session["contact"] = txtNum.Text;
        Session["email"] = txtEmail.Text;*/

        //insert to database personal info
        cmd = new SqlCommand("INSERT INTO PARTICIPANT(part_name, part_icno, part_phoneno, part_email) VALUES (@v1, @v3, @v4, @v5)", conn);
        cmd.Parameters.AddWithValue("@v1", txtName.Text);
        //cmd.Parameters.AddWithValue("@v2", txtAge.Text);
        cmd.Parameters.AddWithValue("@v3", txtIC.Text);
        cmd.Parameters.AddWithValue("@v4", txtNum.Text);
        cmd.Parameters.AddWithValue("@v5", txtEmail.Text);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        //getting foreign key value for package
        cmd = new SqlCommand("SELECT * FROM  PARTICIPANT WHERE (part_icno = '" + txtIC.Text + "')", conn);
        conn.Open();
        String part_id = "0";
        SqlDataReader read = cmd.ExecuteReader();
        if (read.Read())
        {
            part_id = read.GetValue(0).ToString();
            lblTest.Text = part_id;
        }
        int parto_id = Int32.Parse(part_id);
        conn.Close();


            // Package Info
            String rb = "";

            if (rb3km.Checked)
            {
                rb = rb3km.Text;
                total += 25;
                disc = 5;
            }
            else if (rb5km.Checked)
            {
                rb = rb5km.Text;
                total += 25;
                disc = 5;
            }
            else if (rb10km.Checked)
            {
                rb = rb10km.Text;
                total += 40;
            }
            else if (rb15km.Checked)
            {
                rb = rb15km.Text;
                total += 40;
            }

            /* Session Test
             * Session["distance"] = rb;*/

            // Add-Ons
            String size = "NO"; //addons = "";
            Boolean medal = false, tshirt = false, cap = false, bag = false, bfast = false;

            if (chckMedal.Checked)
            {
                // addons = addons + "medal<br />";
                total += 20;
                medal = true;
            }
            if (chckTshirt.Checked)
            {
                // addons = addons + "T-Shirt<br />";
                total += 20;
                tshirt = true;
                size = ddlSize.SelectedValue;
            }
            if (chckCap.Checked)
            {
                // addons = addons + "Cap<br />";
                total += 10;
                cap = true;
            }
            if (chckBag.Checked)
            {
                // addons = addons + "Bag<br />";
                total += 30;
                bag = true;
            }
            if (chckBfast.Checked)
            {
                //addons = addons + "BreakFast Set<br />";
                total += 10;
                bfast = true;
            }

            /*Session Test
             * Session["addons"] = addons;
             *Session["total"] = total;*/

            //discount
            total = total - packdiscount(total, disc);

            //insert into database package info
            cmd = new SqlCommand("INSERT INTO PACKAGE(pack_price, pack_shirt, shirt_size, pack_food, pack_discount,pack_medal,pack_category,pack_cap,pack_bag,part_ID) VALUES (@v1, @v2, @v3, @v4, @v5,@v6, @v7, @v8, @v9, @v10)", conn);
            cmd.Parameters.AddWithValue("@v1", total);
            cmd.Parameters.AddWithValue("@v2", tshirt);
            cmd.Parameters.AddWithValue("@v3", size);
            cmd.Parameters.AddWithValue("@v4", bfast);
            cmd.Parameters.AddWithValue("@v5", packdiscount(total, disc));
            cmd.Parameters.AddWithValue("@v6", medal);
            cmd.Parameters.AddWithValue("@v7", rb);
            cmd.Parameters.AddWithValue("@v8", cap);
            cmd.Parameters.AddWithValue("@v9", bag);
            cmd.Parameters.AddWithValue("@v10", parto_id);
            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        
        // Test
        /*String test = "";

        for (int i = 0; i < Session.Count; i++)
        {
            var crntSession = Session.Keys[i];
            test = test + string.Concat(crntSession, "=", Session[crntSession]) + "<br />";
        }

        lblTest.Text = test;*/

        Response.Redirect("landing_page.aspx");
    }

    public int packdiscount(int totl, int disc)
    {
        disc = ((totl*disc) / 100);
        return disc;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("landing_page.aspx");
    }
}