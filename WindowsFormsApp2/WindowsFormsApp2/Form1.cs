using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            IncomeLabel1.Text = "";
            IncomeLabel2.Text = "";
            IncomeLabel3.Text = "";
            IncomeLabel4.Text = "";
            IncomeLabel5.Text = "";
            IncomeLabelError.Text = "";
            IncomeLabelError.ForeColor = Color.Red;

            SaleryLabel.Text = "";
            SaleryLabelError.Text = "";
            SaleryLabelError.ForeColor = Color.Red;
            ArbetsgivarAvgLabel.Text = "";
            BolagsskattLabel.Text = "31,42% av vinsten";

            ProfitLabel1.Text = "";
            ProfitLabel2.Text = "";
            ProfitLabel3.Text = "";
            ProfitLabel4.Text = "";
            ProfitLabel5.Text = "";

            Company company = new Company();
            company.EmployeeList = new List<Employee>();
            for (int i = 0; i < 5; i++)
            {
                Employee employee = new Employee();
                company.EmployeeList.Add(employee);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (CheckTextBoxIncomeValues(IncomeTextBox))
            {
                string perMonthText = "kr/månaden";

                List<double> montlyIncome = new List<double>();
                montlyIncome = RetunIncomeList(montlyIncome);

                IncomeLabel1.Text = montlyIncome[0].ToString() + perMonthText;
                IncomeLabel2.Text = montlyIncome[1].ToString() + perMonthText;
                IncomeLabel3.Text = montlyIncome[2].ToString() + perMonthText;
                IncomeLabel4.Text = montlyIncome[3].ToString() + perMonthText;
                IncomeLabel5.Text = montlyIncome[4].ToString() + perMonthText;
                IncomeLabelError.Text = "";

                calculateProfit();
            }
            else
            {
                IncomeLabel1.Text = "";
                IncomeLabel2.Text = "";
                IncomeLabel3.Text = "";
                IncomeLabel4.Text = "";
                IncomeLabel5.Text = "";
                IncomeLabelError.Text = "Not a valid int value";
            }
        }

        private void SaleryTextBox_TextChanged(object sender, EventArgs e)
        {
            if (CheckTextBoxIncomeValues(SaleryTextBox))
            {             
                SaleryLabel.Text = RetunSalery().ToString() + "kr (5 Personer)";
                SaleryLabelError.Text = "";

                ArbetsgivarAvgLabel.Text = RetunArbetsgivarAvgift().ToString() + "kr (31,42%)";

                calculateProfit();
            }
            else
            {
                SaleryLabelError.Text = "Not a valid int value";
            }
        }

        public void calculateProfit()
        {
            if (CheckTextBoxIncomeValues(IncomeTextBox) && CheckTextBoxIncomeValues(SaleryTextBox))
            {
                string perMonthText = "kr/månaden";

                List<double> incomeList = new List<double>();
                incomeList = RetunIncomeList(incomeList);

                List<double> bruttoVinstList = new List<double>();
                bruttoVinstList = RetunBruttoVinstList(bruttoVinstList);

                List<double> bolagsskattList = new List<double>();
                bolagsskattList = RetunBolagsskattList(bolagsskattList);

                List<double> nettoVinstList = new List<double>();
                for (int i = 0; i < 5; i++)
                {
                    double nettoVinst = Math.Round(bruttoVinstList[i] - bolagsskattList[i]);
                    nettoVinstList.Add(nettoVinst);
                }

                ProfitLabel1.Text = nettoVinstList[0].ToString() + perMonthText;
                ProfitLabel2.Text = nettoVinstList[1].ToString() + perMonthText;
                ProfitLabel3.Text = nettoVinstList[2].ToString() + perMonthText;
                ProfitLabel4.Text = nettoVinstList[3].ToString() + perMonthText;
                ProfitLabel5.Text = nettoVinstList[4].ToString() + perMonthText;
            }
            else
            {
                ProfitLabel1.Text = "";
                ProfitLabel2.Text = "";
                ProfitLabel3.Text = "";
                ProfitLabel4.Text = "";
                ProfitLabel5.Text = "";
            }
        }

        public List<double> RetunIncomeList(List<double> incomeList)
        {    
            for (int i = 1; i < 6; i++)
            {
                // hourSalery * 8h/day * 5days/week * 4weeks/month
                incomeList.Add(i * Convert.ToDouble(IncomeTextBox.Text) * 8 * 5 * 4);
            }         
            return incomeList;
        }

        public List<double> RetunBruttoVinstList(List<double> bruttoVinstList)
        {
            List<double> incomeList = new List<double>();
            incomeList = RetunIncomeList(incomeList);

            for (int i = 0; i < 5; i++)
            {
                // Vinst = Inkomst - Lön - Arbetsgivar-avgift
                double bruttoVinst = incomeList[i] - RetunSalery() - RetunArbetsgivarAvgift();
                bruttoVinstList.Add(bruttoVinst);
            }
            
            return bruttoVinstList;
        }

        public List<double> RetunBolagsskattList(List<double> bolagsskattList)
        {
            List<double> bruttoVinstList = new List<double>();
            bruttoVinstList = RetunBruttoVinstList(bruttoVinstList);

            for (int i = 0; i < 5; i++)
            {
                // Bolagsskatt = Vinst * 0,22%
                double bolagsskatt = Math.Round(bruttoVinstList[i] * 0.22, 0);
                bolagsskattList.Add(bolagsskatt);
            }

            return bolagsskattList;
        }

        public double RetunSalery()
        {
            // Lön = Lön * 5
            return Convert.ToDouble(SaleryTextBox.Text) * 5;
        }

        public double RetunArbetsgivarAvgift()
        {
            // Arbetsgivar-avgift = Lön * 31,42%
            return Math.Round(RetunSalery() * 0.3142, 0);
        }

        public bool CheckTextBoxIncomeValues(Control textBoxControl)
        {
            // Checks that "textBoxIncome" is not null
            if (string.IsNullOrEmpty(textBoxControl.Text))
            {
                return false;
            }

            // Checks that "textBoxIncome" is of int value
            try
            {
                int income = Convert.ToInt32(textBoxControl.Text);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click_1(object sender, EventArgs e)
        {

        }
    }
}
