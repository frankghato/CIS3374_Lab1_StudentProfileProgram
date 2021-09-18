/*
 * Frank Gatto
 * 9/21/21
 * CIS 3374 Lab 1A - Student Profile Program
 * Allows the user to enter a student's information and then writes it to a file
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CIS3374_Lab1A
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool canSubmit = false;
            bool isTUIDValid = false;
            bool isBirthDayValid = false;
            bool isBirthMonthValid = false;
            bool isBirthYearValid = false;
            bool isPhoneNumberValid = false;
            bool isGenderValid = false;

  
            int TUID = 0;
            //validate tuid
            if (txtTUID.TextLength == 9 && int.TryParse(txtTUID.Text, out TUID))
            {
                isTUIDValid = true;
            }
            else
            {
                MessageBox.Show("You must enter a valid 9 digit TUID number.", "TUID Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int month, day, year = 0;
            //validate birthday
            if (txtDateOfBirth.TextLength == 10)
            {
                if (int.TryParse(txtDateOfBirth.Text.Substring(0, 2), out month) && month >= 1 && month <= 12)
                {
                    isBirthMonthValid = true;
                }
                else
                {
                    MessageBox.Show("You must enter a valid date of birth.", "Birth Month Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //assuming that a student can only be at least 16 and at most 61
                if (int.TryParse(txtDateOfBirth.Text.Substring(6, 4), out year) && year <= 2005 && year >= 1960)
                {
                    isBirthYearValid = true;
                }
                else
                {
                    MessageBox.Show("You must enter a valid year of birth.", "Birth Year Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (int.TryParse(txtDateOfBirth.Text.Substring(3, 2), out day))
                {
                    //if day can be parsed, check to see if it is allowed within the month
                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                    {
                        if (day >= 1 && day <= 31)
                        {
                            isBirthDayValid = true;
                        }
                        else
                        {
                            MessageBox.Show("You must enter a valid date of birth.", "Birth Day Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (month == 2)
                    {
                        if (day >= 1 && day <= 28)
                        {
                            isBirthDayValid = true;
                        }
                        //29 days allowed if year is evenly divisble by 4 because of leap year
                        else if (day == 29 && year % 4 == 0)
                        {
                            isBirthDayValid = true;
                        }
                        else
                        {
                            MessageBox.Show("You must enter a valid date of birth.", "Birth Day Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (month == 4 || month == 6 || month == 9 || month == 11)
                    {
                        if (day >= 1 && day <= 30)
                        {
                            isBirthDayValid = true;
                        }
                        else
                        {
                            MessageBox.Show("You must enter a valid date of birth.", "Birth Day Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must enter a valid date of birth.", "Birth Day Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You must enter a valid date of birth.", "Invalid Date of Birth Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int firstThreeDigits, secondThreeDigits, lastFourDigits;
            if(txtPhoneNumber.TextLength == 12 && int.TryParse(txtPhoneNumber.Text.Substring(0,3), out firstThreeDigits) && int.TryParse(txtPhoneNumber.Text.Substring(4,3), out secondThreeDigits) && int.TryParse(txtPhoneNumber.Text.Substring(8,4), out lastFourDigits))
            {
                isPhoneNumberValid = true;
            }
            else
            {
                MessageBox.Show("You must enter a valid phone number.", "Phone Number Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(radFemale.Checked || radMale.Checked || radOther.Checked)
            {
                isGenderValid = true;
            }
            else
            {
                MessageBox.Show("You must select a gender option.", "Gender unselected Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //if all required fields are successfully validated, call the function to write to file
            if(isTUIDValid && isPhoneNumberValid && isBirthDayValid && isBirthMonthValid && isBirthYearValid && isGenderValid)
            {
                String fileName = "student_profiles.txt";
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                }
                using (StreamWriter writer = File.AppendText(fileName))
                {
                    WriteStudentInfoToFIle(writer, year);
                }
                MessageBox.Show("Student's information written to file.", "Success!", MessageBoxButtons.OK);
                ClearAll();
            }
        }

        //function that writes all student's information to file
        public void WriteStudentInfoToFIle(StreamWriter writer, int year)
        {
            writer.WriteLine("Name: " + txtLastName.Text + ", " + txtFirstName.Text + " " + txtMiddleName.Text);
            writer.WriteLine("Age: " + (2021 - year).ToString());
            writer.WriteLine("TUID: " + txtTUID.Text);
            writer.WriteLine("Email Address: " + txtEmail.Text);
            writer.WriteLine("Phone Number: " + txtPhoneNumber.Text);
            writer.WriteLine("Major: " + txtMajor.Text);
            writer.WriteLine("Expected Graduation Date: " + txtExpectedGraduationDate.Text);
            writer.Write("Undergraduate or Graduate: ");
            if(radGraduate.Checked)
            {
                writer.WriteLine("Graduate");
            }
            else
            {
                writer.WriteLine("Undergraduate");
            }
            writer.Write("Gender: ");
            if(radMale.Checked)
            {
                writer.WriteLine("Male");
            }
            else if(radFemale.Checked)
            {
                writer.WriteLine("Female");
            }
            else
            {
                writer.WriteLine("Other");
            }

        }

        //clear every potentially filled in field
        public void ClearAll()
        {
            txtFirstName.Text = "";
            txtMiddleName.Text = "";
            txtLastName.Text = "";
            txtDateOfBirth.Text =  "";
            txtPhoneNumber.Text = "";
            txtExpectedGraduationDate.Text = "";
            txtTUID.Text = "";
            txtEmail.Text = "";
            txtMajor.Text = "";
            radUndergraduate.Checked = false;
            radGraduate.Checked = false;
            radMale.Checked = false;
            radFemale.Checked = false;
            radOther.Checked = false;
        }
    }
}
