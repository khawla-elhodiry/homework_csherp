using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeacherAttendance.helper;
using TeacherAttendance.model;

namespace TeacherAttendance
{
    public partial class frmTeacherAttendance : Form
    {

        private AttendanceManagement attendance;
        List<Attendance> attendanceList = new List<Attendance>();
        List<Teacher> tealist = new List<Teacher>();
        List<Course> coulist = new List<Course>();
        List<Room> roomlist = new List<Room>();
        public frmTeacherAttendance()
        {
            InitializeComponent();
        }

        private void FrmTeacherAttendance_Load(object sender, EventArgs e)
        {
            attendance = new AttendanceManagement();
            ShowCourses();
            ShowTeachers();
            ShowRooms();
        }

        /*private void CmbCourses_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void CmbCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }*/

        private void ShowCourses()
        {
            cmbCourses.DataSource = null;
            cmbCourses.DisplayMember = "CourseName";
            cmbCourses.ValueMember = "CourseId";
            cmbCourses.DataSource = attendance.getAllCourses();
            cmbCourses.SelectedIndex = -1;
        }

        private void ShowTeachers()
        {
            cmbTeacherName.DataSource = null;
            cmbTeacherName.DisplayMember = "TeacherName";
            cmbTeacherName.ValueMember = "TeacherId";
            cmbTeacherName.DataSource = attendance.getAllTeachers();
            cmbTeacherName.SelectedIndex = -1;

        }

        private void ShowRooms()
        {
            cmbRoom.DataSource = null;
            cmbRoom.DisplayMember = "RoomName";
            cmbRoom.ValueMember = "RoomId";
            cmbRoom.DataSource = attendance.getAllRooms();
            cmbRoom.SelectedIndex = -1;

        }
        private void CmbCourses_SelectionChangeCommitted(object sender, EventArgs e)
        {
            

            string value = "";
            

            int id = ((Course)((ComboBox)sender).SelectedItem).CourseId;

            if(id != 0)
            {
                return;
            }

            if (Prompt.InputBox("New course", "New course name:", ref value) == DialogResult.OK)
            {
                if (value.Trim().Length > 0)
                {
                    attendance.addNewCourse(value.Trim());
                    ShowCourses();
                }


            }
        }

        private void CmbTeacherName_SelectionChangeCommitted(object sender, EventArgs e)
        {


            string value = "";


            int id = ((Teacher)((ComboBox)sender).SelectedItem).TeacherId;

            if (id != 0)
            {
                return;
            }

            if (Prompt.InputBox("New teacher", "New teacher name:", ref value) == DialogResult.OK)
            {
                if (value.Trim().Length > 0)
                {
                    attendance.addNewTeacher(value.Trim());
                    ShowTeachers();
                }


            }


        }

        private void CmbRoom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string value = "";


            int id = ((Room)((ComboBox)sender).SelectedItem).RoomId;

            if (id != 0)
            {
                return;
            }

            if (Prompt.InputBox("New Room/Lab", "New Room/Lab name:", ref value) == DialogResult.OK)
            {
                if (value.Trim().Length > 0)
                {
                    attendance.addNewRoom(value.Trim());
                    ShowRooms();
                }


            }

        }

       
        private void btnSave_Click(object sender, EventArgs e)
        {
            tealist = attendance.getAllTeachers();
            var findf = tealist.Find(iteam => iteam.TeacherName == cmbTeacherName.Text).TeacherId-1;
            coulist = attendance.getAllCourses();
            var cou = coulist.Find(iteam => iteam.CourseName == cmbCourses.Text).CourseId-1;
            roomlist = attendance.getAllRooms();
            var r = roomlist.Find(iteam => iteam.RoomName == cmbRoom.Text).RoomId-1;
            
           attendanceList.Add(new Attendance(findf, cou,r,dateTimePicker1.Text,dateTimePicker3.Text,textBox2.Text));
            

            var columns = from s in attendanceList
                          orderby s.TeacherId ascending
                          select new
                          {
                              s.TeacherId,s.CourseId,s.RoomId,s.StartTime,s.LeaveTime,s.Comment
                          };
            dataGridView1.DataSource = columns.ToList();
        }
    }
}
