using Business.Persons;
using DataAccess.Persons;
using FontAwesome.Sharp;
using FromSide.General;
using Models.Persons;
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

namespace FromSide.Persons
{


    public partial class frmPersonsLists : Form
    {
        private static List<PersonDTO> _DtAllPersons = clsPerson.GetAll();

        private DataTable _dtPeople = ConvertListToDataTable(_DtAllPersons);

        private void _RefreshDataGrid()
        {
            //Thread t = new Thread ..
            //_DtAllPersons = clsPerson.GetAll();
            //_dtPeople = ConvertListToDataTable(_DtAllPersons);
            //dtgrdvPersons.DataSource = _dtPeople;
            Thread t = new Thread(() =>
            {
                var allPersons = clsPerson.GetAll();
                var dtPeople = ConvertListToDataTable(allPersons);

                this.Invoke((MethodInvoker)delegate
                {
                    _DtAllPersons = allPersons;
                    _dtPeople = dtPeople;
                    dtgrdvPersons.DataSource = _dtPeople;
                });
            });

            t.IsBackground = true;
            t.Start();
        }

        public frmPersonsLists()
        {
            InitializeComponent();
            this.Icon = FontAwesome.Sharp.IconChar.Users.ToIcon();
        }

        private static DataTable ConvertListToDataTable(List<PersonDTO> list)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("PersonID", typeof(int));
            dt.Columns.Add("Identifier", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("SecondName", typeof(string));
            dt.Columns.Add("ThirdName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("GendorCaption", typeof(string));
            dt.Columns.Add("DateOfBirth", typeof(DateTime));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Address", typeof(string));

            //foreach (var p in list)
            //{
            //    dt.Rows.Add(p.PersonID, p.IdentifierID, p.FirstName, p.SecondName, p.ThirdName,
            //                p.LastName,p.Gender, p.BirthDate, p.Phone, p.Email, p.Address);
            //}
            foreach (var p in list)
            {
                string genderText = (p.Gender == true) ? "Male" : "Female";

                dt.Rows.Add(p.PersonID, p.IdentifierID, p.FirstName, p.SecondName, p.ThirdName,
                            p.LastName, genderText, p.BirthDate, p.Phone, p.Email, p.Address);
            }

            return dt;
        }


        private void frmPersonsLists_Load(object sender, EventArgs e)
        {
            UIManager.ApplySettingsToForm(this);
            dtgrdvPersons.DataSource = _dtPeople;
            cmbxFilterBy.SelectedIndex = 0; // تعيين الفلتر الافتراضي
            lblTitle.Text = "Persons List there are" + _DtAllPersons.Count.ToString();
            if (dtgrdvPersons.Rows.Count > 0)
            {
                dtgrdvPersons.Columns[0].HeaderText = "ID";
                dtgrdvPersons.Columns[0].Width = 100;

                dtgrdvPersons.Columns[1].HeaderText = "Identifier";
                dtgrdvPersons.Columns[1].Width = 150;

                dtgrdvPersons.Columns[2].HeaderText = "First Name";
                dtgrdvPersons.Columns[2].Width = 180;

                dtgrdvPersons.Columns[3].HeaderText = "Second";
                dtgrdvPersons.Columns[3].Width = 150;

                dtgrdvPersons.Columns[4].HeaderText = "Third";
                dtgrdvPersons.Columns[4].Width = 150;

                dtgrdvPersons.Columns[5].HeaderText = "Last";
                dtgrdvPersons.Columns[5].Width = 150;

                dtgrdvPersons.Columns[6].HeaderText = "Gender";
                dtgrdvPersons.Columns[6].Width = 120;

                dtgrdvPersons.Columns[7].HeaderText = "Birth Date";
                dtgrdvPersons.Columns[7].Width = 150;

                dtgrdvPersons.Columns[8].HeaderText = "Phone";
                dtgrdvPersons.Columns[8].Width = 200;

                dtgrdvPersons.Columns[9].HeaderText = "Email";
                dtgrdvPersons.Columns[9].Width = 250;

                dtgrdvPersons.Columns[10].HeaderText = "Address";
                dtgrdvPersons.Columns[10].Width = 250;

            }

        }

        private void cmbxFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbxSearch.Visible = (cmbxFilterBy.Text != "None");
            if (txbxSearch.Visible)
            {
                txbxSearch.Text = "";
                txbxSearch.Focus();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dtgrdvPersons.SelectedRows[0].Cells[0].Value);
            frmShowPersonInfo frmShowPersonInfo = new frmShowPersonInfo(PersonID);
            frmShowPersonInfo.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo(Convert.ToInt32(dtgrdvPersons.SelectedRows[0].Cells[0].Value));
            frm.ShowDialog();
            _RefreshDataGrid();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Person [" + dtgrdvPersons.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (clsPerson.Delete(Convert.ToInt32(dtgrdvPersons.SelectedRows[0].Cells[0].Value)))
                {
                    MessageBox.Show("Person Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshDataGrid();
                }
                else
                    MessageBox.Show("Failed to delete Person.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPersonInfo();
            frm.ShowDialog();
            _RefreshDataGrid();
        }

        private void frmPersonsLists_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete)
                this.Close();
        }

        private void dtgrdvPersons_DoubleClick(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dtgrdvPersons.CurrentRow.Cells[0].Value);
            frmShowPersonInfo frmShowPersonInfo = new frmShowPersonInfo(PersonID);
            frmShowPersonInfo.ShowDialog();
        }
        private void txbxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txbxSearch.Text.Trim().Replace("'", "''"); // حماية من مشاكل علامات الاقتباس

            if (string.IsNullOrEmpty(searchText))
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblTitle.Text = "Persons List there are " + _DtAllPersons.Count.ToString();
                return;
            }

            // إذا تريد البحث فقط في عمود محدد:
            string filter = "";

            switch (cmbxFilterBy.Text)
            {
                case "Person ID":
                    // البحث يطابق الرقم بالضبط (لأنه رقم)
                    filter = string.Format("Convert(PersonID, 'System.String') LIKE '%{0}%'", searchText);
                    break;
                case "Identifier":
                    filter = string.Format("Identifier LIKE '%{0}%'", searchText);
                    break;
                case "First Name":
                    filter = string.Format("FirstName LIKE '%{0}%'", searchText);
                    break;
                case "Second Name":
                    filter = string.Format("SecondName LIKE '%{0}%'", searchText);
                    break;
                case "Third Name":
                    filter = string.Format("ThirdName LIKE '%{0}%'", searchText);
                    break;
                case "Last Name":
                    filter = string.Format("LastName LIKE '%{0}%'", searchText);
                    break;
                case "Gendor":
                    filter = string.Format("GendorCaption LIKE '%{0}%'", searchText);
                    break;
                case "Date of Birth":
                    filter = string.Format("Convert(DateOfBirth, 'System.String') LIKE '%{0}%'", searchText);
                    break;
                case "Phone":
                    filter = string.Format("Phone LIKE '%{0}%'", searchText);
                    break;
                case "Email":
                    filter = string.Format("Email LIKE '%{0}%'", searchText);
                    break;
                case "Name or Email":  // حالة خاصة للبحث في عمودين معًا
                    filter = string.Format("(FirstName LIKE '%{0}%' OR Email LIKE '%{0}%')", searchText);
                    break;
                default:
                    filter = string.Format("Identifier LIKE '%{0}%'", searchText);
                    break;
            }

            _dtPeople.DefaultView.RowFilter = filter;

            lblTitle.Text = "Persons List there are " + _dtPeople.DefaultView.Count.ToString();
        }

        private void dtgrdvPersons_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0) // فقط رؤوس الأعمدة
            {
                e.PaintBackground(e.CellBounds, true);

                // تحديد الأيقونة المناسبة
                IconChar iconChar = IconChar.None;
                string columnName = dtgrdvPersons.Columns[e.ColumnIndex].Name;

                switch (columnName)
                {
                    case "PersonID":
                        iconChar = IconChar.IdBadge;
                        break;
                    case "FirstName":
                        iconChar = IconChar.UserAlt;
                        break;
                    case "SecondName":
                        iconChar = IconChar.User;
                        break;
                    case "ThirdName":
                        iconChar = IconChar.User;
                        break;
                    case "LastName":
                        iconChar = IconChar.User;
                        break;
                    case "GendorCaption":
                        iconChar = IconChar.MarsAndVenus;
                        break;
                    case "DateOfBirth":
                        iconChar = IconChar.BirthdayCake;
                        break;
                    case "Identifier":
                        iconChar = IconChar.IdCard;
                        break;
                    case "Phone":
                        iconChar = IconChar.Phone;
                        break;
                    case "Email":
                        iconChar = IconChar.Envelope;
                        break;
                    case "Address":
                        iconChar = IconChar.MapMarkerAlt;
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

                        // رسم النص بجانب الأيقونة
                        string headerText = dtgrdvPersons.Columns[e.ColumnIndex].HeaderText;
                        using (Brush textBrush = new SolidBrush(dtgrdvPersons.ColumnHeadersDefaultCellStyle.ForeColor))
                        {
                            using (StringFormat sf = new StringFormat()
                            {
                                LineAlignment = StringAlignment.Center,
                                Alignment = StringAlignment.Near
                            })
                            {
                                RectangleF textRect = new RectangleF(x + iconSize + 4, e.CellBounds.Top,
                                                                      e.CellBounds.Width - iconSize - 4, e.CellBounds.Height);
                                e.Graphics.DrawString(headerText, dtgrdvPersons.ColumnHeadersDefaultCellStyle.Font,
                                                      textBrush, textRect, sf);
                            }
                        }
                    }
                }
                else
                {
                    // إذا لم تكن هناك أيقونة، استخدم الرسم العادي للنص
                    e.PaintContent(e.CellBounds);
                }

                e.Handled = true;
            }
        }

    }
}