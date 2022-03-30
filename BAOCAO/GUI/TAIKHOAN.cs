using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace BAOCAO.GUI
{
    public partial class TAIKHOAN : Form
    {
        ConnectToDB ConnDB = new ConnectToDB();
        public TAIKHOAN()
        {
            InitializeComponent();
            dgvTK.DataSource = Load_form().Tables["TAIKHOAN"];
            CBQuyen.SelectedIndex = 0;

            /**/
            CBID.DataSource = Load_CBID().Tables["LOADID"];
            CBID.DisplayMember = "ID";
            CBID.ValueMember = "ID";
            /**/
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

        }
        public DataSet Load_form()
        {
            string sql = "select * from TAIKHOAN";
            DataSet dataSet = ConnDB.get_data(sql,"TAIKHOAN",null);
            return dataSet;
        }
        public DataSet Load_CBID()
        {
            string sql = "select * from TAIKHOAN";
            DataSet dataSet = ConnDB.get_data(sql, "LOADID", null);
            return dataSet;
        }
        public void ClearText()
        {
            txtID.Text = "";
            txtMK.Text = "";
            CBQuyen.SelectedIndex = 0;
            txtTK.Text = "";
            txtID.Focus();
        }
        public void Refresh()
        {
            dgvTK.DataSource = Load_form().Tables["TAIKHOAN"];
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string tk = txtTK.Text;
            string mk = txtMK.Text;
            string id = txtID.Text;
            string quyen = CBQuyen.SelectedItem.ToString();

            string sql = "INSERT INTO TAIKHOAN VALUES(@ID,@TK,@MK,@QUYEN)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TK", tk));
            parameters.Add(new SqlParameter("@MK", mk));
            parameters.Add(new SqlParameter("@QUYEN", quyen));

            if (txtID.Text == "")
                return;
            else
            {
                ConnDB.Excute(sql, parameters);
                MessageBox.Show("Thêm mới thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Refresh();
                ClearText();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtID.Text))
            {
                btnThem.Enabled = false;
            }
            else btnThem.Enabled = true;
        }

        private void dgvTK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if(index >=0 && index < dgvTK.Rows.Count -  1)
            {
                txtID.Text = dgvTK.Rows[index].Cells[0].Value.ToString();
                txtTK.Text = dgvTK.Rows[index].Cells[1].Value.ToString();
                txtMK.Text = dgvTK.Rows[index].Cells[2].Value.ToString();
                CBQuyen.SelectedItem = dgvTK.Rows[index].Cells[3].Value.ToString();

                /**/
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            Refresh();
            ClearText();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string tk = txtTK.Text;
            string mk = txtMK.Text;
            string id = txtID.Text;
            string quyen = CBQuyen.SelectedItem.ToString();

            string sql = "UPDATE TAIKHOAN SET TAIKHOAN = @TK, MATKHAU = @MK, QUYEN = @QUYEN WHERE ID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            parameters.Add(new SqlParameter("@TK", tk));
            parameters.Add(new SqlParameter("@MK", mk));
            parameters.Add(new SqlParameter("@QUYEN", quyen));

            /**/
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn sửa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
            {
                ConnDB.Excute(sql, parameters);
                Refresh();
                ClearText();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string id = txtID.Text;
            string sql = "DELETE TAIKHOAN WHERE ID = @ID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
            {
                ConnDB.Excute(sql, parameters);
                Refresh();
                ClearText();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoát ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
                Application.Exit();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql = "select * from TAIKHOAN where ID = @ID";
            string id = CBID.SelectedValue.ToString();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@ID", id));
            DataSet dataSet = ConnDB.get_data(sql, "TKID", parameters);
            dgvTK.DataSource = dataSet.Tables["TKID"];
        }
    }
}
