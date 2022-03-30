using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BAOCAO.GUI
{
    public partial class HGD : Form
    {
        ConnectToDB connDB = new ConnectToDB();
        public HGD()
        {
            InitializeComponent();
            dgvHGD.DataSource = Load_form().Tables["HGD"];
            dgvHGD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            CBMaHGD.DataSource = Load_CB().Tables["CBHGD"];
            CBMaHGD.DisplayMember = "MAHGD";
            CBMaHGD.ValueMember = "MAHGD";
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        public DataSet Load_form()
        {
            string sql = "Select * from HGD ";
            DataSet dataSet = connDB.get_data(sql,"HGD",null);
            return dataSet; 
        }
        public DataSet Load_CB()
        {
            string sql = "Select MAHGD from HGD ";
            DataSet dataSet = connDB.get_data(sql, "CBHGD", null);
            return dataSet;
        }
        public void ClearText()
        {
            txtCMND.Text = "";
            txtMahgd.Text = "";
            txtSLTV.Text = "";
            txtTenCH.Text = "";
        }
        public void Refresh()
        {
            dgvHGD.DataSource = Load_form().Tables["HGD"];
            dgvHGD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO HGD VALUES(@MAHGD,@TENCH,@SOCMND,@SLTV)";
            string mahgd = txtMahgd.Text;
            string tench = txtTenCH.Text;
            string cmnd = txtCMND.Text;
            string sltv = txtSLTV.Text;
            if (mahgd == "")
                return;
            else
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@MAHGD", mahgd));
                parameters.Add(new SqlParameter("@TENCH", tench));
                parameters.Add(new SqlParameter("@SOCMND", cmnd));
                parameters.Add(new SqlParameter("@SLTV", sltv));
                connDB.Excute(sql, parameters);
                /**/
                MessageBox.Show("Thêm mới thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Refresh();
                ClearText();
                txtMahgd.Focus();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string mahgd = txtMahgd.Text;
            string tench = txtTenCH.Text;
            string cmnd = txtCMND.Text;
            string sltv = txtSLTV.Text;
            string sql = "UPDATE HGD SET TENCH  = @TENCH, SOCMND = @SOCMND, SLTV = @SLTV WHERE MAHGD = @MAHGD";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MAHGD", mahgd));
            parameters.Add(new SqlParameter("@TENCH", tench));
            parameters.Add(new SqlParameter("@SOCMND", cmnd));
            parameters.Add(new SqlParameter("@SLTV", sltv));

            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn sửa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(rs == DialogResult.Yes)
            {
                connDB.Excute(sql, parameters);
                Refresh();
                ClearText();
                txtMahgd.Focus();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "DELETE HGD WHERE MAHGD = @MAHGD";
            string mahgd = txtMahgd.Text;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MAHGD", mahgd));


            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
            {
                connDB.Excute(sql, parameters);
                Refresh();
                ClearText();
                txtMahgd.Focus();
            }
        }

        private void dgvHGD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if(index >= 0 && index < dgvHGD.Rows.Count - 1)
            {
                txtMahgd.Text = dgvHGD.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenCH.Text = dgvHGD.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCMND.Text = dgvHGD.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSLTV.Text = dgvHGD.Rows[e.RowIndex].Cells[3].Value.ToString();
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql = "select * from HGD where MAHGD = @MAHGD";
            string mahgd = CBMaHGD.SelectedValue.ToString();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MAHGD", mahgd));
            DataSet dataSet = connDB.get_data(sql, "HGD", parameters);
            dgvHGD.DataSource = dataSet.Tables["HGD"];
            dgvHGD.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoát ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
                Application.Exit();
        }

        private void txtMahgd_TextChanged(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMahgd.Text))
            {
                btnThem.Enabled = false;
            }
            else btnThem.Enabled = true;
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            Refresh();
            ClearText();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
    }
}
