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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<string> danhSachCauTraLoi = new List<string>();
        private int chiSoCauTraLoiDung = -1;
        private int chiSoCauHoiHienTai = 0;

        private string chuoiKetNoi = "Server=MSI\\SQLEXPRESS;Database=DB_TracNghiem;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
            TaiCauHoiTiepTheo();
        }

        private void TaiCauHoiTiepTheo()
        {
            try
            {
                using (SqlConnection ketNoi = new SqlConnection(chuoiKetNoi))
                {
                    ketNoi.Open();

                    string truyVanCauHoi = "SELECT NoiDung FROM CauHoi WHERE ID = @ID_CauHoi";
                    SqlCommand lenhTruyVanCauHoi = new SqlCommand(truyVanCauHoi, ketNoi);
                    lenhTruyVanCauHoi.Parameters.AddWithValue("@ID_CauHoi", chiSoCauHoiHienTai + 1);

                    SqlDataReader docGiaCauHoi = lenhTruyVanCauHoi.ExecuteReader();

                    if (docGiaCauHoi.Read())
                    {
                        labelCauHoi.Text = docGiaCauHoi["NoiDung"].ToString();
                    }

                    docGiaCauHoi.Close();

                    string truyVanCauTraLoi = "SELECT NoiDung, LaDapAnDung FROM CauTraLoi WHERE ID_CauHoi = @ID_CauHoi";
                    SqlCommand lenhTruyVanCauTraLoi = new SqlCommand(truyVanCauTraLoi, ketNoi);
                    lenhTruyVanCauTraLoi.Parameters.AddWithValue("@ID_CauHoi", chiSoCauHoiHienTai + 1);

                    SqlDataReader docGiaCauTraLoi = lenhTruyVanCauTraLoi.ExecuteReader();

                    danhSachCauTraLoi.Clear();
                    List<int> chiSoCauTraLoiDungList = new List<int>();

                    while (docGiaCauTraLoi.Read())
                    {
                        danhSachCauTraLoi.Add(docGiaCauTraLoi["NoiDung"].ToString());
                        if (Convert.ToBoolean(docGiaCauTraLoi["LaDapAnDung"]))
                        {
                            chiSoCauTraLoiDungList.Add(danhSachCauTraLoi.Count - 1);
                        }
                    }

                    docGiaCauTraLoi.Close();

                    if (danhSachCauTraLoi.Count > 0)
                    {
                        Random ngauNhien = new Random();
                        danhSachCauTraLoi = danhSachCauTraLoi.OrderBy(item => ngauNhien.Next()).ToList();

                        radioButton1.Text = danhSachCauTraLoi[0];
                        radioButton2.Text = danhSachCauTraLoi[1];
                        radioButton3.Text = danhSachCauTraLoi[2];
                        radioButton4.Text = danhSachCauTraLoi[3];

                        chiSoCauTraLoiDung = chiSoCauTraLoiDungList.IndexOf(danhSachCauTraLoi.IndexOf("Câu trả lời đúng"));
                    }
                    else
                    {
                        MessageBox.Show("Không có câu trả lời cho câu hỏi này!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu từ cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void KiemTraCauTraLoi(int chiSoDaChon)
        {
            if (chiSoDaChon == chiSoCauTraLoiDung)
            {
                MessageBox.Show("Chính xác!");
            }
            else
            {
                MessageBox.Show("Sai rồi!");
            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            chiSoCauHoiHienTai++;
            TaiCauHoiTiepTheo();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            KiemTraCauTraLoi(0);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            KiemTraCauTraLoi(1);

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            KiemTraCauTraLoi(2);

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            KiemTraCauTraLoi(3);

        }
    }
}
