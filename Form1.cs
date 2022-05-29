using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Form1 : Form
    {
        string filename = "";
        bool isModified;

        public Form1()
        {
            InitializeComponent();
            this.isModified = false;
        }

        private void 새항목ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            this.isModified = false;

            this.Text = "제목없음 - 메모장";

        }

        private void 열기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.isModified)
            {
                string msg = string.Format("{0} 파일의 내용이 변경되었습니다.\r\n\r\n변경된 내용을 저장하시겠습니까?", filename);
                DialogResult result = MessageBox.Show(msg, "메모장", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    저장ToolStripMenuItem_Click(null, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            this.openFileDialog1.Filter = "텍스트 문서(*.txt)|*.txt|모든파일|*.*";
            openFileDialog1.ShowDialog();
            filename = openFileDialog1.FileName;
            string Data = System.IO.File.ReadAllText(filename);
            textBox1.Text = Data;
            this.isModified = false;
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename == "")
            {
                this.saveFileDialog1.Filter = "텍스트 문서(*.txt)|*.txt|모든파일|*.*";
                this.saveFileDialog1.ShowDialog();
                System.IO.File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
                this.isModified = false;
                filename = saveFileDialog1.FileName;
            }
            else
            {
                System.IO.File.WriteAllText(filename, textBox1.Text);
                this.isModified = false;

            }
        }

        private void 다른이름으로저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.Filter = "텍스트 문서(*.txt)|*.txt|모든파일|*.*";
            this.saveFileDialog1.ShowDialog();
            System.IO.File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            this.isModified = false;
        }

        private void 종료ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 확대ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size + 1, textBox1.Font.Style);
        }

        private void 축소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size - 1, textBox1.Font.Style);
        }

        private void 상태표시줄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (상태표시줄ToolStripMenuItem.Checked)
            {
                상태표시줄ToolStripMenuItem.Checked = statusStrip1.Visible = false;
            }
            else
            {
                상태표시줄ToolStripMenuItem.Checked = true;
                statusStrip1.Visible = true;

            }
           
            if (this.isModified)
                toolStripStatusLabel1.Text = "*";
            else
                toolStripStatusLabel1.Text = "";
                toolStripStatusLabel2.Text = "줄 : " + (textBox1.GetLineFromCharIndex(textBox1.SelectionStart) + 1).ToString();
        }

        private void 줄바꾸기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (줄바꾸기ToolStripMenuItem.Checked)
            {
                줄바꾸기ToolStripMenuItem.Checked = false;
                textBox1.WordWrap = false;
                상태표시줄ToolStripMenuItem.Enabled = true;
                statusStrip1.Visible = true;
            }
            else
            {
                줄바꾸기ToolStripMenuItem.Checked = true;
                textBox1.WordWrap = true;
                상태표시줄ToolStripMenuItem.Enabled = true;
                statusStrip1.Visible = true;
            }
        }

        private void 실행취소ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void 붙여놓ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void 복사ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Copy();
        }

        private void 자르기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Cut();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.isModified = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.isModified = true;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            toolStripSeparator1.Text = FlatStyle.System.ToString();
        }


    }
}
