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
    public partial class HOPDONG : Form
    {
        ConnectToDB connDB = new ConnectToDB();
        public HOPDONG()
        {
            InitializeComponent();
            dgvHD.DataSource = Load_form().Tables["HOPDONG"];
            CBMAHD.DataSource = Load_form().Tables["HOPDONG"];
            CBMAHD.DisplayMember = "MAHD";
            CBMAHD.ValueMember = "MAHD";
            /**/
            CBMaCH.DataSource = Load_CBCanHO().Tables["CBCANHO"];
            CBMaCH.DisplayMember = "MACANHO";
            CBMaCH.ValueMember = "MACANHO";
            /**/
            CBMaNV.DataSource = Load_CBNhanVien().Tables["CBNHANVIEN"];
            CBMaNV.DisplayMember = "MANV";
            CBMaNV.ValueMember = "MANV";
            /**/
            CBMaHGD.DataSource = Load_CBHGD().Tables["CBHGD"];
            CBMaHGD.DisplayMember = "MAHGD";
            CBMaHGD.ValueMember = "MAHGD";
            /**/
            CBloaiHD.SelectedIndex = 0;
            /**/
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        public HOPDONG(string mahgd)
        {
            InitializeComponent();
            dgvHD.DataSource = Load_form_Condition(mahgd).Tables["HOPDONGCONDITION"];
            CBMAHD.DataSource = Load_form().Tables["HOPDONG"];
            CBMAHD.DisplayMember = "MAHD";
            CBMAHD.ValueMember = "MAHD";
            /**/
            CBMaCH.DataSource = Load_CBCanHO().Tables["CBCANHO"];
            CBMaCH.DisplayMember = "MACANHO";
            CBMaCH.ValueMember = "MACANHO";
            /**/
            CBMaNV.DataSource = Load_CBNhanVien().Tables["CBNHANVIEN"];
            CBMaNV.DisplayMember = "MANV";
            CBMaNV.ValueMember = "MANV";
            /**/
            CBMaHGD.DataSource = Load_CBHGD().Tables["CBHGD"];
            CBMaHGD.DisplayMember = "MAHGD";
            CBMaHGD.ValueMember = "MAHGD";
            /**/
            CBloaiHD.SelectedIndex = 0;
            /**/
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        public DataSet Load_form()
        {
            string sql = "Select * from HOPDONG";
            DataSet dataSet = connDB.get_data(sql, "HOPDONG", null);
            return dataSet;
        }
        public DataSet Load_form_Condition(string mahgd)
        {
            string sql = "Select * from HOPDONG WHERE MAHGD = '"+mahgd+"'";
            DataSet dataSet = connDB.get_data(sql, "HOPDONGCONDITION", null);
            return dataSet;
        }
        public DataSet Load_CBCanHO()
        {
            string sql = "Select * from CANHO";
            DataSet dataSet = connDB.get_data(sql, "CBCANHO", null);
            return dataSet;
        }
        public DataSet Load_CBNhanVien()
        {
            string sql = "Select * from NHANVIEN";
            DataSet dataSet = connDB.get_data(sql, "CBNHANVIEN", null);
            return dataSet;
        }
        public DataSet Load_CBHGD()
        {
            string sql = "Select * from HGD";
            DataSet dataSet = connDB.get_data(sql, "CBHGD", null);
            return dataSet;
        }
        public void ClearText()
        {
            txtMahd.Text = "";
            txtND.Text = "";
            txtTenCH.Text = "";
            txtTenHD.Text = "";
            txtTenNV.Text = "";
            txtTongtien.Text = "";
            DateNK.Text = "01-01-2021";
            DateTH.Text = "01-01-2021";
            CBloaiHD.SelectedIndex = 0;
            CBMaNV.SelectedIndex = 0;
            CBMaHGD.SelectedIndex = 0;
            CBMaCH.SelectedIndex = 0;
            txtMahd.Focus();
        }
        public void Refresh()
        {
            dgvHD.DataSource = Load_form().Tables["HOPDONG"];
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string mahd = txtMahd.Text;
            string tenhd = txtTenHD.Text;
            string loaihd = CBloaiHD.SelectedItem.ToString();
            string manv = CBMaNV.SelectedValue.ToString();
            string tennv = txtTenNV.Text;
            string mach = CBMaCH.SelectedValue.ToString();
            string mahgd = CBMaHGD.SelectedValue.ToString();
            string tenchuho = txtTenCH.Text;
            string noidung = txtND.Text;
            float tongtien = float.Parse(txtTongtien.Text);
            string sql = "INSERT INTO HOPDONG VALUES(@MAHD,@TENHD,@LOAIHD,@MANV,@TENNV," +
                "@MACH,@MAHGD,@TENCH,@NGAYKI,@THOIHAN,@NOIDUNG,@TONGTIEN)";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MAHD", mahd));
            parameters.Add(new SqlParameter("@TENHD", tenhd));
            parameters.Add(new SqlParameter("@LOAIHD", loaihd));
            parameters.Add(new SqlParameter("@MANV", manv));
            parameters.Add(new SqlParameter("@TENNV", tennv));
            parameters.Add(new SqlParameter("@MACH", mach));
            parameters.Add(new SqlParameter("@MAHGD", mahgd));
            parameters.Add(new SqlParameter("@TENCH", tenchuho));
            parameters.Add(new SqlParameter("@THOIHAN", DateTH.Value.Date));
            parameters.Add(new SqlParameter("@NGAYKI", DateNK.Value.Date));
            parameters.Add(new SqlParameter("@NOIDUNG", noidung));
            parameters.Add(new SqlParameter("@TONGTIEN", tongtien));

            if (txtMahd.Text == "")
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            string mahd = txtMahd.Text;
            string tenhd = txtTenHD.Text;
            string loaihd = CBloaiHD.SelectedItem.ToString();
            string manv = CBMaNV.SelectedValue.ToString();
            string tennv = txtTenNV.Text;
            string mach = CBMaCH.SelectedValue.ToString();
            string mahgd = CBMaHGD.SelectedValue.ToString();
            string tenchuho = txtTenCH.Text;
            string noidung = txtND.Text;
            float tongtien = float.Parse(txtTongtien.Text);
            string sql = "UPDATE HOPDONG SET TENHD = @TENHD,LOAIHD = @LOAIHD,MANV = @MANV,TENNV = @TENNV," +
                "MACH = @MACH,MAHGD = @MAHGD,TENCHUHO = @TENCH,THOIHAN = @THOIHAN," +
                "NGAYKIHD = @NGAYKI,NOIDUNG = @NOIDUNG,TONGTIEN = @TONGTIEN WHERE MAHD = @MAHD";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MAHD", mahd));
            parameters.Add(new SqlParameter("@TENHD", tenhd));
            parameters.Add(new SqlParameter("@LOAIHD", loaihd));
            parameters.Add(new SqlParameter("@MANV", manv));
            parameters.Add(new SqlParameter("@TENNV", tennv));
            parameters.Add(new SqlParameter("@MACH", mach));
            parameters.Add(new SqlParameter("@MAHGD", mahgd));
            parameters.Add(new SqlParameter("@TENCH", tenchuho));
            parameters.Add(new SqlParameter("@THOIHAN", DateTH.Value.Date));
            parameters.Add(new SqlParameter("@NGAYKI", DateNK.Value.Date));
            parameters.Add(new SqlParameter("@NOIDUNG", noidung));
            parameters.Add(new SqlParameter("@TONGTIEN", tongtien));

            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn sửa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
            {
                connDB.Excute(sql, parameters);
                Refresh();
                ClearText();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "DELETE HOPDONG WHERE MAHD = '"+txtMahd.Text+"'";
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn xóa ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
            {
                connDB.Excute(sql, null);
                Refresh();
                ClearText();
                CBMaNV.ValueMember = "MANV";
                txtTenNV.Text = Load_TextNV().Tables["TENNV"].Rows[0].ItemArray.GetValue(0).ToString();
                CBMaHGD.ValueMember = "MAHGD";
                txtTenCH.Text = Load_TextTENCHUHO().Tables["TENCH"].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            Refresh();
            ClearText();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            CBMaNV.ValueMember = "MANV";
            txtTenNV.Text = Load_TextNV().Tables["TENNV"].Rows[0].ItemArray.GetValue(0).ToString();
            CBMaHGD.ValueMember = "MAHGD";
            txtTenCH.Text = Load_TextTENCHUHO().Tables["TENCH"].Rows[0].ItemArray.GetValue(0).ToString();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql = "Select * from HOPDONG WHERE MAHD = @MAHD";
            string mahd = CBMAHD.SelectedValue.ToString();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MAHD", mahd));
            DataSet dataSet = connDB.get_data(sql, "TKHD", parameters);
            dgvHD.DataSource = dataSet.Tables["TKHD"];
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn thoát ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rs == DialogResult.Yes)
                Application.Exit();
        }
        public DataSet Load_TextNV()
        {
            string sql = "Select TENNV from NHANVIEN WHERE MANV = @MANV";
            string manv = CBMaNV.SelectedValue.ToString();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MANV", manv));
            DataSet dataSet = connDB.get_data(sql, "TENNV", parameters);
            return dataSet;
        }
        public DataSet Load_TextTENCHUHO()
        {
            string sql = "Select TENCH from HGD WHERE MAHGD = @MAHGD";
            string mahgd = CBMaHGD.SelectedValue.ToString();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@MAHGD", mahgd));
            DataSet dataSet = connDB.get_data(sql, "TENCH", parameters);
            return dataSet;
        }
        private void CBMaNV_SelectedValueChanged(object sender, EventArgs e)
        {
            CBMaNV.ValueMember = "MANV";
            txtTenNV.Text = Load_TextNV().Tables["TENNV"].Rows[0].ItemArray.GetValue(0).ToString();
        }

        private void CBMaHGD_SelectedValueChanged(object sender, EventArgs e)
        {
            CBMaHGD.ValueMember = "MAHGD";
            txtTenCH.Text = Load_TextTENCHUHO().Tables["TENCH"].Rows[0].ItemArray.GetValue(0).ToString();
        }

        private void txtMahd_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMahd.Text))
            {
                btnThem.Enabled = false;
            }
            else btnThem.Enabled = true;
        }

        private void dgvHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if(index >=0 && index < dgvHD.Rows.Count - 1)
            {
                txtMahd.Text = dgvHD.Rows[index].Cells[0].Value.ToString();
                txtTenHD.Text = dgvHD.Rows[index].Cells[1].Value.ToString();
                CBloaiHD.SelectedValue = dgvHD.Rows[index].Cells[2].Value.ToString();
                CBMaNV.SelectedValue = dgvHD.Rows[index].Cells[3].Value.ToString();
                txtTenNV.Text = dgvHD.Rows[index].Cells[4].Value.ToString();
                CBMaCH.SelectedValue = dgvHD.Rows[index].Cells[5].Value.ToString();
                CBMaHGD.SelectedValue = dgvHD.Rows[index].Cells[6].Value.ToString();
                txtTenCH.Text = dgvHD.Rows[index].Cells[7].Value.ToString();
                DateNK.Text = dgvHD.Rows[index].Cells[8].Value.ToString();
                DateTH.Text = dgvHD.Rows[index].Cells[9].Value.ToString();
                txtND.Text = dgvHD.Rows[index].Cells[10].Value.ToString();
                txtTongtien.Text = dgvHD.Rows[index].Cells[11].Value.ToString();
                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }
    }
}
