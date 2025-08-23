using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Business.Employees;
using Models.General;
using Business.General;
using DataAccess.General;
using FromSide.General;

namespace FromSide.Persons
{
    public partial class frmEmployeesLists : Form
    {
        private static  List<clsSomeFullDetailsDTO.vw_EmployeeFullDetailsDTO> _DtAllEmployee = clsSomeFullDetailsData.GetAllEmployeeDetails();

        private DataTable _dtPeople = ConvertListToDataTable(_DtAllEmployee);

        private void _RefreshDataGrid()
        {
            _DtAllEmployee = clsSomeFullDetailsData.GetAllEmployeeDetails();
            _dtPeople = ConvertListToDataTable(_DtAllEmployee);
            dtgrdvEmployees.DataSource = _dtPeople;
        }

        public frmEmployeesLists()
        {
            InitializeComponent();
            this.Icon = FontAwesome.Sharp.IconChar.UserTie.ToIcon();
        }

        private static DataTable ConvertListToDataTable(List<clsSomeFullDetailsDTO.vw_EmployeeFullDetailsDTO> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Employee ID",typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Job Title",typeof (string));
            dt.Columns.Add("Gendor",typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Salary",typeof(int));
            dt.Columns.Add("Is Active",typeof(string));
            dt.Columns.Add("Is Admin",typeof(string));
            dt.Columns.Add("Can Teach",typeof(string));
            dt.Columns.Add("Identifier value", typeof(string));
            dt.Columns.Add("Hire Date",typeof(string));

            foreach (var p in list)
            {
                dt.Rows.Add(
                    p.EmployeeID,
                    p.FullName,
                    p.JobTitleName,
                    p.Gender,
                    p.Phone,
                    p.Salary,
                    p.EmployeeIsActive,
                    p.IsAdministrative,
                    p.CanTeach,
                    p.IdentifierValue,
                    p.HireDate?.ToString("yyyy-MM-dd") ?? "" // التأكد من عدم وجود null
                );
            }

            return dt;
        }
        private void frmEmployeesLists_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);
            _DtAllEmployee = clsSomeFullDetailsData.GetAllEmployeeDetails();
            _dtPeople = ConvertListToDataTable(_DtAllEmployee);

            if (_DtAllEmployee == null || _DtAllEmployee.Count == 0)
            {
                MessageBox.Show("No data returned!");
            }
            else
            {
                Console.WriteLine($"Number of records: {_DtAllEmployee.Count}");
            }

            dtgrdvEmployees.DataSource = _dtPeople;

            cmbxFilterBy.SelectedIndex = 0; 
            lblTitle.Text = "Persons List there are" + _DtAllEmployee.Count.ToString();
            if (dtgrdvEmployees.Rows.Count > 0)
            {

                dtgrdvEmployees.Columns[0].HeaderText = "Employee ID";
                dtgrdvEmployees.Columns[0].Width = 130;

                dtgrdvEmployees.Columns[1].HeaderText = "Name";
                dtgrdvEmployees.Columns[1].Width = 300;

                dtgrdvEmployees.Columns[2].HeaderText = "Job Title";
                dtgrdvEmployees.Columns[2].Width = 200;

                dtgrdvEmployees.Columns[3].HeaderText = "Gender";
                dtgrdvEmployees.Columns[3].Width = 120;

                dtgrdvEmployees.Columns[4].HeaderText = "Phone";
                dtgrdvEmployees.Columns[4].Width = 200;

                dtgrdvEmployees.Columns[5].HeaderText = "Salary";
                dtgrdvEmployees.Columns[5].Width = 150;

                dtgrdvEmployees.Columns[6].HeaderText = "Is Active";
                dtgrdvEmployees.Columns[6].Width = 100;

                dtgrdvEmployees.Columns[7].HeaderText = "Is Admin";
                dtgrdvEmployees.Columns[7].Width = 100;

                dtgrdvEmployees.Columns[8].HeaderText = "Can Teach";
                dtgrdvEmployees.Columns[8].Width = 100;

                dtgrdvEmployees.Columns[9].HeaderText = "Identifier value";
                dtgrdvEmployees.Columns[9].Width = 220;

                dtgrdvEmployees.Columns[10].HeaderText = "Hire Date";
                dtgrdvEmployees.Columns[10].Width = 200;
            }

        }
        private void UpdateFilterInputVisibility()
        {
            bool isDateFilter = cmbxFilterBy.Text == "Hire Date";
            dateTimePicker1.Visible = isDateFilter;
            txbxSearch.Visible = !isDateFilter;
        }

        private void cmbxFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbxSearch.Visible = (cmbxFilterBy.Text != "None");
            if (txbxSearch.Visible)
            {
                txbxSearch.Text = "";
                txbxSearch.Focus();
            }
            UpdateFilterInputVisibility();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int EmployeeId = Convert.ToInt32(dtgrdvEmployees.SelectedRows[0].Cells[0].Value);
            frmShowEmployeeInfo frmShowPersonInfo = new frmShowEmployeeInfo(EmployeeId);
            frmShowPersonInfo.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditEmployeeInfo(Convert.ToInt32(dtgrdvEmployees.SelectedRows[0].Cells[0].Value));
            frm.ShowDialog();
            _RefreshDataGrid();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Employee [" + dtgrdvEmployees.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsEmployee.Delete(Convert.ToInt32(dtgrdvEmployees.SelectedRows[0].Cells[0].Value)))
                {
                    MessageBox.Show("Employee Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshDataGrid();
                }
                else
                    MessageBox.Show("Failed to delete Employee.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is not implemented yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void addNewEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditEmployeeInfo();
            frm.ShowDialog();
            _RefreshDataGrid();
        }

        private void frmEmployeesLists_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete)
                this.Close();
        }

        private void dtgrdvEmployee_DoubleClick(object sender, EventArgs e)
        {
            int EmployeeID = Convert.ToInt32(dtgrdvEmployees.CurrentRow.Cells[0].Value);
            frmShowEmployeeInfo frmshowinfo = new frmShowEmployeeInfo(EmployeeID);
            frmshowinfo.ShowDialog();
        }
        private void txbxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txbxSearch.Text.Trim().Replace("'", "''"); // حماية من مشاكل علامات الاقتباس

            if (string.IsNullOrEmpty(searchText))
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblTitle.Text = "Employee List there are " + _DtAllEmployee.Count.ToString();
                return;
            }
            string filter = "";

            switch (cmbxFilterBy.Text)
            {
                case "None":
                    filter = "";
                    break;
                case "Employee ID":
                    filter = string.Format("Convert(EmployeeID, 'System.String') LIKE '%{0}%'", searchText);
                    break;
                case "Full Name":
                    filter = string.Format("FullName LIKE '%{0}%'", searchText);
                    break;
                case "Identifier":
                    filter = string.Format("IdentifierValue LIKE '%{0}%'", searchText);
                    break;
                case "Phone":
                    filter = string.Format("Phone LIKE '%{0}%'", searchText);
                    break;
                case "Job Title":
                    filter = string.Format("JobTitleName LIKE '%{0}%'", searchText);
                    break;
                case "Is Teaching":
                    filter = string.Format("IsTeaching LIKE '%{0}%'", searchText);
                    break;
                case "Is Administrative":
                    filter = string.Format("IsAdministrative LIKE '%{0}%'", searchText);
                    break;
                case "Can Teach":
                    filter = string.Format("CanTeach LIKE '%{0}%'", searchText);
                    break;
                case "Hire Date":
                    filter = string.Format("HireDate = #{0}#", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    break;
                case "Salary":
                    filter = string.Format("Convert(Salary, 'System.String') LIKE '%{0}%'", searchText);
                    break;
                case "Gender":
                    filter = string.Format("Gender LIKE '%{0}%'", searchText);
                    break;
                default:
                    filter = string.Format("FullName LIKE '%{0}%'", searchText);
                    break;
            }

            _dtPeople.DefaultView.RowFilter = filter;

            lblTitle.Text = "Employee List there are " + _dtPeople.DefaultView.Count.ToString();
        }

        private void dtgrdvEmployees_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.PaintBackground(e.CellBounds, true);

                IconChar iconChar = IconChar.None;
                string columnName = dtgrdvEmployees.Columns[e.ColumnIndex].Name;

                switch (columnName)
                {
                    case "EmployeeID":
                        iconChar = IconChar.IdBadge;
                        break;
                    case "Full Name":
                        iconChar = IconChar.UserAlt;
                        break;
                    case "Job Title":
                        iconChar = IconChar.Briefcase;
                        break;
                    case "Gendor":
                        iconChar = IconChar.MarsAndVenus;
                        break;
                    case "Phone":
                        iconChar = IconChar.Phone;
                        break;
                    case "Salary":
                        iconChar = IconChar.MoneyBillWave;
                        break;
                    case "Is Active":
                        iconChar = IconChar.CheckCircle;
                        break;
                    case "Is Admin":
                        iconChar = IconChar.UserShield;
                        break;
                    case "Can Teach":
                        iconChar = IconChar.ChalkboardTeacher;
                        break;
                    case "Identifier value":
                        iconChar = IconChar.IdCard;
                        break;
                    case "Hire Date":
                        iconChar = IconChar.CalendarCheck;
                        break;
                }

                int iconSize = 24;

                if (iconChar != IconChar.None)
                {
                    using (IconPictureBox pb = new IconPictureBox())
                    {
                        pb.IconChar = iconChar;
                        pb.IconColor = Color.Black;
                        pb.IconSize = iconSize;

                        Bitmap iconBitmap = new Bitmap(iconSize, iconSize);
                        pb.DrawToBitmap(iconBitmap, new Rectangle(0, 0, iconSize, iconSize));

                        int x = e.CellBounds.Left + 4;
                        int y = e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2;

                        e.Graphics.DrawImage(iconBitmap, new Point(x, y));

                        string headerText = dtgrdvEmployees.Columns[e.ColumnIndex].HeaderText;
                        using (Brush textBrush = new SolidBrush(dtgrdvEmployees.ColumnHeadersDefaultCellStyle.ForeColor))
                        {
                            using (StringFormat sf = new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Near
                            })
                            {
                                RectangleF textRect = new RectangleF(x + iconSize + 4, e.CellBounds.Top,
                                                                      e.CellBounds.Width - iconSize - 4, e.CellBounds.Height);
                                e.Graphics.DrawString(headerText, dtgrdvEmployees.ColumnHeadersDefaultCellStyle.Font,
                                                      textBrush, textRect, sf);
                            }
                        }
                    }
                }
                else
                {
                    e.PaintContent(e.CellBounds);
                }

                e.Handled = true;
            }
        }

    }
}