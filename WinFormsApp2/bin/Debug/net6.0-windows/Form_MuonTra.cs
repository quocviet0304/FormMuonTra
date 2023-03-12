using System.Xml.Linq;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp2
{
    public partial class Form_MuonTra : Form
    {
        string currentDirectory = System.IO.Directory.GetCurrentDirectory() + "/Data";
        public Form_MuonTra()
        {
            InitializeComponent();
        }

        // Load 2 comboBox ở page1 và 1 comboBox ở page2
        private void LoadComboBox()
        {
            string File_Sach = currentDirectory + "/Sach.txt";
            string File_Doc_Gia = currentDirectory + "/Doc_gia.txt";

            string[] lines_Sach = File.ReadAllLines(File_Sach);
            string[] lines_Doc_Gia = File.ReadAllLines(File_Doc_Gia);

            foreach (string line in lines_Sach)
                cbChonMaSach.Items.Add(line.Split(",")[1]);
            foreach (string line in lines_Doc_Gia)
                cbMaDG.Items.Add(line.Split(",")[0]);

        }

        // Load listViewTable ở lvwDanhSach
        private void Load_lvwDanhSach()
        {
        }
        private void Load_lvwDanhSachTra()
        {
        }
        
        // Load toàn bộ dữ liệu khi mở form lên
        private void Form_MuonTra_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        // Đóng form hiện tại
        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Xóa sạch textbox để người quản lý nhập mới
        private void btnMuonMoi_Click(object sender, EventArgs e)
        {
            cbMaDG.Text = "";
            txtMaSach.Text = "";
            dtNgayMuon.Text = "";
            dtNgayHenTra.Text = "";
            txtSoLuong.Text = "";
        }

        
        // Nhận sự kiện thay đổi text để update các thuộc tính của sách
        private void cbChonMaSach_TextChanged(object sender, EventArgs e)
        {

            string textFile = currentDirectory + "/Sach.txt";
            string[] lines = File.ReadAllLines(textFile);
            foreach (string line in lines)
            {
                if (line.Split(",")[1] == cbChonMaSach.Text)
                {
                    lblMaSach.Text = line.Split(",")[0];
                    lblMaLoai.Text = line.Split(",")[2];
                    lblSoLuong.Text = line.Split(",")[3]; 
                    lblMaTG.Text = line.Split(",")[4];
                    txtMaSach.Text = line.Split(",")[0];
                }
            }
        }

        private void btnChoMuon_Click(object sender, EventArgs e)
        {
            bool exists = false;
            int so_luong_Sach_Muon = 0;
            int so_luong_Sach = 0;

            string File_Muon_Sach = currentDirectory + "/Muon_Sach.txt";

            string File_Sach = currentDirectory + "/Sach.txt";
            string[] lines_Sach = File.ReadAllLines(File_Sach);
            string[] lines_Muon_Sach = File.ReadAllLines(File_Muon_Sach);

            string noiDung = cbMaDG.Text + ","
                                + txtMaSach.Text + ","
                                + txtSoLuong.Text + ","
                                + dtNgayMuon.Text + ","
                                + dtNgayHenTra.Text + "\n";

            //===========================================================//
            // Kiểm tra mã sách đúng không
            foreach (string line in lines_Sach)
                if (txtMaSach.Text == line.Split(",")[0])
                {
                    so_luong_Sach = int.Parse(line.Split(",")[3]);
                    exists = true;
                    break;
                }

            if (!exists)
            {
                MessageBox.Show("Không tồn tại mã sách như vậy");
                txtMaSach.Focus();
                return;
            }

            //===========================================================//
            // Kiểm tra đã điền độc giả chưa
            if (cbMaDG.Text == "")
            {
                MessageBox.Show("Chưa điền độc giả");
                cbMaDG.Focus();
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Chưa điền số lượng");
                return;
            }

            foreach (string line in lines_Muon_Sach)
                if (txtMaSach.Text == line.Split(",")[2])
                {
                    so_luong_Sach_Muon += int.Parse(line.Split(",")[2]);
                }

            if (int.Parse(txtSoLuong.Text) > (so_luong_Sach - so_luong_Sach_Muon))
            {
                MessageBox.Show(string.Format("Số sách còn lại {0} < {1}", so_luong_Sach - so_luong_Sach_Muon, txtSoLuong.Text));
                txtSoLuong.Focus();
                return;
            }
            File.AppendAllText(File_Muon_Sach, noiDung);

        }
    }
}