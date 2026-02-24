using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cqwang.BackEnd.CSharp.Syntax.MemoryLeak
{
    /// <summary>
    /// MainFrame 未退订的事件， 内存泄漏
    /// </summary>
    public partial class MainForm : Form
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainForm()
        {
            //InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();

            this.PropertyChanged += frm.frm_PropertyChanged;
            //MainForm referenced form2, because main form is not released, therefore form2 will not released.    

            DialogResult d = frm.ShowDialog();

            GC.Collect();
            ShowTotalMemory();

        }

        private void ShowTotalMemory()
        {
            //this.listBox1.Items.Add(string.Format("Memory: {0:###,###,###,##0} bytes", GC.GetTotalMemory(true)));
        }
    }

    public partial class Form2 : Form
    {
        public Form2()
        {
            //InitializeComponent();
        }
        public void frm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
    }
}
