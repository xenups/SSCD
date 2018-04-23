using PersianDate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SoldierCountDown
{
    
    public struct timeS
    {
        public int year, month,day;
    }
    public partial class Form1 : Form
    {
        System.Timers.Timer t;
        int h, m, s,d,month;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Stop();
            Application.DoEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            d = 0;
            month = 0;
            timeS time = new timeS();
            time.year = 0;

            t.Start();
            calculateDate();

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += onTimeEvent;
            maskedTextBox1.Mask = "0000/00/00";
            maskedTextBox1.MaskInputRejected += new MaskInputRejectedEventHandler(maskedTextBox1_MaskInputRejected);



        }
        struct DataParameter
        {
            public int Process;
            public int Delay;

        }
        private DataParameter _inputParameter;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int process = ((DataParameter)e.Argument).Process;
            int delay = ((DataParameter)e.Argument).Delay;
            int index = 1;
            try
            {
                for(int i  = 0; i < process; i++)
                {
                    if(!backgroundWorker1.CancellationPending )
                    {
                        backgroundWorker1.ReportProgress(index++*100/process,string.Format("Process data {0}",i));
                        Thread.Sleep(delay);
                    }
                }
            }
            catch(Exception ex)
            {
                backgroundWorker1.CancelAsync();
                MessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }

        }

      

       
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            MessageBox.Show("input digit");
        }

        public Form1()
        {
            InitializeComponent();
        }
        private void calculateDate()
        {
            DateTime datestart = DateTime.Now;
           

            string finishFa = maskedTextBox1.Text;
            if(finishFa == null || finishFa =="")
            { finishFa = "1397/5/29"; }
            //string to FaDateTime
            FaDateTime faDate = new  FaDateTime();
            FaDate dateFarsi = faDate.getFaDateTime(finishFa);
            if (dateFarsi.day > faDate.getthisMonthsDay())
            {
                MessageBox.Show("خطا روز بزرگتر از روز های ماه است");
                return ;
            }
            if((dateFarsi.month< dateFarsi.thisMonth && dateFarsi.year<dateFarsi.thisYear)||(dateFarsi.year < dateFarsi.thisYear))
            {
                MessageBox.Show("خطا  سال یا ماه کوچکتر از مقدار معین است");
                return;
            }
          try { 
            DateTime finishTime = ConvertDate.ToEn(finishFa);
            TimeSpan difference = finishTime - datestart;

                int days = dateFarsi.toEndMonth ;
                int daystoend = dateFarsi.toEndMonth;
                if ( daystoend>= faDate.getthisMonthsDay())
                {
                    daystoend = daystoend - faDate.getthisMonthsDay();
                    this.month ++;
                }
            s = difference.Seconds;
            m = difference.Minutes;
            h = difference.Hours;

            this.month = ((finishTime.Year - datestart.Year) * 12) + finishTime.Month - datestart.Month; ;
            this.d= daystoend;
            }
            catch(Exception ex)
            {
            }

        }

        private void onTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(()=>
                {
                    s -= 1;
                    if(s==0)
                    {
                        s = 59;
                        m -= 1;

                    }
                    if(m==0)
                    {
                        m = 59;
                        h -= 1;

                    }
                    if(h==0)
                    {
                        h = 23;
                        d -= 1;
                    }
                    if(d==0)
                    {
                        d = 29;
                        month -= 1;
                    }
                    if(month==0)
                    {

                    }
                    textResult.Text = string.Format("{0} : {1} : {2}",
                    h.ToString().PadLeft(2, '0'),
                    m.ToString().PadLeft(2,'0'),
                    s.ToString().PadLeft(2, '0')
                    );
                   dayMonthlabel.Text = string.Format("{0}  {1}",
                   month.ToString().PadLeft(2, '0'),
                   d.ToString().PadLeft(2, '0')
                  );
                }
                ));
        }
    }
}
