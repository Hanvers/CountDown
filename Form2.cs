using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;
namespace 高考倒计时3._1
{
    public partial class Form2 : Form
    {

        // // // -------------------------------------------背景，透明度，初始位置，图片
        /// <summary>
        /// 字体结构（名字，样式，大小，颜色）
        /// </summary>
        struct Fontset
        {
            public string FontName;
            public string FontStyle;
            public string FontSize;
            public string color1;
            public string color2;
            public string color3;
        }//字体结构

        //
        //--------------------
        new Fontset DefaultFont;//默认字体
        Fontset newfont;//新字体
        //--------------------
        //

        /// <summary>
        /// 默认值设置
        /// </summary>
        void Defualtset()
        {
            DefaultFont.FontName = "宋体";//name
            DefaultFont.FontSize = "60";//size
            DefaultFont.FontStyle = " Bold, Italic";//style
            DefaultFont.color1 = "-16744448";//color
            DefaultFont.color2 = "-256";
            DefaultFont.color3 = " -65536";

        }
        /// <summary>
        /// 新字体设置
        /// </summary>
        /// <param name="fontset"></param>
        void FontSet(Fontset fontset)
        {
            //新字体样式
            SetConfigValue("FontSize", fontset.FontSize);
            SetConfigValue("FontStyle", fontset.FontStyle);
            SetConfigValue("FontName", fontset.FontName);
        }
        void FontColorSet(Fontset fontset)
        {   //新字体颜色
            SetConfigValue("color1", fontset.color1);
            SetConfigValue("color2", fontset.color2);
            SetConfigValue("color3", fontset.color3);
        }
        /// <summary>
        /// 更改字体的config文件
        /// 设置字体样式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetConfigValue(string key, string value)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//获取config path
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value;
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value);
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Form2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 改变颜色,颜色不能为纯白或透明
        /// </summary>
        /// <param name="label"></param>
        void ColorChange(Label label)
        {
            DialogResult dr = colorDialog1.ShowDialog();
            if (dr == DialogResult.OK && colorDialog1.Color == Color.White)
            {
                MessageBox.Show(Text = "不支持纯白字体");
            }
            else if (dr == DialogResult.OK && colorDialog1.Color != Color.Transparent)
            {
                label.BackColor = colorDialog1.Color;
            }
            else if (dr == DialogResult.OK && colorDialog1.Color == Color.Transparent)
            {
                MessageBox.Show(Text = "颜色不能为透明");
            }
        }//颜色设置
        private void Form2_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = fontDialog1.ShowDialog();
            if (fontDialog1.Font.Size < 30 && dr == DialogResult.OK)
            {
                MessageBox.Show(Text = "字体过小,至少要大于30");
            }
            else if (fontDialog1.Font.Size > 120)
            {
                MessageBox.Show(Text = "字体过大,至少要小于120");
            }
            else if (dr == DialogResult.OK)//字体大小>30
            {
                newfont.FontName = fontDialog1.Font.Name;
                newfont.FontSize = Convert.ToInt16(fontDialog1.Font.Size).ToString();
                newfont.FontStyle = fontDialog1.Font.Style.ToString();

                //更改字体
                //---------------------------------------------------------------//
                listBox1.Items.Clear();

                listBox1.Items.Add(fontDialog1.Font.Name);
                listBox1.Items.Add(fontDialog1.Font.Style);
                listBox1.Items.Add(fontDialog1.Font.Size);
            }
        }
        #region 颜色更改
        private void button4_Click(object sender, EventArgs e)
        {
            ColorChange(label7);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ColorChange(label8);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ColorChange(label9);
        }
        #endregion
        /// <summary>
        /// 恢复默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            Defualtset();
            FontSet(DefaultFont);
            FontSet(DefaultFont);
            System.Windows.Forms.Application.Restart();
        }
        /// <summary>
        /// 保存颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked && checkBox2.CheckState != CheckState.Checked)
            {
                newfont.color1 = label7.BackColor.ToArgb().ToString();
                newfont.color2 = label8.BackColor.ToArgb().ToString();
                newfont.color3 = label9.BackColor.ToArgb().ToString();
                FontColorSet(newfont);
                System.Windows.Forms.Application.Restart();
            }
            else if (checkBox2.Checked && checkBox1.CheckState != CheckState.Checked)
            {
                FontSet(newfont);
                System.Windows.Forms.Application.Restart();

            }
            else if (checkBox1.Checked)
            {
                this.Close();

            }
            else if (listBox1.Text == null)
            {
                MessageBox.Show(Text = "字体不能为空");
            }
            else
            {
                newfont.color1 = label7.BackColor.ToArgb().ToString();
                newfont.color2 = label8.BackColor.ToArgb().ToString();
                newfont.color3 = label9.BackColor.ToArgb().ToString();
                FontSet(newfont);
                FontColorSet(newfont);
                System.Windows.Forms.Application.Restart();
            }

        }

        #region 释放内存
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
        #endregion
    }
}
