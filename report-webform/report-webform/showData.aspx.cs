using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace report_webform
{
    public partial class showData : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            DrawTable();
        }

        public List<MyInvoice> getInvoice()
        {
            List<MyInvoice> list = new List<MyInvoice>();
            string connectionString = "Data Source=hq03; Initial Catalog=EmplasPortal; User ID='dbdev'; Password='Hqgaming123'";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = "SELECT po_no ,invoice_id,invoice_po_item_id,invoice_vat_amount AS invoice_total, invoice_good_vat_amount + invoice_currency_vat_amount AS line_total FROM  ft_wd_po_invoices  INNER JOIN  ft_wd_po_items  on ft_wd_po_invoices.invoice_po_item_id=ft_wd_po_items.po_id INNER JOIN  ft_wd_po on ft_wd_po_items.po_header_id = ft_wd_po.po_id "
            + "WHERE(invoice_good_vat_amount IS NOT NULL) AND(invoice_currency_vat_amount IS NOT NULL) AND(invoice_good_vat_amount <> 0) AND(invoice_currency_vat_amount <> 0)AND invoice_vat_amount != invoice_good_vat_amount + invoice_currency_vat_amount";     
            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MyInvoice invoice = new MyInvoice();
                    invoice.poID = Convert.ToInt32(reader["invoice_po_item_id"]);
                    invoice.itemID = Convert.ToInt32(reader["invoice_id"]);
                    invoice.poNo = reader["po_no"].ToString();
                    invoice.InvoiceTotal = Convert.ToDecimal(reader["invoice_total"]);
                    invoice.LineTotal = Convert.ToDecimal(reader["line_total"]);
                    invoice.Variance =invoice.LineTotal-invoice.InvoiceTotal;
                    list.Add(invoice);
                }
            }
            return list;
        }

        private void DrawTable()
        {
            List<MyInvoice> list = getInvoice();

            foreach (MyInvoice o in list)
            {
                HtmlGenericControl tr = new HtmlGenericControl("tr");               
                tr.InnerHtml += "<td class=\"col-sm-4\"><a href=\"/Internal/Purchasing/PurchaseOrder.aspx?action=view&po_no="+o.poNo+"\">" + o.poNo+ "</a></td>";
                tr.InnerHtml += "<td class=\"col-sm-2  text-right\">" + o.InvoiceTotal + "</td>";        
                tr.InnerHtml += "<td class=\"col-sm-2  text-right\">" + o.LineTotal + "</td>";
                if (o.Variance < 0)
                {
                    tr.InnerHtml += "<td  class=\"col-sm-2  text-right\"><span class=\"text-danger\" >" + o.Variance + "</span></td>";
                }
                else
                {
                    tr.InnerHtml += "<td class=\"col-sm-2  text-right\"><span class=\"text-success\" >" + "+"+o.Variance + "</span></td>";
                }

                tr.InnerHtml += "<td class=\" col-sm-2 text-center\"><a href=\"PurchaseOrderInvoice.aspx?action=view&item_id=" + o.itemID+"&invoice_id="+o.poID+"\">" + "View"+"</a></td>";
                tableResults.Controls.Add(tr);
                
            }

        }

    }
}
    public class MyInvoice
    {
        public String poNo;
        public decimal InvoiceTotal;
        public decimal LineTotal;
        public decimal Variance;
        public int poID;
        public int itemID;         
    }
    
    



    