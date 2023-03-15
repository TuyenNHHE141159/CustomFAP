namespace MyFAP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBox1 = new ComboBox();
            btnShow = new Button();
            label1 = new Label();
            label2 = new Label();
            checkBox1 = new CheckBox();
            button2 = new Button();
            checkedListBox1 = new CheckedListBox();
            dataGridView1 = new DataGridView();
            linkLabel1 = new LinkLabel();
            button3 = new Button();
            label3 = new Label();
            lbTest = new Label();
            openFileDialog1 = new OpenFileDialog();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(111, 80);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(137, 28);
            comboBox1.TabIndex = 0;
            // 
            // btnShow
            // 
            btnShow.Location = new Point(263, 80);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(86, 31);
            btnShow.TabIndex = 1;
            btnShow.Text = "Show";
            btnShow.UseVisualStyleBackColor = true;
            btnShow.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 37);
            label1.Name = "label1";
            label1.Size = new Size(60, 20);
            label1.TabIndex = 2;
            label1.Text = "Teacher";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 84);
            label2.Name = "label2";
            label2.Size = new Size(91, 20);
            label2.TabIndex = 3;
            label2.Text = "SubjectClass";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(27, 524);
            checkBox1.Margin = new Padding(3, 4, 3, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(93, 24);
            checkBox1.TabIndex = 4;
            checkBox1.Text = "Select All";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button2
            // 
            button2.Location = new Point(959, 514);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(86, 31);
            button2.TabIndex = 5;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(14, 152);
            checkedListBox1.Margin = new Padding(3, 4, 3, 4);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(334, 334);
            checkedListBox1.TabIndex = 6;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(365, 152);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(842, 341);
            dataGridView1.TabIndex = 7;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(140, 524);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(69, 20);
            linkLabel1.TabIndex = 8;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Generate";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // button3
            // 
            button3.Location = new Point(1111, 514);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(96, 31);
            button3.TabIndex = 9;
            button3.Text = "Exit";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(111, 37);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 11;
            label3.Text = "label3";
            // 
            // lbTest
            // 
            lbTest.AutoSize = true;
            lbTest.Location = new Point(655, 82);
            lbTest.Name = "lbTest";
            lbTest.Size = new Size(50, 20);
            lbTest.TabIndex = 12;
            lbTest.Text = "label4";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Filter = "Excel Document|*.xls;*xlsx";
            // 
            // button4
            // 
            button4.Location = new Point(215, 524);
            button4.Name = "button4";
            button4.Size = new Size(160, 29);
            button4.TabIndex = 13;
            button4.Text = "Load data from excel";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1269, 616);
            Controls.Add(button4);
            Controls.Add(lbTest);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(linkLabel1);
            Controls.Add(dataGridView1);
            Controls.Add(checkedListBox1);
            Controls.Add(button2);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnShow);
            Controls.Add(comboBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Button btnShow;
        private Label label1;
        private Label label2;
        private CheckBox checkBox1;
        private Button button2;
        private CheckedListBox checkedListBox1;
        private LinkLabel linkLabel1;
        private Button button3;
        private Label label3;
        private Label lbTest;
        private OpenFileDialog openFileDialog1;
        private DataGridView dataGridView1;
        private Button button4;
    }
}