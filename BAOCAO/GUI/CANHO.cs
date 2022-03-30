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
    public partial class CANHO : Form
    {
        ConnectToDB connDB = new ConnectToDB();
        public CANHO()
        {
            InitializeComponent();
            dgvCH.DataSource = Load_form().Tables["CANHO"];
            CBMaLoaiCH.DataSource = Load_CBMALOAICH().Tables["CBMALOAICH"];
            CBMaLoaiCH.DisplayMember = "MALOAICANHO";
            CBMaLoaiCH.ValueMember = "MALOAICANHO";

            CBLoaiCH.DataSource = Load_CBLOAICH().Tables["CBLOAICH"];
            CBLoaiCH.DisplayMember = "TENLOAICANHO";
            CBLoaiCH.ValueMember = "TENLOAICANHO";

            CBmaCH.DataSource = Load_MACH().Tables["LOADMACH"];
            CBmaCH.DisplayMember = "MACANHO";
            CBmaCH.ValueMember = "MACANHO";

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        public DataSet Load_form()
        {
            string sql = "Select * from CANHO";
            DataSet dataSet = connDB.get_data(sql, "CANHO", null);
            return dataSet;
        }
        public DataSet Load_MACH()
        {
            string sql = "Select * from CANHO";
            DataSet dataSet = connDB.get_data(sql, "LOADMACH", null);
            return dataSet;
        }
        public DataSet Load_CBMALOAICH()
        {
            string sql = "Select * from LOAICANHO";
            DataSet dataSet = connDB.get_data(sql, "CBMALOAICH", null);
            return dataSet;
        }
        public DataSet Load_CBLOAICH()
        {
            string sql = "Select * from LOAICANHO WHERE MALOAICANHO = @MALOAI";
            string maloaich = CBMaLoaiCH.SelectedValue.ToString();
            List<SqlParameter> parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@MALOAI", maloaich));
            DataSet dataSet = connDB.get_data(sql, "CBLOAICH", parameter);
            return dataSet;
        }
        public void ClearText()
        {
            txtmach.Text = "";
            txtGia.Text = "";
            txtGhichu.Text = "";
            CBMaKhu.SelectedIndex = 0;
            CBLoaiCH.SelectedIndex = 0;
            CBMaLoaiCH.SelectedIndex = 0;
            CbTrangThai.SelectedIndex = 0;
        }
        public void Refresh()
        {
            dgvCH.DataSource = Load_form().Tables["CANHO"];
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO CANHO VALUES(@MACH,@MAKHU,@LOAICH,@MALOAICH,@GHICHU,@GIA,@TRANGTHAI)";
            string mach = txtmach.Text;
            string makhu = CBMaKhu.SelectedItem.ToString();
            string maloaich = CBMaLoaiCH.SelectedValue.ToString();
            string loaich = CBLoaiCH.SelectedValue.ToString();
            string ghichu = txtGhichu.Text;
            float gia = float.Parse(txtGia.Text);
            string trangthai = CbTrangThai.SelectedItem.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MACH", mach));
            parameters.Add(new SqlParameter("@MAKHU", makhu));
            parameters.Add(new SqlParameter("@LOAICH", loaich));
            parameters.Add(new SqlParameter("@MALOAICH", maloaich));
            parameters.Add(new SqlParameter("@GHICHU", ghichu));
            parameters.Add(new SqlParameter("@GIA", gia));
            parameters.Add(new SqlParameter("@TRANGTHAI", trangthai));
            if (txtmach.Text == "")
                return;
            else
            {
                connDB.Excute(sql, parameters);
                MessageBox.Show("Thêm mới thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Refresh();
                ClearText();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }

        }

        private void CBMaLoaiCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBLoaiCH.DataSource = Load_CBLOAICH().Tables["CBLOAICH"];
            CBLoaiCH.DisplayMember = "TENLOAICANHO";
            CBLoaiCH.ValueMember = "TENLOAICANHO";
        }

        private void txtmach_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtmach.Text))
            {
                btnThem.Enabled = false;
            }
            else btnThem.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "DELETE CANHO WHERE MACANHO = @MACH";
            string mach = txtmach.Text;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MACH", mach));
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
            {
                connDB.Excute(sql, parameters);
                Refresh();
                ClearText();
            }
        }

        private void dgvCH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if(index >=0 && index < dgvCH.Rows.Count - 1)
            {
                txtmach.Text = dgvCH.Rows[index].Cells[0].Value.ToString();
                CBMaKhu.SelectedItem = dgvCH.Rows[index].Cells[1].Value.ToString();
                CBLoaiCH.SelectedValue = dgvCH.Rows[index].Cells[2].Value.ToString();
                CBMaLoaiCH.SelectedValue = dgvCH.Rows[index].Cells[3].Value.ToString();
                txtGhichu.Text = dgvCH.Rows[index].Cells[4].Value.ToString();
                txtGia.Text = dgvCH.Rows[index].Cells[5].Value.ToString();
                CbTrangThai.SelectedItem = dgvCH.Rows[index].Cells[6].Value.ToString();

                /**/
                CBLoaiCH.DataSource = Load_CBLOAICH().Tables["CBLOAICH"];
                CBLoaiCH.DisplayMember = "TENLOAICANHO";
                CBLoaiCH.ValueMember = "TENLOAICANHO";

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void CBMaLoaiCH_SelectedValueChanged(object sender, EventArgs e)
        {
            CBLoaiCH.DataSource = Load_CBLOAICH().Tables["CBLOAICH"];
            CBLoaiCH.DisplayMember = "TENLOAICANHO";
            CBLoaiCH.ValueMember = "MALOAICANHO";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoát ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
                Application.Exit();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql = "select * from CANHO where MACANHO = @MACH";
            string mach = CBmaCH.SelectedValue.ToString();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MACH", mach));
            DataSet dataSet = connDB.get_data(sql, "MACH", parameters);
            dgvCH.DataSource = dataSet.Tables["MACH"];
       
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
            string sql = "UPDATE CANHO SET MAKHU = @MAKHU,LOAICANHO = @LOAICH,MALOAICANHO = @MALOAICH,GHICHU = @GHICHU,GIA = @GIA,TRANGTHAI = @TRANGTHAI WHERE MACANHO = @MACH";
            string mach = txtmach.Text;
            string makhu = CBMaKhu.SelectedItem.ToString();
            string maloaich = CBMaLoaiCH.SelectedValue.ToString();
            string loaich = CBLoaiCH.SelectedValue.ToString();
            string ghichu = txtGhichu.Text;
            float gia = float.Parse(txtGia.Text);
            string trangthai = CbTrangThai.SelectedItem.ToString();

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MACH", mach));
            parameters.Add(new SqlParameter("@MAKHU", makhu));
            parameters.Add(new SqlParameter("@LOAICH", loaich));
            parameters.Add(new SqlParameter("@MALOAICH", maloaich));
            parameters.Add(new SqlParameter("@GHICHU", ghichu));
            parameters.Add(new SqlParameter("@GIA", gia));
            parameters.Add(new SqlParameter("@TRANGTHAI", trangthai));

            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn sửa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
            {
                connDB.Excute(sql, parameters);
                Refresh();
                ClearText();
            }
        }
    }
}
