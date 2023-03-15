using BusinessObject.Models;
using DataAccess;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using BusinessObject.DTO;
using ClosedXML.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace MyFAP
{
    public partial class Form1 : Form
    {
        MyFapContext context;
        TeacherDAO dao;
        ClassDAO classDAO;
        MarkDAO markDAO;
        Login login;
        string tid = "1";
        public Form1()
        {


            InitializeComponent();

        }
        public Form1(string teacherID, Login l)
        {
            context = new MyFapContext();
            dao = new TeacherDAO(context);
            classDAO = new ClassDAO(context);
            markDAO = new MarkDAO(context);


            InitializeComponent();
            login = l;
            tid = teacherID;
            login.Hide();
            button2.Visible = false;
            this.FormClosed += form_close;

        }
        private void form_close(object sender, EventArgs e)
        {
            login.Close();
        }
        void teacherInformationBinding()
        {
            Teacher teacher = dao.GetById(tid);
            if (teacher != null)
            {
                label3.Text = teacher.TeacherName;

            }
        }
        void subjectsBinding()
        {
            using (var context = new MyFapContext())
            {
                var teacher_enrollment = from e in context.TeacherEnrollments
                                         join c in context.Classes on e.ClassId equals c.ClassId
                                         join s in context.Subjects on e.SubjectId equals s.SubjectId
                                         where e.TeacherId == tid
                                         select e;
                foreach (TeacherEnrollment enrollment in teacher_enrollment)
                {
                    Class @class = classDAO.GetById(enrollment.ClassId);
                    comboBox1.Items.Add(enrollment.SubjectId.Trim() + "-" + @class.ClassName.Trim());
                }
                comboBox1.SelectedIndex = 0;
            }

        }
        void checklistboxBinding()
        {
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.Add("Lab");
            checkedListBox1.Items.Add("ProgrestTest");
            checkedListBox1.Items.Add("PE");
            checkedListBox1.Items.Add("FE");
            for(int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);
            }
        }
        void bindingColumsDatagridview()
        {
            //binding data from excel to datagridview
            // Hiển thị dữ liệu trên DataGridView
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("StudentID", "StudentID");
            dataGridView1.Columns["StudentID"].DataPropertyName = "StudentID";
            dataGridView1.Columns["StudentID"].ReadOnly = true;
            // dataGridView1.Columns["StudentID"].Visible = false;
            dataGridView1.Columns.Add("SubjectId", "SubjectId");
            dataGridView1.Columns["SubjectId"].DataPropertyName = "SubjectId";
            dataGridView1.Columns["SubjectId"].Visible = false;
            dataGridView1.Columns.Add("StudentName", "StudentName");
            dataGridView1.Columns["StudentName"].DataPropertyName = "StudentName";
            dataGridView1.Columns["StudentName"].ReadOnly = true;
            dataGridView1.Columns.Add("Lab", "Lab");
            dataGridView1.Columns["Lab"].DataPropertyName = "Lab";

            dataGridView1.Columns.Add("ProgrestTest", "ProgrestTest");
            dataGridView1.Columns["ProgrestTest"].DataPropertyName = "ProgrestTest";

            dataGridView1.Columns.Add("FE", "FE");
            dataGridView1.Columns["FE"].DataPropertyName = "FE";

            dataGridView1.Columns.Add("PE", "PE");
            dataGridView1.Columns["PE"].DataPropertyName = "PE";
        }
        void bindingDataGridView()
        {
            try
            {
                //checkBox1.Checked = false;
                string key = comboBox1.SelectedItem.ToString();
                string[] arr = key.Split('-');
                lbTest.Text = arr[0];
                object[] o = new object[checkedListBox1.CheckedItems.Count];
                checkedListBox1.CheckedItems.CopyTo(o, 0);
                bindingColumsDatagridview();

                 var students = from enrollment in context.StudentEnrollments
                               join mark in context.Marks on new { enrollment.StudentId, enrollment.SubjectId } equals new { mark.StudentId, mark.SubjectId } into markGroup
                               from mark in markGroup.DefaultIfEmpty()
                               join Class in context.Classes on enrollment.ClassId equals Class.ClassId
                               join Student in context.Students on enrollment.StudentId equals Student.StudentId
                               where Class.ClassName == arr[1] && enrollment.SubjectId == arr[0]
                               select new
                               {
                                   StudentID = enrollment.StudentId,
                                   Student.StudentName,
                                   enrollment.SubjectId,
                                   mark.Lab,
                                   mark.ProgrestTest,
                                   PE = mark.Pe,
                                   FE = mark.Fe
                               };
                List<MarkDTO> marks = new List<MarkDTO>();
                foreach (var item in students)
                {
                    MarkDTO markDTO = new MarkDTO();
                    markDTO.Lab = item.Lab;
                    markDTO.SubjectId = item.SubjectId;
                    markDTO.StudentId = item.StudentID;
                    markDTO.StudentName = item.StudentName;
                    markDTO.ProgrestTest = item.ProgrestTest;
                    markDTO.Pe = item.PE;
                    markDTO.Fe = item.FE;
                    marks.Add(markDTO);
                }
                dataGridView1.DataSource = marks;
                lbTest.Text = students.Count().ToString();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.ReadOnly = false;
                    row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            teacherInformationBinding();
            subjectsBinding();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checklistboxBinding();
            bindingDataGridView();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Lặp qua tất cả các mục trong CheckBoxList
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                // Kiểm tra xem mục nào được chọn
                if (checkedListBox1.GetItemChecked(i))
                {
                    // Tìm cột tương ứng trong DataGridView và đặt Visible thành true
                    string columnName = checkedListBox1.Items[i].ToString();
                    dataGridView1.Columns[columnName].Visible = true;
                }
                else
                {checkBox1.Checked = false;
                    // Tìm cột tương ứng trong DataGridView và đặt Visible thành false
                    string columnName = checkedListBox1.Items[i].ToString();
                    dataGridView1.Columns[columnName].Visible = false;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                   
                }
                button2.Visible = true;
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                    
                }
               
            }
            //bindingDataGridView();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        checkBox1.Checked = false;
        //bindingDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            login.Close();
        }
        public bool validateData()
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToDouble(dataGridView1.Rows[i].Cells["Lab"].Value.ToString() != "" ? dataGridView1.Rows[i].Cells["Lab"].Value.ToString() : "11") > 10
                    || Convert.ToDouble(dataGridView1.Rows[i].Cells["ProgrestTest"].Value.ToString() != "" ? dataGridView1.Rows[i].Cells["ProgrestTest"].Value.ToString() : "11") > 10
                    || Convert.ToDouble(dataGridView1.Rows[i].Cells["PE"].Value.ToString() != "" ? dataGridView1.Rows[i].Cells["PE"].Value.ToString() : "11") > 10
                    || Convert.ToDouble(dataGridView1.Rows[i].Cells["FE"].Value.ToString() != "" ? dataGridView1.Rows[i].Cells["FE"].Value.ToString() : "11") > 10
                    )
                {
                    return false;
                }
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!validateData())
            {
                MessageBox.Show("0=<Mark<=10", "500");
                bindingDataGridView();
                return;

            }
            else
            {
                string message = "Do you want to save?";
                string title = "Save Mark";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons);
                string key = comboBox1.SelectedItem.ToString();
                string[] arr = key.Split('-');
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            Mark mark = new Mark();
                            mark.StudentId = dataGridView1.Rows[i].Cells["StudentId"].Value.ToString();
                            mark.SubjectId = dataGridView1.Rows[i].Cells["SubjectId"].Value.ToString();
                            mark.Lab = float.Parse(dataGridView1.Rows[i].Cells["Lab"].Value.ToString());
                            mark.Pe = float.Parse(dataGridView1.Rows[i].Cells["PE"].Value.ToString());
                            mark.Fe = float.Parse(dataGridView1.Rows[i].Cells["FE"].Value.ToString());
                            mark.ProgrestTest = float.Parse(dataGridView1.Rows[i].Cells["ProgrestTest"].Value.ToString());
                            // Lấy đối tượng Mark hiện tại với cùng khóa chính nếu đã tồn tại trong context
                            string subjectid = arr[0];
                            Mark currentMark = context.Marks.FirstOrDefault(m => m.SubjectId == subjectid && m.StudentId ==
                            dataGridView1.Rows[i].Cells["StudentId"].Value.ToString().Trim());
                            if (currentMark != null)
                            {
                                // Cập nhật thuộc tính của đối tượng hiện tại với giá trị mới                            
                                currentMark.Lab = float.Parse(dataGridView1.Rows[i].Cells["Lab"].Value.ToString());
                                currentMark.Pe = float.Parse(dataGridView1.Rows[i].Cells["PE"].Value.ToString());
                                currentMark.Fe = float.Parse(dataGridView1.Rows[i].Cells["FE"].Value.ToString());
                                currentMark.ProgrestTest = float.Parse(dataGridView1.Rows[i].Cells["ProgrestTest"].Value.ToString());
                            }
                            else
                            {
                                // Thêm mới đối tượng Mark vào context nếu chưa tồn tại
                                context.Marks.Add(mark);
                            }

                            // Lưu thay đổi vào cơ sở dữ liệu
                            context.SaveChanges();
                        }
                        MessageBox.Show("Successfully");
                        //bindingDataGridView();
                    }
                    catch (Exception ex)
                    {
                        lbTest.Text = ex.Message; 
                        MessageBox.Show("Failed");
                    }

                }

                else
                {
                    // Do something  
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {

            checklistboxBinding();
            List<MarkDTO> markDTOs = LoadDMarkFromExcel();
            if (markDTOs != null)
            {
                bindingColumsDatagridview();
                dataGridView1.DataSource = markDTOs;
            }


        }
        private List<MarkDTO> LoadDMarkFromExcel()
        {
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file Excel được chọn
                string filePath = openFileDialog.FileName;

                // Mở file Excel
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    // Tạo đối tượng Workbook từ stream
                    var workbook = new XLWorkbook(stream);

                    // Lấy worksheet đầu tiên
                    var worksheet = workbook.Worksheet(1);

                    // Lấy tất cả các hàng trong worksheet
                    var rows = worksheet.RowsUsed();

                    // Tạo DataTable
                    var dataTable = new DataTable();

                    // Thêm các cột vào DataTable
                    foreach (var cell in rows.First().Cells())
                    {
                        dataTable.Columns.Add(cell.Value.ToString());
                    }
                    List<MarkDTO> marks = new List<MarkDTO>();
                    // Thêm các dòng vào DataTable
                    foreach (var row in rows.Skip(1))
                    {
                        MarkDTO markDTO = new MarkDTO();
                        markDTO.StudentId = row.Cell(1).Value.ToString();
                        markDTO.StudentName = row.Cell(2).Value.ToString();
                        markDTO.SubjectId = row.Cell(3).Value.ToString();
                        markDTO.Lab = float.Parse(row.Cell(4).Value.ToString());
                        markDTO.Pe = float.Parse(row.Cell(5).Value.ToString());
                        markDTO.Fe = float.Parse(row.Cell(6).Value.ToString());
                        markDTO.ProgrestTest = float.Parse(row.Cell(7).Value.ToString());
                        marks.Add(markDTO);
                    }
                    return marks;
                }

            }
            return null;
        }
    }
}
