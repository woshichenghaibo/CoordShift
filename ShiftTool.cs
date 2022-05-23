using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CoordShift
{
    public partial class MainForm : Form

    {
        const double PI = 3.14159265358979323846264338327950288;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile("Resources/AppBG.png");
            //buttonQuit.BackgroundImage = Image.FromFile("Resources/Cancel.png");
            buttonOK.BackgroundImage = Image.FromFile("Resources/OK.png");
            //buttonTips.BackgroundImage = Image.FromFile("Resources/Faq.png");
            button1.BackgroundImage = Image.FromFile("Resources/A2B.png");
            button2.BackgroundImage = Image.FromFile("Resources/B2A.png");
            labelVer.Text = "V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() ;
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }



void AngleToArc(int a,int b,double c,double Arc)
{
	Arc = (a+b/60.0+c/3600.0)*PI/180.0;
}

void ArcToAngle(double Arc, int a, int b, double c)
{
	a = (int)(Arc * 180 / PI);
	b = (int)((Arc *180 *60/ PI)-(a) * 60);
	c = (Arc * 180 * 3600 / PI) - (b) * 60 - (a) * 3600;
}
        #region ��˹ͶӰ������
        /// <summary>
        /// �Ӵ�����굽ƽ������ĸ�˹����
        /// 
        /// Ĭ�ϵ���ʹ�üٶ���������ȴ�ͶӰ
        /// </summary>
        /// <param name="B">���γ��</param>
        /// <param name="L">��ؾ���</param>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        public static void BL_xy(double B, double L, out double x, out double y, double a, double f)
        {
            BL_xy(B, L, out x, out y, a, f, 6, true);
        }
        /// <summary>
        /// �Ӵ�����굽ƽ������ĸ�˹����
        /// 
        /// Ĭ�ϵ���ʹ�üٶ�����
        /// </summary>
        /// <param name="B">���γ��</param>
        /// <param name="L">��ؾ���</param>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        /// <param name="beltWidth">ͶӰ�ִ��Ĵ���</param>
        public static void BL_xy(double B, double L, out double x, out double y, double a, double f, int beltWidth)
        {
            BL_xy(B, L, out x, out y, a, f, beltWidth, true);
        }
        /// <summary>
        /// �Ӵ�����굽ƽ������ĸ�˹����
        /// 
        /// Ĭ�ϵ������ȴ�ͶӰ
        /// </summary>
        /// <param name="B">���γ��</param>
        /// <param name="L">��ؾ���</param>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        /// <param name="assumedCoord">�Ƿ�ʹ�üٶ�����</param>
        public static void BL_xy(double B, double L, out double x, out double y, double a, double f, bool assumedCoord)
        {
            BL_xy(B, L, out x, out y, a, f, 6, assumedCoord);
        }
        /// <summary>
        /// �Ӵ�����굽ƽ������ĸ�˹����
        /// </summary>
        /// <param name="B">���γ��</param>
        /// <param name="L">��ؾ���</param>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        /// <param name="beltWidth">ͶӰ�ִ��Ĵ���</param>
        /// <param name="assumedCoord">�Ƿ�ʹ�üٶ�����</param>
        public static void BL_xy(double B, double L, out double x, out double y, double a, double f, int beltWidth, bool assumedCoord)
        {
            int beltNum;                           //ͶӰ�ִ��Ĵ���
            beltNum = (int)Math.Ceiling((L - (beltWidth == 3 ? 1.5 : 0)) / beltWidth);
            if (beltWidth == 3 && beltNum * 3 == L - 1.5) beltNum += 1;
            L -= beltNum * beltWidth - (beltWidth == 6 ? 3 : 0);
            Bl_xy(B, L, out x, out y, a, f, beltWidth);
            //����ɼٶ����꣬ƽ��500km��ǰ��Ӵ���
            if (assumedCoord) y += 500000 + beltNum * 1000000;
        }
        /// <summary>
        /// �Ӵ�����굽ƽ������ĸ�˹����
        /// 
        /// ָ�����������ߣ����ڽ����ڴ����㣬��ʱ�ز�ʹ�üٶ�����
        /// </summary>
        /// <param name="B">���γ��</param>
        /// <param name="L">��ؾ���</param>
        /// <param name="CenterL">����������</param>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        /// <param name="beltWidth">ͶӰ�ִ��Ĵ���</param>
        public static void Bl_xy(double B, double dL, out double x, out double y, double a, double f, int beltWidth)
        {
            double ee = (2 * f - 1) / f / f;       //��һƫ���ʵ�ƽ��
            double ee2 = ee / (1 - ee);            //�ڶ�ƫ���ʵ�ƽ��
            double rB, tB, m;
            rB = B * Math.PI / 180;
            tB = Math.Tan(rB);
            m = Math.Cos(rB) * dL * Math.PI / 180;
            double N = a / Math.Sqrt(1 - ee * Math.Sin(rB) * Math.Sin(rB));
            double it2 = ee2 * Math.Pow(Math.Cos(rB), 2);
            x = m * m / 2 + (5 - tB * tB + 9 * it2 + 4 * it2 * it2) * Math.Pow(m, 4) / 24 + (61 - 58 * tB * tB + Math.Pow(tB, 4)) * Math.Pow(m, 6) / 720;
            x = MeridianLength(B, a, f) + N * tB * x;
            y = N * (m + (1 - tB * tB + it2) * Math.Pow(m, 3) / 6 + (5 - 18 * tB * tB + Math.Pow(tB, 4) + 14 * it2 - 58 * tB * tB * it2) * Math.Pow(m, 5) / 120);
        }
        /// <summary>
        /// ƽ�����꣨��Ȼ�����ٶ����꣩���������ĸ�˹����
        /// 
        /// Ĭ��ʹ�����ȴ�
        /// </summary>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="B">���γ��</param>
        /// <param name="L">��ؾ���</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        public static void xy_BL(double x, double y, out double B, out double L, double a, double f)
        {
            xy_BL(x, y, out B, out L, a, f, 6);
        }
        /// <summary>
        /// ƽ�����꣨��Ȼ�����ٶ����꣩���������ĸ�˹����
        /// </summary>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="B">���γ��</param>
        /// <param name="L">��ؾ���</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        /// <param name="beltWidth">ͶӰ�ִ��Ĵ���</param>
        public static void xy_BL(double x, double y, out double B, out double L, double a, double f, int beltWidth)
        {
            //���Ϊ�ٶ����꣬ת��Ϊ��Ȼ����
            int beltNum = 0;
            if (y > 1000000)
            {
                beltNum = (int)Math.Ceiling(y / 1000000) - 1;
                y -= 1000000 * beltNum + 500000;
            }
            //���γ���뾭��
            xy_Bl(x, y, out B, out L, a, f, beltWidth);
            //��⾭��
            L += beltWidth * beltNum - ((beltWidth == 6) ? 3 : 0);
        }
        /// <summary>
        /// ƽ�����꣨��Ȼ���꣩���������ĸ�˹����
        /// </summary>
        /// <param name="x">ƽ������</param>
        /// <param name="y">ƽ�����</param>
        /// <param name="B">���γ��</param>
        /// <param name="l">���Ȳ�</param>
        /// <param name="a">�ο����򳤰���</param>
        /// <param name="f">�ο�������ʵ���</param>
        /// <param name="beltWidth">ͶӰ�ִ��Ĵ���</param>
        private static void xy_Bl(double x, double y, out double B, out double l, double a, double f, int beltWidth)
        {
            if (y > 1000000)
            {
                throw new Exception("coordinates type error,natural one please!");
            }
            double ee = (2 * f - 1) / f / f;       //��һƫ���ʵ�ƽ��
            double ee2 = ee / (1 - ee);            //�ڶ�ƫ���ʵ�ƽ��
            double cA, cB, cC, cD, cE;
            cA = 1 + 3 * ee / 4 + 45 * ee * ee / 64 + 175 * Math.Pow(ee, 3) / 256 + 11025 * Math.Pow(ee, 4) / 16384;
            cB = 3 * ee / 4 + 15 * ee * ee / 16 + 525 * Math.Pow(ee, 3) / 512 + 2205 * Math.Pow(ee, 4) / 2048;
            cC = 15 * ee * ee / 64 + 105 * Math.Pow(ee, 3) / 256 + 2205 * Math.Pow(ee, 4) / 4096;
            cD = 35 * Math.Pow(ee, 3) / 512 + 315 * Math.Pow(ee, 4) / 2048;
            cE = 315 * Math.Pow(ee, 4) / 131072;
            double Bf = x / (a * (1 - ee) * cA);
            do
            {
                B = Bf;
                Bf = (x + a * (1 - ee) * (cB * Math.Sin(2 * Bf) / 2 - cC * Math.Sin(4 * Bf) / 4 + cD * Math.Sin(6 * Bf) / 6) - cE * Math.Sin(8 * Bf) / 8) / (a * (1 - ee) * cA);
            }
            while (Math.Abs(B - Bf) > 0.00000000001);
            double N = a / Math.Sqrt(1 - ee * Math.Pow(Math.Sin(Bf), 2));
            double V2 = 1 + ee2 * Math.Pow(Math.Cos(Bf), 2);
            double it2 = ee2 * Math.Pow(Math.Cos(Bf), 2);
            double tB2 = Math.Pow(Math.Tan(Bf), 2);
            B = Bf - V2 * Math.Tan(Bf) / 2 * (Math.Pow(y / N, 2) - (5 + 3 * tB2 + it2 - 9 * it2 * tB2) * Math.Pow(y / N, 4) / 12 + (61 + 90 * tB2 + 45 * tB2 * tB2) * Math.Pow(y / N, 6) / 360);
            l = (y / N - (1 + 2 * tB2 + it2) * Math.Pow(y / N, 3) / 6 + (5 + 28 * tB2 + 24 * tB2 * tB2 + 6 * it2 + 8 * it2 * tB2) * Math.Pow(y / N, 5) / 120) / Math.Cos(Bf);
            B = B * 180 / Math.PI;
            l = l * 180 / Math.PI;
        }
        #endregion
        /// <summary>
        /// ��γ����������߻���
        /// </summary>
        /// <param name="B">γ��</param>
        /// <param name="a">������</param>
        /// <param name="f">���ʵ���</param>
        /// <returns>�����߻���</returns>
        public static double MeridianLength(double B, double a, double f)
        {
            double ee = (2 * f - 1) / f / f; //��һƫ���ʵ�ƽ�� 
            double rB = B * Math.PI / 180; //����ת��Ϊ����
            //�����߻�����ʽ��ϵ��
            double cA, cB, cC, cD, cE;
            cA = 1 + 3 * ee / 4 + 45 * Math.Pow(ee, 2) / 64 + 175 * Math.Pow(ee, 3) / 256 + 11025 * Math.Pow(ee, 4) / 16384;
            cB = 3 * ee / 4 + 15 * Math.Pow(ee, 2) / 16 + 525 * Math.Pow(ee, 3) / 512 + 2205 * Math.Pow(ee, 4) / 2048;
            cC = 15 * Math.Pow(ee, 2) / 64 + 105 * Math.Pow(ee, 3) / 256 + 2205 * Math.Pow(ee, 4) / 4096;
            cD = 35 * Math.Pow(ee, 3) / 512 + 315 * Math.Pow(ee, 4) / 2048;
            cE = 315 * Math.Pow(ee, 4) / 131072;
            //�����߻���
            return a * (1 - ee) * (cA * rB - cB * Math.Sin(2 * rB) / 2 + cC * Math.Sin(4 * rB) / 4 - cD * Math.Sin(6 * rB) / 6 + cE * Math.Sin(8 * rB) / 8);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double x=0, y=0; // ��˹ͶӰƽ������
            double B=0, L=0;
            //if (textBoxB.Text.Replace(",", "").Length != textBoxB.Text.Length - 1)//�������������ţ���Ϊ�ȷ����ʾ�ľ�γ��
            //{

            //    textBoxB.Text = textBoxB.Text.Substring(textBoxB.Text.LastIndexOf(",") + 1);
            //    labelVer.Text = textBoxB.Text.Substring(textBoxB.Text.LastIndexOf(",") + 1);
            //}
            if (!IsNumber(textBoxB.Text) || !IsNumber(textBoxL.Text))

            {
                MessageBox.Show(this, "Not a number!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Hand) ;

            }
            else
            {
                
                B = double.Parse(textBoxB.Text);
                L = double.Parse(textBoxL.Text);
                if ((B > 0) && (L > 0))
                {
                BL_xy(B, L, out x, out y, 6378137, 298.2572235635, 6, true);
                textBoxX.Text = x.ToString();
                textBoxY.Text = y.ToString();
                } 

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double x = 0, y = 0; // ��˹ͶӰƽ������
            double B=0, L=0;
            if (!IsNumber(textBoxX.Text) || !IsNumber(textBoxY.Text))
            {

                MessageBox.Show(this, "Not a number!", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
            x = double.Parse(textBoxX.Text);
            y = double.Parse(textBoxY.Text);

            if ((x > 0) && (y > 0))
            {
                xy_BL(x, y, out B, out L, 6378137, 298.2572235635, 6);
                textBoxB.Text = B.ToString();
                textBoxL.Text = L.ToString();
            }
            }


        }

        /// <summary>
        /// �ж��Ƿ�������
        /// </summary>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        public static bool IsNumber(string strNumber)
        {

            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");


            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return
                !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }
    }
}