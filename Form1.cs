using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
namespace 高考倒计时3._1
{

    public partial class Form1 : Form
    {
        /// <summary>
        /// 初始目标时间(2022/6/7/09:00)，可以自动矫正
        /// </summary>
        DateTime t2 = new DateTime(2022, 6, 7, 9, 0, 0);


        public Form1()
        {
            InitializeComponent();
            TimeUpdate();
            Position();
            FontSet();

        }


        void TimeUpdate()//更新时间间隔
        {
            timer1.Enabled = true;
            timer1.Interval = 990;
        }

        void Position() //初始化+锁定
        {
            //@w@!!
            this.StartPosition = FormStartPosition.CenterScreen;
            // this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 0);
        }
        void FontSet()//字体设置
        {
            #region 
            //-----------------------字体设置----------------------//
            this.label1.Font = new Font(ConfigurationManager.AppSettings["FontName"], Convert.ToInt32(ConfigurationManager.AppSettings["FontSize"]), (FontStyle)Enum.Parse(typeof(FontStyle), ConfigurationManager.AppSettings["FontStyle"]));
            #endregion
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form fr1 = new Form2();
            fr1.Show();
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            #region --------------时间矫正-----------
            TimeSpan t3 = t2.Subtract(DateTime.Now);//计算时间差

            //------------------------------//
            while (t3.Seconds < 0)//时间矫正
            {
                t2 = t2.AddYears(1);
                t3 = t2.Subtract(DateTime.Now);
            }
            label1.Text = "距离高考还有:" + "\n" + Convert.ToString(t3.Days + "天" + t3.Hours + "时" + t3.Minutes + "分" + t3.Seconds + "秒");//显示时间
            #endregion

            //------------------------------//

            #region --------------颜色---------------
            if (t3.Days <= 100)//判断天数 更改颜色
            {
                this.label1.ForeColor = Color.FromArgb(Convert.ToInt32(ConfigurationManager.AppSettings["color1"]));//红色
            }
            else if (t3.Days <= 200)//判断天数 更改颜色
            {
                this.label1.ForeColor = Color.FromArgb(Convert.ToInt32(ConfigurationManager.AppSettings["color2"]));//黄色
            }
            else//初始颜色
            {
                this.label1.ForeColor = Color.FromArgb(Convert.ToInt32(ConfigurationManager.AppSettings["color3"]));//绿色

            }
            #endregion

        }
    }
}
