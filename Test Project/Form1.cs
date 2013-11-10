using MouseHookLib;
using System;
using System.Windows.Forms;

namespace TestProject
{
    public partial class Form1 : Form
    {
        MouseHook mh = new MouseHook();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mh.Hook();
            mh.MouseMove += mh_MouseMove;
            mh.MouseClickDown += mh_MouseClickDown;
            mh.MouseClickUp += mh_MouseClickUp;
        }

        void mh_MouseClickUp(object o, MouseEventArgs e)
        {
            Console.WriteLine("Mouse click up !");
            Console.WriteLine(e.Button);
        }

        void mh_MouseClickDown(object o, MouseEventArgs e)
        {
            Console.WriteLine("Mouse click down !");
            Console.WriteLine(e.Button);
        }

        void mh_MouseMove(object o, MouseEventArgs e)
        {
            Console.WriteLine("Mouse move: " + e.Location);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mh.UnHook();
        }
    }
}
