using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pzz
{
    public partial class Twenty_Four_Puzzle : Form
    {
        public System.Drawing.Point Location { get; set; }
        int sobuoc = 0;
        bool check;
        Bitmap bitmap_cut = new Bitmap(375, 375);
        List<Bitmap> list = new List<Bitmap>();
        List<Button> list_btn = new List<Button>();
        Stack<Node> stack_daxong = new Stack<Node>();
        List<Node> list_open = new List<Node>();
        List<Node> list_close = new List<Node>();
        public int x_empty = 300, y_empty = 300;
        public int[] random = new int[26];
        public int[,] location_state1 = { { 0,0 }, { 75, 0 }, { 150, 0 },{ 225, 0 },{ 300, 0 },
                                         { 0,75 }, { 75, 75 }, { 150, 75 },{ 225, 75 },{ 300, 75 },
                                         { 0,150}, { 75, 150}, { 150,150 },{ 225,150 },{ 300,150 },
                                         { 0,225}, { 75, 225}, { 150,225 },{ 225,225 },{ 300,225 },
                                         { 0,300}, { 75, 300}, { 150,300 },{ 225,300 },{ 300,300 },
                                        };
        public int cd_cd = 75;
        public Twenty_Four_Puzzle()
        {

            InitializeComponent();

            list_btn.Add(btn1);
            list_btn.Add(btn2);
            list_btn.Add(btn3);
            list_btn.Add(btn4);
            list_btn.Add(btn5);
            list_btn.Add(btn6);
            list_btn.Add(btn7);
            list_btn.Add(btn8);
            list_btn.Add(btnn8);
            list_btn.Add(btn9);
            list_btn.Add(btn10);
            list_btn.Add(btn11);
            list_btn.Add(btn12);
            list_btn.Add(btn13);
            list_btn.Add(btn14);
            list_btn.Add(btn15);
            list_btn.Add(btn16);
            list_btn.Add(btn17);
            list_btn.Add(btn18);
            list_btn.Add(btn19);
            list_btn.Add(btn20);
            list_btn.Add(btn21);
            list_btn.Add(btn22);
            list_btn.Add(btn23);

        }

        public void Swap(Button a, int so)
        {
            int x = a.Location.X;
            int y = a.Location.Y;
            a.Location = new Point(x_empty, y_empty);
            x_empty = x;
            y_empty = y;
            is_goal();
            int vt1 = 0, vt2 = 0, bientam = 0;
            for (int i = 0; i < 25; i++)
            {
                if (random[i] == so) vt1 = i;
                if (random[i] == 24) vt2 = i;
            }
            bientam = random[vt1];
            random[vt1] = random[vt2];
            random[vt2] = bientam;
        }
        public void Set(Button a, int x, int y)
        {
            a.Location = new Point(x, y);
        }
        public bool Check_location(Button a)
        {
            if ((a.Location.X + cd_cd == x_empty && a.Location.Y == y_empty)
                || (a.Location.X == x_empty && a.Location.Y + cd_cd == y_empty)
                || (a.Location.X == x_empty + cd_cd && a.Location.Y == y_empty)
                || (a.Location.X == x_empty && a.Location.Y == y_empty + cd_cd)) return true;

            return false;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (Check_location(btn1))
                Swap(btn1, 0);
        }

        private void btn3_Click(object sender, EventArgs e)
        {

            if (Check_location(btn3))
                Swap(btn3, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (Check_location(btn4))
                Swap(btn4, 3);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (Check_location(btn6))
                Swap(btn6, 5);
        }

        private void btn5_Click(object sender, EventArgs e)
        {

            if (Check_location(btn5))
                Swap(btn5, 4);
        }

        private void btn7_Click(object sender, EventArgs e)
        {

            if (Check_location(btn7))
                Swap(btn7, 6);
        }

        private void btn8_Click(object sender, EventArgs e)
        {

            if (Check_location(btn8))
                Swap(btn8, 7);
        }

        private void btn2_Click(object sender, EventArgs e)
        {

            if (Check_location(btn2))
                Swap(btn2, 1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            tron();
            button2.Enabled = true;
            foreach (Button btn in panel1.Controls)
            {
                btn.Visible = true;
            }
        }

        private void btnchonanh_Click(object sender, EventArgs e)
        {
            foreach (Button btn in panel1.Controls)
            {
                btn.Visible = true;
            }
            OpenFileDialog choose = new OpenFileDialog();
            choose.Filter =
                "Image Files (JPEG, GIF, PNG , JPG) |*.jpeg; *.gif; *.png; *.jpg";

            if (choose.ShowDialog() == DialogResult.OK)
            {
                picture.Image = Image.FromFile(choose.FileName);

                Graphics graphic = Graphics.FromImage((Image)bitmap_cut);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.DrawImage(Image.FromFile(choose.FileName), 0, 0, 375, 375);
                graphic.Dispose();
                list = new List<Bitmap>();
                int cl = 0, rw = 0;
                for (int x = 0; x < 25 - 1; x++)
                {
                    Bitmap mp = new Bitmap(75, 75);
                    for (int i = 0; i < 75; i++)
                        for (int j = 0; j < 75; j++)
                        {
                            mp.SetPixel(j, i,
                                bitmap_cut.GetPixel(cl * 75 + j, rw * 75 + i));
                        }
                    list.Add(mp);
                    cl += 1;
                    if (cl == 5)
                    {
                        rw += 1;
                        cl = 0;
                    }
                }
                for (int i = 0; i < 24; i++)
                {
                    list_btn[i].Image = list[i];
                }

            }
        }
        public void tron()
        {
            for (int i = 0; i < 26; i++)
            {
                random[i] = i;
            }

            Random rand = new Random();
            do
            {
                for (int i = 0; i < 24; i++)
                {
                    int r = rand.Next(i + 1, 25);

                    int c = random[i];
                    random[i] = random[r];
                    random[r] = c;

                }
            } while (is_solvable(random) == false);
            //for (int i = 0; i < 25; i++)
            //{
            //    random[i] = i;
            //}
            //random[23] = 24;
            //random[24] = 23;
            for (int i = 0; i < 25; i++)
            {

                if (random[i] == 24)
                {
                    x_empty = location_state1[i, 0];
                    y_empty = location_state1[i, 1];
                }
                else list_btn[random[i]].Location = new Point(location_state1[i, 0], location_state1[i, 1]);

            }


        }

        public void is_goal()
        {
            int dem = 0;
            for (int i = 0; i < 24; i++)
            {
                if (list_btn[i].Location.X == location_state1[i, 0] && list_btn[i].Location.Y == location_state1[i, 1]) dem++;
                else break;
            }
            if (dem == 24) MessageBox.Show("Bạn đã thắng");
        }
        public bool is_solvable(int[] state1)
        {
            int ans = 0;
            for (int i = 0; i < 24; i++)
                for (int j = i + 1; j < 25; j++)
                    if (state1[i] > state1[j] && state1[i] != 24) ans += 1;

            if (ans % 2 == 0) return true;
            return false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            list_open.Clear();
            list_close.Clear();
            sobuoc = 0;
            int empty = 0;
            for (int i = 0; i < 25; i++)
            {
                if (random[i] == 24) { empty = i; break; }
            }
            Node root = new Node();
            for (int i = 0; i < 25; i++) { root.state1[i] = random[i]; }
            root.g = 0;
            root.h = tinh_h(root.state1);
            root.f = root.g + root.h;
            root.location_empty = empty;
            root.parent = null;
            //MessageBox.Show(root.h + "");
            list_open.Add(root);

            Xep(root);
            do
            {
                Node lay_ra = new Node();
                if (stack_daxong.Count() == 1) random = stack_daxong.Peek().state1;
                if (stack_daxong.Count() == 0) break;
                lay_ra = stack_daxong.Pop();
                for (int i = 0; i < 25; i++)
                {

                    if (lay_ra.state1[i] == 24)
                    {
                        x_empty = location_state1[i, 0];
                        y_empty = location_state1[i, 1];
                    }
                    else list_btn[lay_ra.state1[i]].Location = new Point(location_state1[i, 0], location_state1[i, 1]);

                }
                Thread.Sleep(200);
            } while (1 == 1);

        }

        public void Xep(Node root)
        {
            Node root1 = (Node)root.Clone();
            do
            {
                //if (list_open.Count  ==0)
                //{
                //    MessageBox.Show("Khong xong");
                //}
                //else
                if (root1.h == 0)
                {
                    stack_daxong.Push(root1);
                    Node get_parent = new Node();
                    do
                    {
                        if (stack_daxong.Peek().parent != null)
                        {
                            get_parent = (Node)stack_daxong.Peek().parent.Clone();
                            stack_daxong.Push(get_parent);
                        }
                    }
                    while (get_parent.parent != null);
                    //MessageBox.Show(" Xong");
                    break;
                }
                else
                {

                    list_close.Add(root1);
                    int vt_remove = 0;
                    for (int i = 0; i < list_open.Count; i++)
                    {
                        if (Node_khacnhau(root1, list_open[i]) == false)
                            vt_remove = i;
                    }
                    list_open.RemoveAt(vt_remove);




                    if (root1.location_empty + 5 <= 24)
                    {
                        Node root_tam = new Node();
                        for (int i = 0; i < 25; i++) { root_tam.state1[i] = root1.state1[i]; }
                        root_tam.g = root1.g + 1;
                        root_tam.location_empty = root1.location_empty;
                        int a = root_tam.state1[root_tam.location_empty];
                        root_tam.state1[root_tam.location_empty] = root_tam.state1[root_tam.location_empty + 5];
                        root_tam.state1[root_tam.location_empty + 5] = a;
                        root_tam.h = tinh_h(root_tam.state1);
                        root_tam.location_empty = root1.location_empty + 5;
                        root_tam.f = root_tam.g + root_tam.h;
                        root_tam.parent = (Node)root1.Clone();

                        if (root1.parent == null)
                        {
                            list_open.Add(root_tam);
                        }
                        else
                        if (kiemtra_open_close(root_tam) == true && root_tam.h != root1.parent.h)
                        {
                            list_open.Add(root_tam);
                        }
                    }
                    if (root1.location_empty - 5 >= 0)
                    {
                        Node root_tam = new Node();
                        for (int i = 0; i < 25; i++) { root_tam.state1[i] = root1.state1[i]; }
                        root_tam.g = root1.g + 1;
                        root_tam.location_empty = root1.location_empty;
                        int a = root_tam.state1[root_tam.location_empty];
                        root_tam.state1[root_tam.location_empty] = root_tam.state1[root_tam.location_empty - 5];
                        root_tam.state1[root_tam.location_empty - 5] = a;
                        root_tam.h = tinh_h(root_tam.state1);
                        root_tam.location_empty = root1.location_empty - 5;
                        root_tam.f = root_tam.g + root_tam.h;
                        root_tam.parent = (Node)root1.Clone();

                        if (root1.parent == null)
                        {
                            list_open.Add(root_tam);
                        }
                        else
                        if (kiemtra_open_close(root_tam) == true && root_tam.h != root1.parent.h)
                        {
                            list_open.Add(root_tam);
                        }
                    }
                    if (((root1.location_empty % 5) + 1) < 5)
                    {
                        Node root_tam = new Node();
                        for (int i = 0; i < 25; i++) { root_tam.state1[i] = root1.state1[i]; }
                        root_tam.g = root1.g + 1;
                        root_tam.location_empty = root1.location_empty;
                        int a = root_tam.state1[root_tam.location_empty];
                        root_tam.state1[root_tam.location_empty] = root_tam.state1[root_tam.location_empty + 1];
                        root_tam.state1[root_tam.location_empty + 1] = a;
                        root_tam.h = tinh_h(root_tam.state1);
                        root_tam.location_empty = root1.location_empty + 1;
                        root_tam.f = root_tam.g + root_tam.h;
                        root_tam.parent = (Node)root1.Clone();

                        if (root1.parent == null)
                        {
                            list_open.Add(root_tam);
                        }
                        else
                        if (kiemtra_open_close(root_tam) == true && root_tam.h != root1.parent.h)
                        {
                            list_open.Add(root_tam);
                        }

                    }
                    if (((root1.location_empty % 5) - 1) >= 0)
                    {
                        Node root_tam = new Node();
                        for (int i = 0; i < 25; i++) { root_tam.state1[i] = root1.state1[i]; }
                        root_tam.g = root1.g + 1;
                        root_tam.location_empty = root1.location_empty;
                        int a = root_tam.state1[root_tam.location_empty];
                        root_tam.state1[root_tam.location_empty] = root_tam.state1[root_tam.location_empty - 1];
                        root_tam.state1[root_tam.location_empty - 1] = a;
                        root_tam.h = tinh_h(root_tam.state1);
                        root_tam.location_empty = root1.location_empty - 1;
                        root_tam.f = root_tam.g + root_tam.h;
                        root_tam.parent = (Node)root1.Clone();

                        if (root1.parent == null)
                        {
                            list_open.Add(root_tam);
                        }
                        else
                        if (kiemtra_open_close(root_tam) == true && root_tam.h != root1.parent.h)
                        {
                            list_open.Add(root_tam);
                        }

                    }
                    root1 = (Node)getNodemin().Clone();
                    Console.WriteLine(root1.h + "  " + root1.g + "  " + sobuoc++);
                    //Xep(node_min);
                }
            } while (1 == 1);
        }
        public bool kiemtra_open_close(Node node)
        {
            foreach (Node valuse in list_open)
            {       //2 node khac nhau tra ve true
                if (Node_khacnhau(node, valuse) == false) return false;
            }
            foreach (Node valuse in list_close)
            {
                if (Node_khacnhau(node, valuse) == false) return false;
            }
            return true;
        }

        public bool Node_khacnhau(Node a, Node b)
        {
            for (int i = 0; i < 25; i++)
            {
                if (a.state1[i] != b.state1[i]) return true;
            }
            return false;
        }
        public int tinh_h(int[] state1)
        {
            int h = 0;
            for (int i = 0; i < 25; i++)
            {
                //toa do hien tại cua i
                if (i != 24)
                {
                    //h +=  Math.Abs((i / 3) - (state1[i] / 3)) +  Math.Abs((i % 3) - (state1[i] % 3));
                    h += Math.Abs((i / 5) - (state1[i] / 5)) * Math.Abs((i / 5) - (state1[i] / 5)) + Math.Abs((i % 5) - (state1[i] % 5)) * Math.Abs((i % 5) - (state1[i] % 5));
                    if (i + 5 < 25 && i == state1[state1[i + 5]]) h++;
                    if (i - 5 >= 0 && i == state1[state1[i - 5]]) h++;
                    if ((i % 5) + 1 < 5 && i == state1[state1[i + 1]]) h++;
                    if ((i % 5) - 1 >= 0 && i == state1[state1[i - 1]]) h++;

                }
            }
            return h;
        }

        public Node getNodemin()
        {
            Node min = new Node();
            min = (Node)list_open[0].Clone();
            foreach (Node Noe in list_open)
            {
                if (Noe.f < min.f) min = (Node)Noe.Clone();
            }
            return min;
        }

        private void btnn8_Click(object sender, EventArgs e)
        {

            if (Check_location(btnn8))
                Swap(btnn8, 8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {

            if (Check_location(btn9))
                Swap(btn9, 9); ;
        }

        private void btn10_Click(object sender, EventArgs e)
        {

            if (Check_location(btn10))
                Swap(btn10, 10);
        }

        private void btn11_Click(object sender, EventArgs e)
        {

            if (Check_location(btn11))
                Swap(btn11, 11);
        }

        private void btn12_Click(object sender, EventArgs e)
        {

            if (Check_location(btn12))
                Swap(btn12, 12);
        }

        private void btn13_Click(object sender, EventArgs e)
        {

            if (Check_location(btn13))
                Swap(btn13, 13);
        }

        private void btn14_Click(object sender, EventArgs e)
        {

            if (Check_location(btn14))
                Swap(btn14, 14);
        }

        private void btn15_Click(object sender, EventArgs e)
        {

            if (Check_location(btn15))
                Swap(btn15, 15);
        }

        private void btn16_Click(object sender, EventArgs e)
        {

            if (Check_location(btn16))
                Swap(btn16, 16);
        }

        private void btn17_Click(object sender, EventArgs e)
        {

            if (Check_location(btn17))
                Swap(btn17, 17);
        }

        private void btn18_Click(object sender, EventArgs e)
        {

            if (Check_location(btn18))
                Swap(btn18, 18);
        }

        private void btn19_Click(object sender, EventArgs e)
        {

            if (Check_location(btn19))
                Swap(btn19, 19);
            //is_goal();
        }

        private void btn20_Click(object sender, EventArgs e)
        {

            if (Check_location(btn20))
                Swap(btn20, 20);
        }

        private void btn21_Click(object sender, EventArgs e)
        {

            if (Check_location(btn21))
                Swap(btn21, 21);
        }

        private void btn22_Click(object sender, EventArgs e)
        {

            if (Check_location(btn22))
                Swap(btn22, 22);
        }

        private void Twenty_Four_Puzzle_Load(object sender, EventArgs e)
        {
            check = false;
            this.Width =500;
            foreach (Button btn in panel1.Controls)
            {
                btn.Visible = false;
            }
        }

        private void Twenty_Four_Puzzle_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Start._main != null)
            {
                Start._main.Show();
            }
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            if (check == false)
            {
                int x = this.Width + 350;
                if (x <= 850)
                    this.Width = x;
                check = !check;
                btnOption.Text = "<";
            }
            else if (check == true)
            {
                int x = this.Width - 350;
                if (x >= 500)
                    this.Width = x;
                check = !check;
                btnOption.Text = ">";
            }
        }

        private void btn23_Click(object sender, EventArgs e)
        {

            if (Check_location(btn23))
                Swap(btn23, 23);
            //is_goal();
        }
    }
}
