using System.Diagnostics;
using TSoft.SHOficina.ReportGenerator.Application.Receivable.GenerateReceivableReport;
using TSoft.SHOficina.ReportGenerator.Presentation.Factory;

namespace TSoft.SHOficina.ReportGenerator.Presentation
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void btSelectSourceFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK
                && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                txtSourceFile.Text = openFileDialog.FileName;
            }
        }

        private async void btGenerateReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSourceFile.Text))
            {
                MessageBox.Show("Escolha uma planilha de origem", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(txtSourceFile.Text))
            {
                MessageBox.Show("A planilha informada n�o existe", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var useCase = GenerateReceivableReportUseCaseFactory.GenerateUseCase();
                var generateReceivableReportResponse = await useCase.ExecuteAsync(new GenerateReceivableReportRequest
                {
                    InputFilePath = txtSourceFile.Text,
                    OutputFilePath = saveFileDialog.FileName
                }, CancellationToken.None);

                if (!generateReceivableReportResponse.IsSuccess)
                {
                    MessageBox.Show(generateReceivableReportResponse.Message, "Problema ao gerar relat�rio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Relat�rio gerado com sucesso.", "Gera��o de relat�rio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = saveFileDialog.FileName;
                processStartInfo.UseShellExecute = true;
                Process.Start(processStartInfo);
            }            
        }
    }
}
