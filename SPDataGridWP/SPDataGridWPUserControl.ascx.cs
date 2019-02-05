using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SPDataGridWP.VisualWebPart
{
    public partial class SPDataGridWPUserControl : UserControl
    {
        private SqlConnection con;
        private const string databaseServerName = "gww09072appg078";
        private const string databaseName = "HermesCMS";
        private const string tableName = "Risk";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                StringBuilder connectionString = new StringBuilder("Data Source=");
                connectionString.Append(databaseServerName);
                connectionString.Append(";Initial Catalog=");
                connectionString.Append(databaseName);
                //connectionString.Append(";Integrated Security=SSPI;");
                connectionString.Append("User Id=CLMUser;");
                connectionString.Append("Password=CLM@123;");
                connectionString.Append("Connect Timeout=0");

                con = new SqlConnection(connectionString.ToString());
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        protected void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gridView.EditIndex = e.NewEditIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        protected void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow ro = (GridViewRow)gridView.Rows[e.RowIndex];
                Label riskId = (Label)ro.FindControl("lblRiskId");
                TextBox riskTitle = (TextBox)ro.FindControl("txtRiskTitle");
                TextBox riskDescription = (TextBox)ro.FindControl("txtRiskDescription");
                TextBox riskEBITDA = (TextBox)ro.FindControl("txtEBITDA");
                UpdateGrid(Convert.ToInt32(riskId.Text), riskTitle.Text, riskDescription.Text, Convert.ToInt32(riskEBITDA.Text));
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        protected void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                gridView.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gridView.PageIndex = e.NewPageIndex;
                BindGrid();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        protected void imgDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int i = 0;
                ImageButton imgdetails = sender as ImageButton;
                GridViewRow gvrow = (GridViewRow)imgdetails.NamingContainer;
                Label riskId = (Label)gvrow.FindControl("lblRiskId");
                SqlCommand cmd = new SqlCommand("delete from " + tableName + " where RiskID=@RiskID", con);
                cmd.Parameters.AddWithValue("@RiskID", riskId.Text);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
                BindGrid();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        public void BindGrid()
        {
            try
            {
                gridView.PagerTemplate = null;
                con.Open();
                SqlCommand cmd = new SqlCommand(string.Format("select {0}.RiskID, {1}.RiskTitle, {2}.RiskDescription, {3}.LifetimeEBITDAValue from {4}", tableName, tableName, tableName, tableName, tableName), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                gridView.DataSource = ds;
                gridView.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }

        private void UpdateGrid(int RiskID, string RiskTitle, string RiskDescription, int RiskEBITDA)
        {
            try
            {
                string query = "UPDATE " + tableName + " SET RiskTitle = @RiskTitle, RiskDescription = @RiskDescription, LifetimeEBITDAValue = @LifetimeEBITDAValue  WHERE RiskID = @RiskID";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.Add("@RiskID", SqlDbType.Int).Value = RiskID;
                com.Parameters.Add("@RiskTitle", SqlDbType.VarChar).Value = RiskTitle;
                com.Parameters.Add("@RiskDescription", SqlDbType.VarChar).Value = RiskDescription;
                com.Parameters.Add("@LifetimeEBITDAValue", SqlDbType.Int).Value = RiskEBITDA;
                con.Open();
                com.ExecuteNonQuery();
                con.Close();

                gridView.EditIndex = -1;
                BindGrid();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message + "\n" + ex.StackTrace;
            }
        }
    }

}
