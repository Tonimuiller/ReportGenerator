namespace TSoft.SHOficina.ReportGenerator.Presentation
{
    partial class MainFrm
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            openFileDialog = new OpenFileDialog();
            btSelectSourceFile = new Button();
            txtSourceFile = new TextBox();
            btGenerateReport = new Button();
            saveFileDialog = new SaveFileDialog();
            SuspendLayout();
            // 
            // openFileDialog
            // 
            openFileDialog.Filter = "Excel|*.xlsx";
            openFileDialog.Title = "Selecione um arquivo fonte para gerar o relatório";
            // 
            // btSelectSourceFile
            // 
            btSelectSourceFile.Location = new Point(12, 12);
            btSelectSourceFile.Name = "btSelectSourceFile";
            btSelectSourceFile.Size = new Size(117, 23);
            btSelectSourceFile.TabIndex = 0;
            btSelectSourceFile.Text = "Selecionar Planilha";
            btSelectSourceFile.UseVisualStyleBackColor = true;
            btSelectSourceFile.Click += btSelectSourceFile_Click;
            // 
            // txtSourceFile
            // 
            txtSourceFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSourceFile.Location = new Point(135, 12);
            txtSourceFile.Name = "txtSourceFile";
            txtSourceFile.ReadOnly = true;
            txtSourceFile.Size = new Size(354, 23);
            txtSourceFile.TabIndex = 1;
            txtSourceFile.TabStop = false;
            // 
            // btGenerateReport
            // 
            btGenerateReport.Location = new Point(193, 57);
            btGenerateReport.Name = "btGenerateReport";
            btGenerateReport.Size = new Size(117, 23);
            btGenerateReport.TabIndex = 2;
            btGenerateReport.Text = "Gerar Relatório";
            btGenerateReport.UseVisualStyleBackColor = true;
            btGenerateReport.Click += btGenerateReport_Click;
            // 
            // saveFileDialog
            // 
            saveFileDialog.Filter = "Excel|*.xlsx";
            // 
            // MainFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(504, 88);
            Controls.Add(btGenerateReport);
            Controls.Add(txtSourceFile);
            Controls.Add(btSelectSourceFile);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gerador de relatórios";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog;
        private Button btSelectSourceFile;
        private TextBox txtSourceFile;
        private Button btGenerateReport;
        private SaveFileDialog saveFileDialog;
    }
}
