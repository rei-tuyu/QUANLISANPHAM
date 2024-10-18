using De02.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De02
{
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Thoát không?", "Xác nhận thoát",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
        private void FillcbbLoai(List<LOAISP> listLoaiSP)
        {
            cbbLoai.DataSource = listLoaiSP;
            cbbLoai.DisplayMember = "TENLOAI";
            cbbLoai.ValueMember = "MALOAI";
        }

        // Bind dữ liệu vào DataGridView
        private void BindGrid(List<SANPHAM> listSanPham)
        {
            dgvSanPham.Rows.Clear();
            foreach (SANPHAM sanPham in listSanPham)
            {
                try
                {
                    int index = dgvSanPham.Rows.Add();
                    dgvSanPham.Rows[index].Cells[0].Value = sanPham.MASP;
                    dgvSanPham.Rows[index].Cells[1].Value = sanPham.TENSP;
                    dgvSanPham.Rows[index].Cells[2].Value = sanPham.NGAYNHAP.ToString();
                    dgvSanPham.Rows[index].Cells[3].Value = sanPham.MALOAI;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // Load dữ liệu khi form mở
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            using (SanPhamContexDB context = new SanPhamContexDB())
            {
                List<LOAISP> listLoaiSP = context.LOAISPs.ToList();
                List<SANPHAM> listSanPham = context.SANPHAMs.ToList();
                FillcbbLoai(listLoaiSP);
                cbbLoai.SelectedIndex = 0;
                BindGrid(listSanPham);
            }
        }

        // Tải lại dữ liệu sau khi thêm mới
        private void LoadData()
        {
            using (SanPhamContexDB context = new SanPhamContexDB())
            {
                List<SANPHAM> listSanPham = context.SANPHAMs.ToList();
                BindGrid(listSanPham);
            }
        }

        // Reset form
        private void ResetForm()
        {
            txtMa.Clear();
            txtTen.Clear();
            cbbLoai.SelectedIndex = 0;
        }

        // Xử lý sự kiện nút thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMa.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã sản phẩm");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên sản phẩm");
                    return;
                }

                SANPHAM newSanPham = new SANPHAM
                {
                    MASP = txtMa.Text.Trim(),
                    TENSP = txtTen.Text.Trim(),
                    NGAYNHAP = dtpNgayNhap.Value,
                    MALOAI = (string)cbbLoai.SelectedValue
                };

                themSanPhamToDatabase(newSanPham);
                LoadData();
                ResetForm();
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message);
            }
        }

        private void themSanPhamToDatabase(SANPHAM newSanPham)
        {
            using (SanPhamContexDB context = new SanPhamContexDB())
            {
                context.SANPHAMs.Add(newSanPham);
                context.SaveChanges(); // Check if there are any inner exceptions
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                string maSP = dgvSanPham.SelectedRows[0].Cells[0].Value.ToString();

                using (SanPhamContexDB context = new SanPhamContexDB())
                {
                    var sanPham = context.SANPHAMs.SingleOrDefault(s => s.MASP == maSP);

                    if (sanPham != null)
                    {
                        // Xóa sinh viên khỏi cơ sở dữ liệu
                        context.SANPHAMs.Remove(sanPham);
                        context.SaveChanges();

                        // Xóa sinh viên khỏi DataGridView
                        dgvSanPham.Rows.RemoveAt(dgvSanPham.SelectedRows[0].Index);

                        MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SanPhamContexDB context = new SanPhamContexDB())
            {
                string MaSP = txtMa.Text.Trim();
                var sanPham = context.SANPHAMs.SingleOrDefault(s => s.MASP == MaSP);

                if (sanPham != null)
                {
                    sanPham.TENSP = txtTen.Text.Trim();
                    sanPham.NGAYNHAP = dtpNgayNhap.Value;
                    sanPham.MALOAI = (string)cbbLoai.SelectedValue;
                    context.SaveChanges();
                    LoadData();
                    ResetForm();
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên với mã số này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string tenSanPham = txtTim.Text.Trim();

            if (string.IsNullOrEmpty(tenSanPham))
            {
                using (SanPhamContexDB context = new SanPhamContexDB())
                {
                    List<SANPHAM> listSanPham = context.SANPHAMs.ToList();
                    BindGrid(listSanPham);
                }
            }
            else
            {
                using (SanPhamContexDB context = new SanPhamContexDB())
                {
                    var results = context.SANPHAMs
                                         .Where(sp => sp.TENSP.Contains(tenSanPham))
                                         .ToList();
                    BindGrid(results);

                    if (results.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            txtTim.Clear();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem có phải click vào một dòng hợp lệ hay không
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                // Hiển thị thông tin lên các điều khiển
                txtMa.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
                dtpNgayNhap.Value = DateTime.Parse(row.Cells[2].Value.ToString());

                // Tìm và chọn giá trị của ComboBox dựa trên MALOAI
                string maloai = row.Cells[3].Value.ToString();
                for (int i = 0; i < cbbLoai.Items.Count; i++)
                {
                    if (((LOAISP)cbbLoai.Items[i]).MALOAI == maloai)
                    {
                        cbbLoai.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

    }
}
