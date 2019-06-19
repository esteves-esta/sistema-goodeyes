using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Xml.Linq;
using System.IO;
using tccGoodEyes.Models;
using System.Web.Hosting;

namespace tccGoodEyes.Relatorio
{
    public class RelatorioVendas
    {
        #region Declaration
        int colunasTotal = 6;
        Document doc;
        Font fontStyle;
        PdfPTable pdfTabela = new PdfPTable(6);
        PdfPCell pdfCell;
        MemoryStream memory = new MemoryStream();
        List<VendasModel> listVendas = new List<VendasModel>();
        #endregion

        public byte[] PrepareReport(List<VendasModel> vendas)
        {
            listVendas = vendas;

            #region
            doc = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            doc.SetPageSize(PageSize.A4);
            doc.SetMargins(20f, 20f, 20f, 20f);
            pdfTabela.WidthPercentage = 100;
            pdfTabela.HorizontalAlignment = Element.ALIGN_LEFT;
            fontStyle = FontFactory.GetFont("Calibri");
            PdfWriter.GetInstance(doc, memory);
            doc.Open();
            pdfTabela.SetWidths(new float[] { 50f, 50f, 50f, 50f, 50f, 50f });
            #endregion

            this.ReportHeader();
            this.ReportBody();
            pdfTabela.HeaderRows = 2;
            doc.Add(pdfTabela);
            doc.Close();
            return memory.ToArray();

        }

        //TOPO DA PÁGINA
        public void ReportHeader()
        {
            //LOGO

            var img = Image.GetInstance(HostingEnvironment.ApplicationPhysicalPath.ToString().Replace('\\', '/') + "imagens/logo2.png");
            pdfCell = new PdfPCell();
            pdfCell.AddElement(img);
            pdfCell.Colspan = 1;
            pdfCell.Border = 0;
            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell.Padding = 10;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            pdfCell = new PdfPCell();
            pdfCell.Colspan = 5;
            pdfCell.Border = 0;
            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell.Padding = 10;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);

            pdfTabela.CompleteRow();

            fontStyle = FontFactory.GetFont("Tahoma", 10f, 1);
            pdfCell = new PdfPCell(new Phrase("RELATÓRIO DE VENDAS", fontStyle));
            pdfCell.Padding = 10;
            pdfCell.Colspan = colunasTotal;
            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfCell.ExtraParagraphSpace = 0;
            pdfTabela.AddCell(pdfCell);
            pdfTabela.CompleteRow();

            fontStyle = FontFactory.GetFont("Tahoma", 9f, 1);
            pdfCell = new PdfPCell(new Phrase("Data do Relatório: " + DateTime.Now.ToString("dd / MM / yyyy"), fontStyle));
            pdfCell.Padding = 10;
            pdfCell.Colspan = colunasTotal;
            pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfCell.Border = 0;
            pdfCell.BackgroundColor = BaseColor.WHITE;
            pdfTabela.AddCell(pdfCell);
            pdfTabela.CompleteRow();
        }

        //ESTILO DE CADA CELULA DA TABELA 
        public void Celula(String conteudo, Single tamanhoFonte, int bold, BaseColor cor) {
            fontStyle = FontFactory.GetFont("Tahoma", tamanhoFonte, bold, cor);

            pdfCell = new PdfPCell(new Phrase(conteudo, fontStyle));
            pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell.BackgroundColor = BaseColor.WHITE;

            pdfCell.Border = 0;
            pdfCell.BorderWidthBottom = 1;
            pdfCell.BorderColorBottom = BaseColor.DARK_GRAY;

            pdfCell.Padding = 10;

            pdfTabela.AddCell(pdfCell);
            
        }
        
        //CORPO DO RELATÓRIO
        public void ReportBody()
        {
            #region COLUNAS

            Celula("CÓD. DA VENDA", 7f, 1, BaseColor.BLACK);
            Celula("CÓD. DO CLIENTE", 7f, 1, BaseColor.BLACK);
            Celula("NOME DO CLIENTE", 7f, 1, BaseColor.BLACK);
            Celula("DATA DA VENDA", 7f, 1, BaseColor.BLACK);
            Celula("TIPO DE PARCELAMENTO", 7f, 1, BaseColor.BLACK);
            Celula("PREÇO TOTAL", 7f, 1, BaseColor.BLACK);

            #endregion

            double precoTotal = 0;
            #region Table Body
            foreach(VendasModel vend in listVendas)
            {
                Celula(vend.cd_pedido, 10f, 0, BaseColor.DARK_GRAY);
                Celula(vend.cd_cliente, 10f, 0, BaseColor.DARK_GRAY);
                Celula(vend.nm_cliente, 10f, 0, BaseColor.DARK_GRAY);
                Celula(vend.dt_pedido, 10f, 0, BaseColor.DARK_GRAY);
                Celula(vend.parcelamento, 10f, 0, BaseColor.DARK_GRAY);
                Celula(vend.vl_total, 10f, 0, BaseColor.DARK_GRAY);

                precoTotal += Convert.ToDouble(vend.vl_total);
            }

            Celula(" ", 10f, 0, BaseColor.DARK_GRAY);
            Celula(" ", 10f, 0, BaseColor.DARK_GRAY);
            Celula(" ", 10f, 0, BaseColor.DARK_GRAY);
            Celula(" ", 10f, 0, BaseColor.DARK_GRAY);
            Celula("TOTAL", 11f, 0, BaseColor.BLACK);
            Celula(precoTotal.ToString(), 11f, 1, BaseColor.BLACK);
            
            #endregion
        }

    }
}