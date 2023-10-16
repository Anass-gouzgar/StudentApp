using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace StudentApp
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=EasySolutions7;Initial Catalog=Student_Crud_App;Integrated Security=True");

        public int StudentId;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecords();
        }

        public void GetStudentsRecords()
        {
            SqlConnection conn = new SqlConnection("Data Source=EasySolutions7;Initial Catalog=Student_Crud_App;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("select * from StudentTable", conn);

            DataTable dt = new DataTable();

            conn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            dt.Load(sdr);

            conn.Close();

            StudentDataGrid.DataSource = dt;
        }


        private void StudentDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentId = Convert.ToInt32(StudentDataGrid.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentDataGrid.SelectedRows[0].Cells[1].Value.ToString();
            txtStudentFname.Text = StudentDataGrid.SelectedRows[0].Cells[2].Value.ToString();
            txtStudentAdresse.Text = StudentDataGrid.SelectedRows[0].Cells[3].Value.ToString();
            txtStudentNumber.Text = StudentDataGrid.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("Insert into StudentTable (StudentName, FatherName, Adresse, MobileNum) values (@StudentName,@FatherName, @Adresse, @MobileNum )", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtStudentFname.Text);
                cmd.Parameters.AddWithValue("@Adresse", txtStudentAdresse.Text);
                cmd.Parameters.AddWithValue("@MobileNum", txtStudentNumber.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("New Student is successfuly Added !", "succes" , MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentsRecords();
                ResetFormControls();
            }
        }

        private void ResetFormControls()
        {
            StudentId = 0;
            txtStudentAdresse.Clear();
            txtStudentFname.Clear();
            txtStudentName.Clear();
            txtStudentNumber.Clear();
            txtStudentName.Focus();
        }

        private bool IsValid()
        {
            if (txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student Name Is Required !", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
                return false;
            }
            return true;
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("Update StudentTable set StudentName=@StudentName, FatherName=@FatherName, Adresse=@Adresse, MobileNum=@MobileNum where StudentId = @StudentId", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@StudentName", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtStudentFname.Text);
                cmd.Parameters.AddWithValue("@Adresse", txtStudentAdresse.Text);
                cmd.Parameters.AddWithValue("@MobileNum", txtStudentNumber.Text);
                cmd.Parameters.AddWithValue("@StudentId", this.StudentId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("New Student is Updated Successfuly !", "updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentsRecords();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a student to update !", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (StudentId > 0)
            {
                SqlCommand cmd = new SqlCommand("delete from StudentTable where StudentId = @StudentId", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@StudentId", this.StudentId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Student is deleted Successfuly !", "deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentsRecords();
                ResetFormControls();
            }
            else
                MessageBox.Show("Please select a student to delete !", "select?", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url = "https://easysolutions.ma/"; 

            Process.Start(url);
        }
    }
}
